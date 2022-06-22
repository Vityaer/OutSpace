using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinder;

namespace Platformer{
	public class FlyBotMovementScript : EnemyMovementScript{
		    protected override void NextPoint(){
				currentPoint += 1;
				if(Way.Count > currentPoint){
					GoToPoint(GetRandomBetweenPoint(Way[currentPoint].Point), Way[currentPoint].Point);
				}else{
					currentPoint = 0;
				 	if(delsAfterMove != null)
		            	delsAfterMove();
				}
			}
			private void GoToPoint(Vector3 firstPoint, Transform secondPoint){
				if(iMovement != null){
					StopCoroutine(iMovement);
					iMovement = null; 
				}
				iMovement = StartCoroutine(IMoveToPoint(firstPoint, secondPoint));
			}
			protected override void StartWay(){
				if(Way.Count > 0){
					oldDir = new Vector2();
					CheckValidWay();
					currentPoint = 0;
					GoToPoint(GetRandomBetweenPoint(Way[0].Point), Way[0].Point);
				}
			}
			protected override void FixedUpdate(){
				rb.velocity = dir*speed;
			}
			Vector3 oldDir;
		    IEnumerator IMoveToPoint(Vector3 betweenTarget, Transform target){
		    	Vector2 point1 = tr.position,
		    			point2 = betweenTarget,
				    	point3 = target.position,
				    	posBezie;
	            float dist, alfa = 0f;
	            posBezie = CalculateBezieDir(point1, point2,point3, alfa);
	            while(alfa <= 1f){
	        		dist = Vector2.Distance(posBezie, tr.position);
	        		dir.x = posBezie.x - tr.position.x;
	        		dir.y = posBezie.y - tr.position.y;
	            	dir.Normalize();
	        		if(dist < 0.1f){
	        			alfa += 0.1f;
	        			posBezie = CalculateBezieDir(point1, point2,point3, alfa);
		            	CheckFace(dir);
	        			if(alfa >= 0.6f) SmoothWay();
	        		}
					yield return null;
	            }
	            
	            oldDir = dir;
	            dir.x = 0;
	            dir.y = 0;
		        NextPoint();    
				
				void SmoothWay(){
					if(currentPoint < Way.Count - 1){		
						Vector2 dirNextPoint = Way[currentPoint + 1].Point.position - tr.position;
						if((Way[currentPoint + 1].Point.position - Way[currentPoint].Point.position).magnitude > 3f){
							if(!MyPhysics2D.RaycastFindLayer(tr.position, dirNextPoint, dirNextPoint.magnitude, 9)){
								alfa = 2f; 
							}
						}
	        		}
				}	 
			}

			Vector2 CalculateBezieDir(Vector2 point1, Vector2 point2, Vector2 point3, float alfa){
				Vector2 t1 = alfa * (point2 - point1) + point1;
            	Vector2 t2 = alfa * (point3 - point2) + point2;
            	return alfa * (t2 - t1) + t1;
			} 
			Vector3 GetRandomBetweenPoint(Transform target){
				Vector3 result = tr.position + (UnityEngine.Random.Range(0.5f, 2f) * oldDir);
				return result;
			}
			void CheckFace(Vector2 dir){
				if(Mathf.Abs(dir.x) > 0.1f){
					if((dir.x > 0) != isFacingLeft){
						Flip();
					}
				}
			}
	}
}