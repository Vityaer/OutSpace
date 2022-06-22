using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinder;

namespace Platformer{
	public class KamikadzeMovementScript : EnemyMovementScript{
		private KamikadzeControllerScript mainControllerScript; 
		private Transform groundCheckFaceDown;
	    void Start(){
	    	groundCheckFaceDown = transform.Find("GroundCheckFaceDown");
	    	mainControllerScript = GetComponent<KamikadzeControllerScript>();
			base.Start();
	    }
	    protected override void CheckValidWay(){}
	    protected override void GoToPoint(Transform target){
			if(iMovement != null){
				StopCoroutine(iMovement);
				iMovement = null; 
			}
			iMovement = StartCoroutine(IMoveToPoint(target));
		}
		protected override void FixedUpdate(){
            dir.y = rb.velocity.y;
			if(CheckGround()){
				rb.velocity = dir;
			}
		}
	    IEnumerator IMoveToPoint(Transform target){
            // anim.SetBool("Speed", true);
            Debug.Log("next point " + target.name);
            if(Way.Count > (currentPoint + 1)) {
	            ChooseMethodMove(target, Way[currentPoint + 1].Point);
            }
            float startDist = Vector2.Distance(tr.position, target.position); 
            float dist = startDist;
            while(!CheckDistance(target) && (dist <= (startDist + 3f)) ){
        		dist = Vector2.Distance(tr.position, target.position);
        		
        		if(dist < startDist){
        			startDist = dist;
        		}
				yield return null;
            }
            dir.x = 0;
            if(dist > (startDist + 3f)) {
            	Debug.Log("мы слишком далеко");
            	FindWayToTarget();
            }else{
		        NextPoint();    
				CheckPosTarget(); 
            }
            // anim.SetBool("Speed", false);
		}
		public bool groundPoint0, groundPoint1, groundPoint2;
		private void ChooseMethodMove(Transform target, Transform nextTarget){
			Vector3 point0 = tr.position;
			Vector3 point1 = target.position;
			Vector3 point2 = nextTarget.position;
			bool isLeft = (point0.x - point1.x > 0f) ? true : false;
			bool isJump = (point1.y - point0.y > 1f) ? true : false;
			bool isFall = (point1.y - point2.y > 1f) ? true : false;
			 groundPoint0 = CheckGround();
			 groundPoint1 = MyPhysics2D.RaycastFindLayer(point1, new Vector3(0,-1,0), 2f, 9);
			 groundPoint2 = MyPhysics2D.RaycastFindLayer(point2, new Vector3(0,-1,0), 2f, 9);
			if(isLeft){
				if(groundPoint0)
						dir.x = -speed;
				if(isFall){
					Debug.Log("foll left");
				}
				if(isJump){
					Debug.Log("jump left");
				}
				if(!isFall && !isJump){
					Debug.Log("run left");
					
					if((groundPoint2 == true)&&(groundPoint1 == false) && groundPoint0)
						dir = new Vector2(-6 * speed, 7f);
					if((groundPoint2 == false)&&(groundPoint1 == false) && groundPoint0)
						mainControllerScript.ChangeGravity(-1f);
							
				}
			}else{
				if(groundPoint0)
						dir.x = speed;
				if(isFall){
					Debug.Log("foll right");
				}
				if(isJump){
					Debug.Log("jump right");
				}
				if(!isFall && !isJump){
					Debug.Log("run right");
					
					if((groundPoint2 == true)&&(groundPoint1 == false) && groundPoint0)
						dir = new Vector2(6 * speed, 7f);
					if((groundPoint2 == false)&&(groundPoint1 == false) && groundPoint0)
						mainControllerScript.ChangeGravity(-1f);
				}
			}		
		}
		private bool CheckDistance(Transform target){
			bool result = false;
			if ((Mathf.Abs(tr.position.x - target.position.x) < 0.15f) &&(Mathf.Abs(tr.position.y - target.position.y) < 5f)){
				result = true; 
			} 
			return result;
		}
		private bool CheckGroundFaceDown(){
			return Physics2D.OverlapCircle(groundCheckFaceDown.position, groundRadius, whatIsGround);
		}
	}
}