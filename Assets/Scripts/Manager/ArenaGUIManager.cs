using UnityEngine;
using System.Collections;
using System;

public class ArenaGUIManager : MonoBehaviour
{
		private static ArenaGUIManager _Instance;
		private static string _NAME_ARENA_GUI_MANAGER = "_ArenaGUIManager";
		private GUIText _displayGUIText;
		private GUIText _player1HealthGUIText;
		private GUIText _player1GUIText;
		private SpriteRenderer _player1ImageSpriteRenderer;
		private SpriteRenderer _player1NameBoxSpriteRenderer;
		private GUIText _player2HealthGUIText;
		private GUIText _player2GUIText;
		private SpriteRenderer _player2ImageSpriteRenderer;
		private SpriteRenderer _player2NameBoxSpriteRenderer;
		private SpriteRenderer _loadingSpriteRenderer;
		private GameObject _player1GameObject;
		private GameObject _player2GameObject;

		public enum AnimationState
		{
				IDLE,
				DAMAGE,
				ACTIVE,
				KILLED
		}

		public static ArenaGUIManager Instance {

				get {
						if (_Instance == null) {

								GameObject arenaGUIManager = GameObject.Find (ArenaGUIManager._NAME_ARENA_GUI_MANAGER);
								if (arenaGUIManager == null) {
										arenaGUIManager = new GameObject (ArenaGUIManager._NAME_ARENA_GUI_MANAGER);
								}

								_Instance = arenaGUIManager.GetComponent<ArenaGUIManager> ();
								if (_Instance == null) {
										Debug.Log ("ArenaGUIManager.created()");
										_Instance = arenaGUIManager.AddComponent<ArenaGUIManager> (); 	
								}

						}
						return _Instance;
				}
		}

		public void Awake ()
		{
				DoSetBrittleReferences ();
		}

		public void SetDisplayText (string message)
		{
				_displayGUIText.text = message;
		}

		public void SetPlayerText (int player, string message, Color color)
		{
				if (player == 1) {
						_player1GUIText.text = message;
						_player1GUIText.color = color;
						_player1GUIText.GetComponent<Animator> ().SetTrigger ("show");
				} else {
						_player2GUIText.text = message;
						_player2GUIText.color = color;
						_player2GUIText.GetComponent<Animator> ().SetTrigger ("show");
				}
		}

		public void SetPlayerHealthText (int health, int player)
		{
				if (player == 1) { 
						_player1HealthGUIText.text = "P1 Health: " + health.ToString ();
				} else {
						_player2HealthGUIText.text = "P2 Health: " + health.ToString ();
				}
		}

		public void RenderPlayer (Player player, int gamePlayer)
		{
		
				SetPlayerAnimation (gamePlayer, ArenaGUIManager.AnimationState.IDLE, player.Model);
				this.SetPlayerHealthText (player.Health, gamePlayer);
				if (gamePlayer == 1) { 
						if (MainConstants.MAZINGER_MODEL_ID == player.Model) {
								_player1ImageSpriteRenderer.sprite = Resources.Load <Sprite> (MainConstants.SPRITE_MAZINGER);
								_player1NameBoxSpriteRenderer.sprite = Resources.Load <Sprite> (MainConstants.SPRITE_MAZINGER_NAME_BOX);
						} else if (MainConstants.GODZILLA_MODEL_ID == player.Model) {
								_player1ImageSpriteRenderer.sprite = Resources.Load <Sprite> (MainConstants.SPRITE_GODZILLA);
								_player1NameBoxSpriteRenderer.sprite = Resources.Load <Sprite> (MainConstants.SPRITE_GODZILLA_NAME_BOX);
						} else {
								throw new Exception ("Invalid Model");
						}
				} else {
						if (MainConstants.MAZINGER_MODEL_ID == player.Model) {
								_player2ImageSpriteRenderer.sprite = Resources.Load <Sprite> (MainConstants.SPRITE_MAZINGER);
								_player2NameBoxSpriteRenderer.sprite = Resources.Load <Sprite> (MainConstants.SPRITE_MAZINGER_NAME_BOX);
						} else if (MainConstants.GODZILLA_MODEL_ID == player.Model) {
								_player2ImageSpriteRenderer.sprite = Resources.Load <Sprite> (MainConstants.SPRITE_GODZILLA);
								_player2NameBoxSpriteRenderer.sprite = Resources.Load <Sprite> (MainConstants.SPRITE_GODZILLA_NAME_BOX);
						} else {
								throw new Exception ("Invalid Model");
						}
				}

		}

		public void ShowLoading ()
		{
				_loadingSpriteRenderer.renderer.enabled = true;
		}

		public void SetPlayerAnimation (int player, AnimationState state, long model)
		{

				int modelAsInt = (int)model;

				if (player == 1) {
						this._player1GameObject.GetComponent<Animator> ().SetInteger ("model", modelAsInt);
				} else {
						this._player2GameObject.GetComponent<Animator> ().SetInteger ("model", modelAsInt);
				}
				if (AnimationState.IDLE.Equals (state)) {
						if (player == 1) {
								this._player1GameObject.GetComponent<Animator> ().SetBool ("active", false);
								this._player1GameObject.GetComponent<Animator> ().SetBool ("damage", false);
								this._player1GameObject.GetComponent<Animator> ().SetBool ("killed", false);
						} else {
								this._player2GameObject.GetComponent<Animator> ().SetBool ("active", false);
								this._player2GameObject.GetComponent<Animator> ().SetBool ("damage", false);
								this._player2GameObject.GetComponent<Animator> ().SetBool ("killed", false);
						}
				} else if (AnimationState.DAMAGE.Equals (state)) {
						if (player == 1) {
								this._player1GameObject.GetComponent<Animator> ().SetBool ("active", false);
								this._player1GameObject.GetComponent<Animator> ().SetBool ("damage", true);
								this._player1GameObject.GetComponent<Animator> ().SetBool ("killed", false);
						} else {
								this._player2GameObject.GetComponent<Animator> ().SetBool ("active", false);
								this._player2GameObject.GetComponent<Animator> ().SetBool ("damage", true);
								this._player2GameObject.GetComponent<Animator> ().SetBool ("killed", false);
						}
				} else if (AnimationState.ACTIVE.Equals (state)) {
						if (player == 1) {
								this._player1GameObject.GetComponent<Animator> ().SetBool ("active", true);
								this._player1GameObject.GetComponent<Animator> ().SetBool ("damage", false);
								this._player1GameObject.GetComponent<Animator> ().SetBool ("killed", false);
						} else {
								this._player2GameObject.GetComponent<Animator> ().SetBool ("active", true);
								this._player2GameObject.GetComponent<Animator> ().SetBool ("damage", false);
								this._player2GameObject.GetComponent<Animator> ().SetBool ("killed", false);
						}
				} else if (AnimationState.KILLED.Equals (state)) {
						if (player == 1) {
								this._player1GameObject.GetComponent<Animator> ().SetBool ("active", false);
								this._player1GameObject.GetComponent<Animator> ().SetBool ("damage", false);
								this._player1GameObject.GetComponent<Animator> ().SetBool ("killed", true);
						} else {
								this._player2GameObject.GetComponent<Animator> ().SetBool ("active", false);
								this._player2GameObject.GetComponent<Animator> ().SetBool ("damage", false);
								this._player2GameObject.GetComponent<Animator> ().SetBool ("killed", true);
						}
				}
		}

		public void Clear ()
		{
				_player1HealthGUIText.enabled = false;
				_player2HealthGUIText.enabled = false;
				_player1ImageSpriteRenderer.renderer.enabled = false;
				_player2ImageSpriteRenderer.renderer.enabled = false;
				_player1NameBoxSpriteRenderer.renderer.enabled = false;
				_player2NameBoxSpriteRenderer.renderer.enabled = false;
				_displayGUIText.enabled = false;
				_loadingSpriteRenderer.renderer.enabled = false;

		}

		private void DoSetBrittleReferences ()
		{
				Debug.Log ("ArenaGUIManager.DoSetBrittleReferences ()");
				_displayGUIText = DoThrowErrorIfNull (GameObject.Find (MainConstants.DISPLAY_GUITEXT_GAMEOBJ).GetComponent<GUIText> ()) as GUIText;
				_player1GameObject = GameObject.Find (MainConstants.P1_GAMEOBJ);
				_player2GameObject = GameObject.Find (MainConstants.P2_GAMEOBJ);
				_player1HealthGUIText = DoThrowErrorIfNull (GameObject.Find (MainConstants.PLAYER1_HEALTH_GUITEXT_GAMEOBJ).GetComponent<GUIText> ()) as GUIText;
				_player1GUIText = DoThrowErrorIfNull (GameObject.Find (MainConstants.PLAYER1_GUITEXT_GAMEOBJ).GetComponent<GUIText> ()) as GUIText;
				_player1ImageSpriteRenderer = DoThrowErrorIfNull (GameObject.Find (MainConstants.PLAYER1_IMAGE_SPRITERENDERER_GAMEOBJ).GetComponent<SpriteRenderer> ()) as SpriteRenderer;
				_player1NameBoxSpriteRenderer = DoThrowErrorIfNull (GameObject.Find (MainConstants.PLAYER1_NAMEBOX_SPRITERENDERER_GAMEOBJ).GetComponent<SpriteRenderer> ()) as SpriteRenderer;
				_player2HealthGUIText = DoThrowErrorIfNull (GameObject.Find (MainConstants.PLAYER2_HEALTH_GUITEXT_GAMEOBJ).GetComponent<GUIText> ()) as GUIText;
				_player2GUIText = DoThrowErrorIfNull (GameObject.Find (MainConstants.PLAYER2_GUITEXT_GAMEOBJ).GetComponent<GUIText> ()) as GUIText;
				_player2ImageSpriteRenderer = DoThrowErrorIfNull (GameObject.Find (MainConstants.PLAYER2_IMAGE_SPRITERENDERER_GAMEOBJ).GetComponent<SpriteRenderer> ()) as SpriteRenderer;
				_player2NameBoxSpriteRenderer = DoThrowErrorIfNull (GameObject.Find (MainConstants.PLAYER2_NAMEBOX_SPRITERENDERER_GAMEOBJ).GetComponent<SpriteRenderer> ()) as SpriteRenderer;
				_loadingSpriteRenderer = DoThrowErrorIfNull (GameObject.Find (MainConstants.LOADING_SPRITERENDERER_GAMEOBJ).GetComponent<SpriteRenderer> ()) as SpriteRenderer;
		}

		private UnityEngine.Object DoThrowErrorIfNull (UnityEngine.Object aToCheckObject)
		{

				if (aToCheckObject == null) {
						throw new Exception ("Must not be null : " + aToCheckObject);
				}

				return aToCheckObject;

		}
}
