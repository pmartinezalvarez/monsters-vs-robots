using UnityEngine;
using System.Collections;

public class SimpleGameManager : MonoBehaviour
{
		public GameManager gameManager;
		public TwinspriteManager twinspriteManager;
		public AudioManager audioManager;
		private static SimpleGameManager _Instance;
		private static string _NAME_SIMPLE_GAME_MANAGER = "_SimpleGameManager";

		public void loadNextLevel ()
		{
				Application.LoadLevel (Application.loadedLevel + 1);
		}

		public void Restart ()
		{
				Application.LoadLevel (0);
		}
		//	 Persist Instance
		public void Awake ()
		{ 
				DontDestroyOnLoad (this);
				#pragma warning disable
				if (SimpleGameManager.Instance == null) {
						SimpleGameManager dummy = SimpleGameManager.Instance;
				}
				#pragma warning restore

		}

		public static SimpleGameManager Instance {

				get {
						if (_Instance == null) {

								GameObject simpleGameManager = GameObject.Find (SimpleGameManager._NAME_SIMPLE_GAME_MANAGER);
								if (simpleGameManager == null) {
										simpleGameManager = new GameObject (SimpleGameManager._NAME_SIMPLE_GAME_MANAGER);
								}

								_Instance = simpleGameManager.GetComponent<SimpleGameManager> ();
								if (_Instance == null) {
										Debug.Log ("SimpleGameManager.created()");
										_Instance = simpleGameManager.AddComponent<SimpleGameManager> (); 	
								}

								if (!_Instance.audioManager) {
										_Instance.audioManager = simpleGameManager.AddComponent < AudioManager> ();
								}

								if (!_Instance.gameManager) {
										_Instance.gameManager = simpleGameManager.AddComponent<GameManager> ();
								}

								if (!_Instance.twinspriteManager) {
										_Instance.twinspriteManager = simpleGameManager.AddComponent<TwinspriteManager> ();
								}

						}
						return _Instance;
				}
		}
}