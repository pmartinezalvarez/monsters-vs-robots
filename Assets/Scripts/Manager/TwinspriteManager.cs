using UnityEngine;
using System.Collections;
using TwinSpriteSDK;

public class TwinspriteManager : MonoBehaviour
{
		private Toyx _player1Toyx;
		private Toyx _player2Toyx;
		private bool _player1Loaded = false;
		private bool _player2Loaded = false;
		private bool _loadError = false;
		private bool _player1Saved = false;
		private bool _player2Saved = false;
		private bool _saveError = false;

		public void Init (string gamekey, string gamesecret)
		{
				TwinSpriteSDK.TwinSprite.initialize (gamekey, gamesecret);
		}

		public void LoadPlayer (string toyxId, int player)
		{

				Debug.Log ("Creating P" + player + " session");

				Toyx toyx = new Toyx (toyxId);

				if (player == 1) {
						_player1Loaded = false;
						_player1Toyx = toyx;
				} else {
						_player2Loaded = false;
						_player2Toyx = toyx;
				}


				IntroGUIManager.Instance.ShowLoading ();

				toyx.CreateSessionInBackground (delegate(TwinSpriteError sessionError) {
						if (sessionError != null) {
								Debug.LogError ("Error creating P" + player + " session: " + sessionError.message);
								_loadError = true;
						} else {
								Debug.Log ("P" + player + " session created successfully");
								Debug.Log ("Fetching P" + player + " data");
								toyx.FetchInBackground (delegate(TwinSpriteError fetchError) {
										if (fetchError != null) {
												Debug.LogError ("Error fetching P" + player + " data: " + fetchError.message);
												_loadError = true;
										} else {
												Debug.Log ("P" + player + " fetched successfully");
												if (player == 1) {
														_player1Loaded = true;
												} else {
														_player2Loaded = true;
												}
										}
								}); 
						}
				});
		}

		public void SavePlayer (Player player, int gamePlayer)
		{
				this.SetPlayer (player, gamePlayer);

				Toyx toyx = null;
				if (gamePlayer == 1) {
						_player1Saved = false;
						toyx = this._player1Toyx;
				} else {
						_player2Saved = false;
						toyx = this._player2Toyx;
				}

				toyx.Save (delegate(TwinSpriteError error) {
						if (error != null) {
								Debug.LogError ("Error saving P" + gamePlayer + " data: " + error.message);
								_saveError = true;
						} else {
								Debug.Log ("P" + gamePlayer + " saved successfully");
								if (gamePlayer == 1) {
										_player1Saved = true;
								} else {
										_player2Saved = true;
								}
						}
				});


		}

		void Update ()
		{
				if (_player1Loaded && _player2Loaded) {
						this.HandlePlayersLoad ();
						_player1Loaded = false;
						_player2Loaded = false;
				}
				if (_loadError) {
						this.HandleLoadError ();
						_loadError = false;
				}
				if (_player1Saved && _player1Saved) {
						this.HandlePlayersSaved ();
						_player1Saved = false;
						_player2Saved = false;
				}
				if (_saveError) {
						this.HandleSaveError ();
						_saveError = false;
				}
		}

		private void HandlePlayersLoad ()
		{
				SimpleGameManager.Instance.gameManager.Player1 = this.GetPlayer (this._player1Toyx);
				SimpleGameManager.Instance.gameManager.Player2 = this.GetPlayer (this._player2Toyx);
				SimpleGameManager.Instance.loadNextLevel ();
		}

		private void HandleLoadError ()
		{
				SimpleGameManager.Instance.Restart ();
		}

		private void HandlePlayersSaved ()
		{
				SimpleGameManager.Instance.Restart ();

		}

		private void HandleSaveError ()
		{
				SimpleGameManager.Instance.Restart ();
		}

		private Player GetPlayer (Toyx toyx)
		{
				Player player = new Player ();
				player.Model = toyx.GetToyxSnapshot ().GetToyxModel ().GetId ();
				Debug.Log ("modlel.id:" + player.Model);
				player.Health = toyx.GetInt (MainConstants.HEALTH_TOYX_ATTR);
				Debug.Log ("health:" + player.Health.ToString ());
				player.Strength = toyx.GetInt (MainConstants.STRENGTH_TOYX_ATTR);
				Debug.Log ("strength:" + player.Strength);
				string abilitiesAsStr = toyx.GetString (MainConstants.ABILITIES_TOYX_ATTR);
				Debug.Log ("abilities:" + abilitiesAsStr);
				abilitiesAsStr = abilitiesAsStr.Replace (" ", "");
				string[] abilityKeys = abilitiesAsStr.Split (',');

				foreach (string key in abilityKeys) {
						Ability ability = new Ability ();
						ability.Name = toyx.GetString (MainConstants.ABILITIES_TOYX_ATTR + MainConstants.ABILITIES_FIELD_DELIMITER + key + MainConstants.ABILITIES_FIELD_DELIMITER + MainConstants.NAME_SUFFIX_TOYX_ATTR);
						ability.Value = toyx.GetInt (MainConstants.ABILITIES_TOYX_ATTR + MainConstants.ABILITIES_FIELD_DELIMITER + key + MainConstants.ABILITIES_FIELD_DELIMITER + MainConstants.VALUE_SUFFIX_TOYX_ATTR);
						ability.Count = toyx.GetInt (MainConstants.ABILITIES_TOYX_ATTR + MainConstants.ABILITIES_FIELD_DELIMITER + key + MainConstants.ABILITIES_FIELD_DELIMITER + MainConstants.COUNT_SUFFIX_TOYX_ATTR);
						player.AbilityKeys.Add (key);
						player.Abilities.Add (key, ability);
				}


				return player;
		}

		private void SetPlayer (Player player, int gamePlayer)
		{
			
				Toyx toyx = null;
				if (gamePlayer == 1) {
						toyx = this._player1Toyx;
				} else {
						toyx = this._player2Toyx;
				}

				toyx.PutInt (MainConstants.HEALTH_TOYX_ATTR, player.Health);
				toyx.PutInt (MainConstants.STRENGTH_TOYX_ATTR, player.Strength);

				foreach (string key in player.AbilityKeys) {
						toyx.PutInt (MainConstants.ABILITIES_TOYX_ATTR + MainConstants.ABILITIES_FIELD_DELIMITER + key + MainConstants.ABILITIES_FIELD_DELIMITER + MainConstants.VALUE_SUFFIX_TOYX_ATTR, player.Abilities [key].Value);
						toyx.PutInt (MainConstants.ABILITIES_TOYX_ATTR + MainConstants.ABILITIES_FIELD_DELIMITER + key + MainConstants.ABILITIES_FIELD_DELIMITER + MainConstants.COUNT_SUFFIX_TOYX_ATTR, player.Abilities [key].Count);
				}
		}
}
