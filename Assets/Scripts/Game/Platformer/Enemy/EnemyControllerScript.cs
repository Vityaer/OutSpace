using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinder;
using HelpFuction;
namespace Platformer{
	public class EnemyControllerScript : MonoBehaviour{
		[Header("Characteristics")]
		public float radiusSee;
		public float radiusAttack;
		protected PathFinderTimerScript pathFinderTimer;
		protected EnemyMovementScript movementScript;
		protected HPControllerScript HPController;
		protected Transform tr;
		protected Rigidbody2D rb;
		public LayerMask whatIsPlayer;
		public LayerMask whatIsEnemy;
		public  bool findingIntruder = false;
		public float timeRememberPlayer = 15f;
		public bool playerContact   = false;

		private Vector3 startPos;
		public enum CurrentBehaviour{
			Safe,
			Follow,
			HoldPosition,
			Protect
		}
		public CurrentBehaviour behaviour;

		public void ChangeBehaviour(CurrentBehaviour behaviour){
			this.behaviour = behaviour;
			DoBehaviour();
		} 
		protected void DoBehaviour(){
			switch (behaviour){
				case CurrentBehaviour.Follow:
					FollowTarget();
					break;
				case CurrentBehaviour.Safe:
					Safe();
					break;
				case CurrentBehaviour.HoldPosition:
					HoldPosition();
					break;			
			}
		} 
		void Awake(){
			HPController   = GetComponent<HPControllerScript>();
			tr             = GetComponent<Transform>();
			rb             = GetComponent<Rigidbody2D>();
			movementScript = GetComponent<EnemyMovementScript>();
			movementScript.RegisterOnAfterMove(WayDone);
			GetComponent<HPControllerScript>()?.RegisterOnDeath(Death);
		}
		protected virtual void Start(){
			startPos = tr.position;
			pathFinderTimer = GameObject.Find("PathFinder").GetComponent<PathFinderTimerScript>();
			SeeAround();
			DoBehaviour();
		}

		public virtual void WayDone(){
			if(movementScript.player?.CompareTag("Player") == false){
				movementScript.player = null;
			}
			if(movementScript.player == null){
				ChangeBehaviour(CurrentBehaviour.HoldPosition);
			}
		} 
//Hold Position
		protected virtual void HoldPosition(){
			if(findingIntruder){
				float time = UnityEngine.Random.Range(5f, 10f);
				HelpFuction.TimerScript.Timer.StartTimer(time, ChangePosition);
			}
		}
		protected void ChangePosition(){
			if(behaviour != CurrentBehaviour.Follow && behaviour != CurrentBehaviour.Safe){
				behaviour = CurrentBehaviour.Follow;
				movementScript.GetNearFindPoint();
			}
		}		
//Follow target		
		protected virtual void FollowTarget(){
			movementScript.FindWayToTarget();
		}
//Safe		
		protected virtual void Safe(){
			movementScript.SafeMe();
		}
//Find
		private bool iSawPlayer = false;
	    protected void SeeAround(){
	    	if(tr != null){
		    	Collider2D coll = Physics2D.OverlapCircle(tr.position, radiusSee, whatIsPlayer);
		    	Vector3 dir;
		    	if(coll != null){
		    		dir = coll.transform.position - tr.position;
		    		if(MyPhysics2D.RaycastFindLayer(tr.position, dir, dir.magnitude, 9) == false){
		    			playerContact = true;
		    			InfoAboutPlayer(coll.transform);
		    		}
		    	}

		    	if(playerContact)
			    	if(movementScript.player != null)
				    	if(movementScript.player.gameObject.CompareTag("Player") == true){
							Collider2D[] colls = Physics2D.OverlapCircleAll(tr.position, radiusSee/4, whatIsEnemy);
							if(colls.Length > 0)
								for(int i=0; i<colls.Length; i++){
									dir = colls[i].transform.position - tr.position;
									if((colls[i].gameObject != gameObject) && (MyPhysics2D.RaycastFindLayer(tr.position, dir, dir.magnitude, 9) == false))
										colls[i].transform.GetComponent<EnemyControllerScript>().InfoAboutPlayer(movementScript.player);
								}
		    	}
				HelpFuction.TimerScript.Timer.StartTimer(1f, SeeAround);
	    	}
	    }
//General
	    private GameTimer timerSeePlayer = null;
	    void ChangeTimerSeePlayer(){
			if(timerSeePlayer == null){
				timerSeePlayer = HelpFuction.TimerScript.Timer.StartTimer(timeRememberPlayer, ForgetPositionPlayer);
			}else{
				timerSeePlayer.Time = timeRememberPlayer;
			}
		}
		void ForgetPositionPlayer(){
			pathFinderTimer.UnRegister(PlayerChangePosition);
			playerContact         = false;
			timerSeePlayer        = null;
			movementScript.player = null;
		}
		public void PlayerChangePosition(){
			if(behaviour == CurrentBehaviour.Follow  || behaviour == CurrentBehaviour.Safe)
				DoBehaviour();
		}
		public virtual void Death(){
			pathFinderTimer.UnRegister(PlayerChangePosition);
			GetComponent<HPControllerScript>()?.UnRegisterOnDeath(Death);
			movementScript.Death();
			GetComponent<Dissolve>()?.StartDissolve();
		}
//API
		public void InfoAboutPlayer(Transform target){
			if(movementScript.player == null || (movementScript.player?.CompareTag("Player") == false)){
				pathFinderTimer.Register(PlayerChangePosition);
			}
			movementScript.player = target;
			if(behaviour != CurrentBehaviour.Safe)
				ChangeBehaviour(CurrentBehaviour.Follow);
			ChangeTimerSeePlayer();
		}
		
	}
}