using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChatControllerScript : MonoBehaviour{
	public Text chatText;
	public void AddMessageToChat(string message){
		chatText.text = chatText.text + message + '\n';
	}
}
