using UnityEngine;
using System.Collections;

public class IntroEndTransitionComponent : MonoBehaviour
{

		public void OnIntroEnds ()
		{
				IntroGUIManager.Instance.HideTitle ();
				IntroGUIManager.Instance.ShowTwinspriteForm ();
		}
}
