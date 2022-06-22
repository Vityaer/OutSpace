using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinder;

namespace Platformer{
	public class FireGuardMovementScript : EnemyMovementScript{
	    protected void GoToPoint(Transform target){
			if(iMovement != null){
				StopCoroutine(iMovement);
				iMovement = null; 
			}
			iMovement = StartCoroutine(IMoveToPoint(target));
		}
		protected override void CheckValidWay(){
			Vector2 dist;
			int result = 0;
			for(int i=0; i<Way.Count - 1; i++){
				dist.x = Mathf.Abs(Way[i].Point.position.x - Way[i+1].Point.position.x);
				dist.y = Mathf.Abs(Way[i].Point.position.y - Way[i+1].Point.position.y);
				if(dist.y/dist.x > 2f){
					result = i + 1;
					break;
				}
			}
			Way.RemoveRange(result, Way.Count - result);
		}
		protected override void FixedUpdate(){
            dir.y = rb.velocity.y;
			rb.velocity = dir;
		}
	    IEnumerator IMoveToPoint(Transform target){
            // anim.SetBool("Speed", true);
            Debug.Log("next point " + target.name);
			dir.x = (target.position.x > tr.position.x) ? speed : -speed;
            float vx = dir.x;
            float startDist = Mathf.Abs(target.position.x - tr.position.x);
            float dist = startDist;
            while((dist <= startDist) && (dist > 0.15f)){
        		dist = Mathf.Abs(target.position.x - tr.position.x);
	            dir.x = CheckGround() ? vx : 0f;
				yield return null;
            }
            dir.x = 0;
	        NextPoint();    
			CheckPosTarget(); 
            // anim.SetBool("Speed", false);
		}
	}
}
