using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FearEnemyControllerScript : MonoBehaviour{

	private EnemyMovementScript movementBehaviour; 
	
	void Awake(){
		movementBehaviour = transform.parent.gameObject.GetComponent<EnemyMovementScript>();
	}
	Vector2 posDanger = new Vector2();
    public void Attention(Vector3 danger){
    	posDanger.x = danger.x;
    	posDanger.y = danger.y;
		movementBehaviour.FearPoint(posDanger);
    }
    void OnTriggerEnter2D(Collider2D other) {
	    if(other.gameObject.CompareTag("Bullet")){
	    	Attention(other.gameObject.transform.position);
    	}
    }
}
