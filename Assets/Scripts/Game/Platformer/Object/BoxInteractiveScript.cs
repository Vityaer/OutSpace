using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer;

public class BoxInteractiveScript : MonoBehaviour{

	public GameObject leftObject, rightObject;
	void Start(){
		transform.Find("LeftSide")?.GetComponent<CollisionTriggerScript>().RegisterOnInteractive(MasterLeftPosition);
		transform.Find("RightSide")?.GetComponent<CollisionTriggerScript>().RegisterOnInteractive(MasterRightPosition);
	}
	public void MasterLeftPosition(GameObject masterObject){
		ChangeComponentMaster(ref leftObject, masterObject);
		
	}
	public void MasterRightPosition(GameObject masterObject){
		ChangeComponentMaster(ref rightObject, masterObject);
	}
	private void ChangeComponentMaster(ref GameObject side, GameObject masterObject){
		if(side == null){
			side = masterObject;
			if(masterObject.GetComponent<PlayerPlatformerScript>()){
				masterObject.GetComponent<PlayerPlatformerScript>().enabled = false;
				masterObject.AddComponent<PlayerWithNearBoxScript>();
			}
		}else{
			if(side == masterObject){
				Destroy(masterObject.GetComponent<PlayerWithNearBoxScript>());
				masterObject.GetComponent<PlayerPlatformerScript>().enabled = true;
				side = null;
			}
		}
	} 
}
