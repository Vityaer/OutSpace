using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikadzeBoxSaveScript : MonoBehaviour{

	void Start(){
	    transform.Find("AttackPoint").GetComponent<DangerControllerScript>()?.Register(Open);
	}

	public void Open(GameObject player){
		transform.Find("AttackPoint").GetComponent<DangerControllerScript>().UnRegister(Open);
		Destroy(gameObject);
	}
}
