using UnityEngine;
using System.Collections;

public class TwinspriteFormComponent : MonoBehaviour
{
		private bool _showForm = false;
		private string gamekey = "43376586D5D541829F24260146D4296D";
		private string gamesecret = "90EDD42C73E64AD7A7C14BD02A5FA849";
		private string player1ToyxId = "NCgABJI1Ap8FwLFydRba6SQdOuAfUeMPKRQUxg";
		private string player2ToyxId = "NCgABJOoOPTtDWFKRoUqDjYkuG2xZHkbw48_RQ";
		// Use this for initialization
		void Start ()
		{
		}
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void Show ()
		{
				_showForm = true;
		}

		public void Hide ()
		{
				_showForm = false;
		}

		void OnGUI ()
		{

				if (this._showForm) {

						int screenWidth = Screen.width;
						int screenHeight = Screen.height;

						int windowWidth = 300;
						int windowHeight = 120;
						int windowX = (screenWidth - windowWidth) / 2;
						int windowY = (int)((screenHeight - windowHeight) / 3);

						GUILayout.Window (0, new Rect (windowX, windowY, windowWidth, windowHeight), TwinspriteForm, "Twinsprite API Information");
				}
		}

		void TwinspriteForm (int windowID)
		{
				GUILayout.BeginVertical ();

				// Game Key
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Game Key", GUILayout.Width (80));
				this.gamekey = GUILayout.TextField (gamekey, GUILayout.Width (192), GUILayout.ExpandWidth (false));
				GUILayout.EndHorizontal ();

				// Game Secret
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Game Secret", GUILayout.Width (80));
				this.gamesecret = GUILayout.TextField (gamesecret, GUILayout.Width (192), GUILayout.ExpandWidth (false));
				GUILayout.EndHorizontal ();

				// Player 1 ToyxID
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("P1 ToyxID", GUILayout.Width (80));
				this.player1ToyxId = GUILayout.TextField (player1ToyxId, GUILayout.Width (192), GUILayout.ExpandWidth (false));
				GUILayout.EndHorizontal ();

				// Player 2 ToyxID
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("P2 ToyxID", GUILayout.Width (80));
				this.player2ToyxId = GUILayout.TextField (player2ToyxId, GUILayout.Width (192), GUILayout.ExpandWidth (false));
				GUILayout.EndHorizontal ();

				if (GUILayout.Button ("Submit")) {	
						if (this.gamekey != null && this.gamesecret != null && this.player1ToyxId != null && this.player2ToyxId != null) {
								IntroGUIManager.Instance.HideTitle ();
								IntroGUIManager.Instance.HideTwinspriteForm ();
								SimpleGameManager.Instance.twinspriteManager.Init (this.gamekey, this.gamesecret);
								SimpleGameManager.Instance.twinspriteManager.LoadPlayer (this.player1ToyxId, 1);
								SimpleGameManager.Instance.twinspriteManager.LoadPlayer (this.player2ToyxId, 2);
						}
				}
				GUILayout.EndVertical ();
		}
}
