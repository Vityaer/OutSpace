using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavControllerScript : MonoBehaviour{
	public string NextLevel = "MainMenu"; 
	public void GoInLobby(){
		Application.LoadLevel(NextLevel);
		GameObject.Find("MultiplayerController").GetComponent<NetworkManagerScript>().SendReturnToLobby();
	}
}
