using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
		private Player _player1;
		private Player _player2;
		private System.Random _rand = new System.Random ();

		public Player Player1 {
				get{ return _player1; }
				set{ _player1 = value; }
		}

		public Player Player2 {
				get{ return _player2; }
				set{ _player2 = value; }
		}

		public void Match ()
		{
				StartCoroutine (this.Play ());
		}

		private IEnumerator Play ()
		{

				if (_player1.Health == 0 || _player2.Health == 0) {
						if (_player1.Health == 0) {
								ArenaGUIManager.Instance.SetPlayerAnimation (1, ArenaGUIManager.AnimationState.KILLED, _player1.Model);
						} 
						if (_player2.Health == 0) {
								ArenaGUIManager.Instance.SetPlayerAnimation (2, ArenaGUIManager.AnimationState.KILLED, _player2.Model);
						}
						ArenaGUIManager.Instance.SetDisplayText ("You can not play with a\ndefeated player.");
						yield return new WaitForSeconds (MainConstants.GAME_END_TIME);
						SimpleGameManager.Instance.Restart ();
				} else {

						// 1. Select the attacker.
						int player = _rand.Next (1, 3);


						bool defeated = false;
						int player1Damage = 0;
						int player2Damage = 0;

						for (int round = 1; round <= 5; round++) {

								ArenaGUIManager.Instance.SetDisplayText ("Round " + round);
								yield return new WaitForSeconds (MainConstants.MESSAGES_TIME);

								for (int turn = 0; turn < 2; turn++) {

										Player attacker = null;
										Player attacked = null;

										if (player == 1) {
												attacker = _player1;
												attacked = _player2;
										} else {
												attacker = _player2;
												attacked = _player1;
										}
										ArenaGUIManager.Instance.SetPlayerAnimation (player, ArenaGUIManager.AnimationState.ACTIVE, attacker.Model);
										ArenaGUIManager.Instance.SetPlayerAnimation (NextPlayer (player), ArenaGUIManager.AnimationState.IDLE, attacked.Model);


										// 2. Select the ability involved in the attack.
										string abilityKey = attacker.AbilityKeys [_rand.Next (0, attacker.AbilityKeys.Count)];
										Ability ability = attacker.Abilities [abilityKey];
										ArenaGUIManager.Instance.SetDisplayText ("Round " + round + " - P" + player);
										yield return new WaitForSeconds (MainConstants.MESSAGES_TIME);
										ArenaGUIManager.Instance.SetDisplayText ("Round " + round + " - P" + player + "\nAbility: " + ability.Name + " (" + ability.Value + ")");
										yield return new WaitForSeconds (MainConstants.MESSAGES_TIME);

										// 3. Perform Attack.
										int attack = this.Attack (attacker, abilityKey);
										ArenaGUIManager.Instance.SetDisplayText ("Round " + round + " - P" + player + "\nAttack Power: " + attack);
										yield return new WaitForSeconds (MainConstants.MESSAGES_TIME);

										// 4. Check Attack Improvements
										ability.Count++;
										if (ability.Count % 3 == 0) {
												ability.Value = Math.Min (ability.Value + 5, 100);
												ArenaGUIManager.Instance.SetDisplayText ("P" + player + "\n" + ability.Name + " +5 (" + ability.Value + ")");
												yield return new WaitForSeconds (MainConstants.MESSAGES_TIME);
										}

										// 5. Block the Attack
										int defense = Block (attacked);
										ArenaGUIManager.Instance.SetDisplayText ("Round " + round + " - P" + player + "\nBlocked: " + defense);
										yield return new WaitForSeconds (MainConstants.MESSAGES_TIME);

										// 6. Inflict Damage
										int damage = Damage (attacked, attack, defense);
										if (player == 1) {
												player1Damage += damage;
										} else {
												player2Damage += damage;
										}
										ArenaGUIManager.Instance.SetPlayerAnimation (this.NextPlayer (player), ArenaGUIManager.AnimationState.DAMAGE, attacked.Model);
										ArenaGUIManager.Instance.SetPlayerHealthText (attacked.Health, this.NextPlayer (player));
										ArenaGUIManager.Instance.SetDisplayText ("Round " + round + " - P" + player + "\nDamage: " + damage);
										ArenaGUIManager.Instance.SetPlayerText (NextPlayer (player), "-" + damage, Color.white);
										yield return new WaitForSeconds (MainConstants.MESSAGES_TIME);
										ArenaGUIManager.Instance.SetPlayerAnimation (this.NextPlayer (player), ArenaGUIManager.AnimationState.IDLE, attacked.Model);

										// 7. Check If player is defeated.
										if (attacked.Health == 0) {
												defeated = true;
												ArenaGUIManager.Instance.SetPlayerAnimation (this.NextPlayer (player), ArenaGUIManager.AnimationState.KILLED, attacked.Model);
												break;
										}
										player = this.NextPlayer (player);
								}

								if (defeated) {
										break;
								}

						}

						if (defeated) {
								ArenaGUIManager.Instance.SetDisplayText ("P" + NextPlayer (player) + " wins!\nP" + player + " was defeated.");
								yield return new WaitForSeconds (MainConstants.GAME_END_TIME);
						} else {
								if (player1Damage > player2Damage) {
										ArenaGUIManager.Instance.SetDisplayText ("P1 wins!\nP1 Damage: " + player1Damage + "\nP2 Damage: " + player2Damage);
										yield return new WaitForSeconds (MainConstants.GAME_END_TIME);
										_player1.Strength = Math.Min (_player1.Strength + 5, 100);
								} else if (player1Damage < player2Damage) {
										ArenaGUIManager.Instance.SetDisplayText ("P2 wins!\nP1 Damage: " + player1Damage + "\nP2 Damage: " + player2Damage);
										yield return new WaitForSeconds (MainConstants.GAME_END_TIME);
										_player2.Strength = Math.Min (_player2.Strength + 5, 100);
								} else {
										ArenaGUIManager.Instance.SetDisplayText ("Draw!\nDamage: " + player1Damage);
										yield return new WaitForSeconds (MainConstants.GAME_END_TIME);
								}
						}

						this.SavePlayers ();
				
				}
		}

		private void SavePlayers ()
		{
				ArenaGUIManager.Instance.Clear ();
				ArenaGUIManager.Instance.ShowLoading ();
				SimpleGameManager.Instance.twinspriteManager.SavePlayer (this._player1, 1);
				SimpleGameManager.Instance.twinspriteManager.SavePlayer (this._player2, 2);

		}

		private int Attack (Player attacker, string abilityKey)
		{
				Ability ability = attacker.Abilities [abilityKey];
				return (int)(0.80 * ability.Value + _rand.Next (0, 20));
		}

		private int Block (Player attacked)
		{
				return _rand.Next (0, attacked.Strength + 1);
		}

		private int Damage (Player attacked, int attack, int defense)
		{
				int damage = (int)(0.3 * Math.Max (0, (attack - defense)));
				attacked.Health = Math.Max (0, attacked.Health - damage);
				if (attacked.Model == MainConstants.GODZILLA_MODEL_ID) {
						SimpleGameManager.Instance.audioManager.DoPlaySound (AudioManager.AudioClipType.GODZILLA_DAMAGE);
				} else {
						SimpleGameManager.Instance.audioManager.DoPlaySound (AudioManager.AudioClipType.MAZINGER_DAMAGE);
				}
				return damage;
		}

		private int NextPlayer (int player)
		{
				if (player == 1) {
						return 2;
				} else {
						return 1;
				}
		}
}
