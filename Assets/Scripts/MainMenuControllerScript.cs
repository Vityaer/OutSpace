using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainMenuControllerScript : NetworkBehaviour{


	public bool LocalPlayer = false; 
	private InputField messageText;
	private Button sendMessageButton;
	void Awake(){
		GetComponents();
	} 
	private LobbyPlayerInfoScript LobbyPlayerInfo;
	void GetComponents(){
		LobbyPlayerInfo = GetComponent<LobbyPlayerInfoScript>();
	}
	public void SendMessage(){
		if(messageText.text.Length > 0){
			if (hasAuthority)
				CmdOnServerSendMessageToHost(messageText.text);
			messageText.text = "";
		}
	}

	//Client
	[Command]
    void CmdOnServerSendMessageToHost(string message){
    	RpcOnClientUpdateChat(message);
    }
    //Host
    [ClientRpc]
	public void RpcOnClientUpdateChat(string message){
    	GameObject.Find("Chat").GetComponent<ChatControllerScript>().AddMessageToChat(message);
	}
	public override void OnStartLocalPlayer(){
		CreateChatController();
		CreateListPlayerController();
		LocalPlayer = true;
		gameObject.name = "localPlayer";
    	base.OnStartLocalPlayer();
    }
    void CreateChatController(){
    	messageText = GameObject.Find("InputFieldMessage").GetComponent<InputField>();
		sendMessageButton = GameObject.Find("sendMessageButton").GetComponent<Button>();
		sendMessageButton.onClick.AddListener(delegate { SendMessage(); });
    }

    void CreateListPlayerController(){
    	LobbyPlayer myPlayerInfo = new LobbyPlayer(0, GameObject.Find("InputNamePlayer").GetComponent<InputField>().text, 5);
		LobbyPlayerInfo.myPlayerInfo = myPlayerInfo;
		CmdOnServerSendInfoNewPlayer(myPlayerInfo.Name);
    }
//List Player    
    // Add player in list    
    [Command]
    void CmdOnServerSendInfoNewPlayer(string name){
    	LobbyPlayerInfo.NewPlayerEnterLobby(name);
    }
   
// Remove player in list
	public void OnClientExitLobby(){
		CmdOnServerSendRequestRemovePlayer(LobbyPlayerInfo.myPlayerInfo.Name);
	}
	[Command]
    void CmdOnServerSendRequestRemovePlayer(string name){
    	LobbyPlayerInfo.RemovePlayerFromLobby(name);
    }
    
}
