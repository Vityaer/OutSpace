using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformScript : MonoBehaviour{
    void OnCollisionEnter2D(Collision2D coll){
    	if(coll.gameObject.CompareTag("Player")){
	        coll.transform.parent = transform;
            coll.transform.GetComponent<Rigidbody2D>().gravityScale = 0;
    	}
    }

    void OnCollisionExit2D(Collision2D coll){
    	if(coll.gameObject.CompareTag("Player")){
            coll.transform.GetComponent<Rigidbody2D>().gravityScale = 1;
	        coll.transform.parent = null;
    	}
    }
}
