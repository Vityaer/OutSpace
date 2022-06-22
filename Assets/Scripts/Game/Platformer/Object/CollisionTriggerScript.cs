using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTriggerScript : MonoBehaviour{
	public bool isInteractivePlayer = false;
	public bool isInteractiveEnemy = false;
    public delegate void Del(GameObject masterObject);
	private Del delsCollision;
	private Del delsInteractive;
//Collision	
	public void RegisterOnCollision(Del d){
		delsCollision += d;
	}
	public void UnRegisterOnCollision(Del d){
		delsCollision -= d;
	}
	private void CollisionPlayer(){
		if(delsCollision != null)
			delsCollision(masterObject);	
	}
//Interactive	
	public void RegisterOnInteractive(Del d){
		delsInteractive += d;
	}
	public void UnRegisterOnInteractive(Del d){
		delsInteractive -= d;
	}
	private void MasterInteractiveWithObject(){
		if(delsInteractive != null)
			delsInteractive(masterObject);
	}
	public bool masterObjectInner;
	public GameObject masterObject;
//Triggers	
    void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
	    	masterObject = other.gameObject;
	    	masterObjectInner = true;
			CollisionPlayer();
		}	
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject == masterObject){
			masterObject = null;
			masterObjectInner = false;
		}
	}
	void Update(){
		if(masterObjectInner){
			if(Input.GetKeyDown( KeyCode.E )){
				MasterInteractiveWithObject();
			}
		}
	}
}
