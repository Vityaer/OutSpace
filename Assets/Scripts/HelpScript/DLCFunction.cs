using System.Collections;
using System.Collections.Generic;

namespace UnityEngine{
	public static class MyPhysics2D{
		public static bool RaycastFindLayer(Vector3 startPoint, Vector3 dir, float dist, int layerFind = 0){
			RaycastHit2D[] hit;
			bool result = false;
			hit = Physics2D.RaycastAll(startPoint, dir, dist);
		    	for(int i=0; i<hit.Length; i++){
	    			if(hit[i].collider.gameObject.layer == layerFind){
	    				result  = true;
    					break;
    				}
    		}
			return result;
		}
		public static GameObject RaycastGetFirstLayerCollision(Vector3 startPoint, Vector3 dir, float dist, int layerFind = 0){
			RaycastHit2D[] hit;
			GameObject result = null;
			hit = Physics2D.RaycastAll(startPoint, dir, dist);
		    	for(int i=0; i<hit.Length; i++){
	    			if(hit[i].collider.gameObject.layer == layerFind){
	    				result  = hit[i].collider.gameObject;
	    				break;
    				}
    		}
			return result;
		}  
	}
}
