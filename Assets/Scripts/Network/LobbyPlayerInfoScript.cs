using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class LobbyPlayer{
	
	[SerializeField]
	private int id;
	public int ID{get{return id;} set{id = value;}}

	[SerializeField]
	private string name;
	public string Name{get{return name;} set{if(value.Length > 0) name = value;}}
	
	[SerializeField]
	private int level;
	public int Level{get{return level;} set{level = value > 0 ? value : 0;}}
	
	public LobbyPlayer(int _id,string _name, int _level){
		id = _id;
		name  = _name;
		level = _level;
	}
}
public class LobbyPlayerInfoScript : NetworkLobbyPlayer{
	public LobbyPlayer myPlayerInfo;
	public ListPlayersScript ListPlayersController;
	void Awake(){
		ListPlayersController = GameObject.Find("ListPlayer").GetComponent<ListPlayersScript>();
	}
	public void NewPlayerEnterLobby(string name){
		// ListPlayersController = GameObject.Find("ListPlayer").GetComponent<ListPlayersScript>();
		foreach(LobbyPlayer lp in ListPlayersController.ListPlayer){
	    	RpcOnClientAddPlayerList(lp.ID, lp.Name);
		}
		RpcOnClientAddPlayerList(ListPlayersController.GetNewID(), name);
	}

	[ClientRpc]
	public void RpcOnClientAddPlayerList(int ID, string name){
    	ListPlayersController.AddPlayer(new LobbyPlayer(ID, name, 5));
	}

	public void RemovePlayerFromLobby(string name){
		RpcOnClientRemovePlayerFromList(name);
	}

	[ClientRpc]
	public void RpcOnClientRemovePlayerFromList(string name){
		ListPlayersController.RemovePlayer(new LobbyPlayer(0, name, 5));
	}	
}
