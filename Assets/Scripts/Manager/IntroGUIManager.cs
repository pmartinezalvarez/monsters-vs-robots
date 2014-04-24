using UnityEngine;
using System;
using System.Collections;

public class IntroGUIManager : MonoBehaviour
{
		private static IntroGUIManager _Instance;
		private static string _NAME_INTRO_GUI_MANAGER = "_IntroGUIManager";
		private TwinspriteFormComponent _twinspriteFormComponent;
		private SpriteRenderer _titleSpriteRenderer;
		private SpriteRenderer _loadingSpriteRenderer;
		private GUIText _displayGUIText;
		private SpriteRenderer _godzillaSplashSpriteRenderer;
		private SpriteRenderer _mazingerSplashSpriteRenderer;

		public static IntroGUIManager Instance {

				get {
						if (_Instance == null) {

								GameObject introGUIManager = GameObject.Find (IntroGUIManager._NAME_INTRO_GUI_MANAGER);
								if (introGUIManager == null) {
										introGUIManager = new GameObject (IntroGUIManager._NAME_INTRO_GUI_MANAGER);
								}

								_Instance = introGUIManager.GetComponent<IntroGUIManager> ();
								if (_Instance == null) {
										Debug.Log ("IntroGUIManager.created()");
										_Instance = introGUIManager.AddComponent<IntroGUIManager> (); 	
								}

						}
						return _Instance;
				}
		}

		public void Awake ()
		{
				DoSetBrittleReferences ();
		}

		public void ShowTwinspriteForm ()
		{
				_twinspriteFormComponent.Show ();
		}

		public void HideTwinspriteForm ()
		{
				_twinspriteFormComponent.Hide ();
		}

		public void ShowLoading ()
		{
				_loadingSpriteRenderer.enabled = true;
		}

		public void HideLoading ()
		{
				_loadingSpriteRenderer.enabled = false;
		}

		public void SetDisplayText (string message)
		{
				_displayGUIText.text = message;
		}

		public void HideTitle ()
		{
				_titleSpriteRenderer.enabled = false;
		}

		public void PlayGodzillaIntroAnimation ()
		{
				_godzillaSplashSpriteRenderer.GetComponent<Animator> ().Play ("title_show");
		}

		private void DoSetBrittleReferences ()
		{
				Debug.Log ("IntroGUIManager.DoSetBrittleReferences ()");
				_twinspriteFormComponent = DoThrowErrorIfNull (GameObject.Find (MainConstants.TWINSPRITE_FORM_GAMEOBJ).GetComponent<TwinspriteFormComponent> ()) as TwinspriteFormComponent;
				_titleSpriteRenderer = DoThrowErrorIfNull (GameObject.Find (MainConstants.TITLE_SPRITERENDERER_GAMEOBJ).GetComponent<SpriteRenderer> ()) as SpriteRenderer;
				_displayGUIText = DoThrowErrorIfNull (GameObject.Find (MainConstants.DISPLAY_GUITEXT_GAMEOBJ).GetComponent<GUIText> ()) as GUIText;
				_loadingSpriteRenderer = DoThrowErrorIfNull (GameObject.Find (MainConstants.LOADING_SPRITERENDERER_GAMEOBJ).GetComponent<SpriteRenderer> ()) as SpriteRenderer;
				_godzillaSplashSpriteRenderer = DoThrowErrorIfNull (GameObject.Find (MainConstants.GODZILLA_SPLASH_SPRITERENDERER_GAMEOBJ).GetComponent<SpriteRenderer> ()) as SpriteRenderer;
				_mazingerSplashSpriteRenderer = DoThrowErrorIfNull (GameObject.Find (MainConstants.MAZINGER_SPLASH_SPRITERENDERER_GAMEOBJ).GetComponent<SpriteRenderer> ()) as SpriteRenderer;
		}

		private UnityEngine.Object DoThrowErrorIfNull (UnityEngine.Object aToCheckObject)
		{

				if (aToCheckObject == null) {
						throw new Exception ("Must not be null : " + aToCheckObject);
				}

				return aToCheckObject;

		}
}
