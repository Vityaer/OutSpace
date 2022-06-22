using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ListPlayersScript : NetworkBehaviour{
	private Text listPlayerText;
	public List<LobbyPlayer> ListPlayer = new List<LobbyPlayer>();
	public int IDPlayer = 0;
	public int GetNewID(){
		return IDPlayer++;
	}
	void Awake(){
		listPlayerText = GetComponent<Text>(); 
	}
	public void RemovePlayer(LobbyPlayer player){
		ListPlayer.Remove(player);
		UpdateListPlayerUI();
	}
	public void AddPlayer(LobbyPlayer newPlayer){
		bool flag = false;
		foreach(LobbyPlayer players in ListPlayer){
			if(players.Name == newPlayer.Name){
				flag = true;
			}
		}
		if(!flag){
			ListPlayer.Add(newPlayer);
		}
		UpdateListPlayerUI(); 
	}
	private void UpdateListPlayerUI(){
		string listName = "";
		foreach(LobbyPlayer curPlayer in ListPlayer){
			listName += curPlayer.Name + "\n";
		}
		listPlayerText.text = listName;

	} 
}
