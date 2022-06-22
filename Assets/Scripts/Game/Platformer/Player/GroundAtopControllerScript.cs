using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using Platformer;
using HelpFuction;

public class GroundAtopControllerScript : MonoBehaviour{
	private BoxCollider2D boxCollider;
	void Awake(){
		boxCollider = GetComponent<BoxCollider2D>();
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			if(other.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0){
				other.gameObject.GetComponent<PlayerPlatformerScript>().ChangePlayer(isHard: false);
			}
		}
	}
	GameObject player;
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			player = other.gameObject; 
			HelpFuction.TimerScript.Timer.StartTimer(0.2f, HardTruePlayer);
		}
	}
	void HardTruePlayer(){
		player.GetComponent<PlayerPlatformerScript>().ChangePlayer(isHard: true);
	}
}
