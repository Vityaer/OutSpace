using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer{
	public class BotRepairControllerScript : MonoBehaviour{
	    private Transform tr;
	    private Rigidbody2D rb;
		private EnemyMovementScript movementScript;
	    private bool isWork = false;
	    public Queue<Transform> listPointBreak = new Queue<Transform>();

	    void Awake(){
	        tr             = GetComponent<Transform>();
			rb             = GetComponent<Rigidbody2D>();
			movementScript = GetComponent<EnemyMovementScript>();
	    }

	    public void GoToNextPoint(){
	    	if((listPointBreak.Count > 0)&&(!isWork)){
	    	}
	    }
	    public void AddNewTarget(Transform target){
	    	listPointBreak.Enqueue(target);
	    	GoToNextPoint();
	    }
	}
}