using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DangerControllerScript : MonoBehaviour{
	public delegate void Del(GameObject player);
	public Del dels;
	public void Register(Del d){
		dels += d;
	}
	public void UnRegister(Del d){
		dels -= d;
	}
	private void CollisionPlayer(GameObject player){
		if(dels != null)
			dels(player);	
	}
    void OnTriggerEnter2D(Collider2D other){
			if(other.gameObject.CompareTag("Player")){
				CollisionPlayer(other.gameObject);
			}
		}
}
