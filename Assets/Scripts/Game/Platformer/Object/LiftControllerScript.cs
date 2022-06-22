using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer{
	public class LiftControllerScript : MonoBehaviour{
		public enum TypeMove{
			Loop,
			ForwardAndRevert
		}
		public TypeMove modeMove;
		private bool isForward = true;
		public List<Transform> listPoint = new List<Transform>();
		public float speed;
		private Rigidbody2D rb;
		private Transform tr;
		private int targetPoint = 0;

		public bool onEvent = false;
		public bool repeat = false; 
		Coroutine coroutineGoToPoint;
		void Awake(){
			tr = GetComponent<Transform>();
			rb = GetComponent<Rigidbody2D>();
			FindStart();
			if(onEvent == false){
				GoToNextPoint();
			}
		}
		public void GoToNextPoint(){
			if(coroutineGoToPoint != null){
				StopCoroutine(coroutineGoToPoint);
				coroutineGoToPoint = null;
			}
			coroutineGoToPoint = StartCoroutine(IMoveToPoint(FindNextPoint()));
		}
		private void FindStart(){
			Vector3 startPos = tr.position;
			float dist = Mathf.Infinity;
			float curDist = 0f;
			for(int i=0; i < listPoint.Count; i++){
				curDist = Vector3.Distance(startPos, listPoint[i].position);
				if(dist > curDist){
					dist = curDist;
					targetPoint = i;
				}
			}

		}
		private Transform FindNextPoint(){
			switch (modeMove) {
				case TypeMove.Loop:
					targetPoint = ((targetPoint + 1) < listPoint.Count) ? targetPoint + 1 : 0;
					break;
				case TypeMove.ForwardAndRevert:
					targetPoint = (isForward) ? targetPoint + 1 : targetPoint - 1;
					if(((targetPoint + 1) == listPoint.Count) ||(targetPoint == 0 ))
						isForward = !isForward;
					break;	 
			}
			return listPoint[targetPoint];
		}
		Vector3 dir, velocity;
		IEnumerator IMoveToPoint(Transform point){
			dir = point.position - tr.position;
            dir.Normalize();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			while(Vector3.Distance(point.position, tr.position) > 0.25f){
          		velocity = dir * speed;
				rb.velocity = velocity;
				yield return null;
			}
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			rb.velocity = new Vector2();
			HelpFuction.TimerScript.Timer.StartTimer(2f, GoToNextPoint);
		}

		public void EventDone(){
			GoToNextPoint();
		}
	}
}
