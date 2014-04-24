using UnityEngine;
using System.Collections;

public class ArenaScript : MonoBehaviour
{
		// Use this for initialization
		void Start ()
		{
				ArenaGUIManager.Instance.RenderPlayer (SimpleGameManager.Instance.gameManager.Player1, 1);
				ArenaGUIManager.Instance.RenderPlayer (SimpleGameManager.Instance.gameManager.Player2, 2);
				SimpleGameManager.Instance.gameManager.Match ();
		}
		// Update is called once per frame
		void Update ()
		{
	
		}
}
