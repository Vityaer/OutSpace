using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinder;
namespace Platformer{
	[AddComponentMenu("Platformer/Platformer Enemy Movement")]
	public class EnemyMovementScript : MonoBehaviour{
		protected PathFinderCore pathFinder;

		public Transform player;
		public float speed = 3f;
		public bool isFacingLeft = true;
		protected Coroutine iMovement = null;
		protected Transform tr;
		protected Rigidbody2D rb;
		protected Animator anim;
		protected EnemyControllerScript mainController;
		protected List<Node> Way = new List<Node>();
		protected int currentPoint = 0;
		public Vector2 dir;
		public delegate void Del();
		public Del delsAfterMove;
		protected Vector3 currentPos;
		private Transform groundCheck;
		protected float groundRadius = 0.1f;
		public LayerMask whatIsGround;
		void Awake(){
			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody2D>();
			tr = GetComponent<Transform>();
			mainController = GetComponent<EnemyControllerScript>();
			groundCheck = transform.Find("GroundCheck");
			pathFinder = GameObject.Find("PathFinder").GetComponent<PathFinderCore>();
		}
		protected virtual void Start(){}


		protected virtual void StartWay(){
			CheckValidWay();
			currentPoint = 0;
			GoToPoint(Way[0].Point);
		}
		protected virtual void CheckValidWay(){
			if(Way.Count >= 2){
				if(MyPhysics2D.RaycastFindLayer(tr.position, Way[1].Point.position - tr.position, (Way[1].Point.position - tr.position).magnitude, 9) == false)
					Way.RemoveAt(0);
			}
		}

		protected virtual void NextPoint(){
			currentPoint++;
			if(Way.Count > currentPoint){
				GoToPoint(Way[currentPoint].Point);
			}else{
				FinishMove();
			}
		}
		protected virtual void FinishMove(){
				currentPoint = 0;
			 	if(delsAfterMove != null)
	            	delsAfterMove();
		}

		protected virtual void Flip(){
            isFacingLeft = !isFacingLeft;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
		protected virtual void GoToPoint(Transform target){
			if(iMovement != null){
				StopCoroutine(iMovement);
				iMovement = null; 
			}
			iMovement = StartCoroutine(IMoveToPoint(target));
		}

		protected virtual void FixedUpdate(){
			rb.velocity = dir * speed * Time.deltaTime * 10f;
		}



		public void RegisterOnAfterMove(Del d){
			delsAfterMove += d;
		}
		IEnumerator IMoveToPoint(Transform target){
            // anim.SetBool("Speed", true);
            currentPos = target.position;
			dir.x = currentPos.x - tr.position.x;
            dir.y = currentPos.y - tr.position.y;
            dir.Normalize();
            float startDist = Vector2.Distance(tr.position, currentPos); 
            float dist = startDist;
            while((dist <= startDist) && (dist > 0.15f)){
        		dist = Vector2.Distance(tr.position, currentPos);
				yield return null;
            }
            dir.x = 0;
           	dir.y = 0;
	        NextPoint();    
			CheckPosTarget(); 
            // anim.SetBool("Speed", false);
		}
		protected bool CheckGround(){
			return Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		}
		protected void CheckPosTarget(){
			if((Mathf.Abs(currentPos.x - tr.position.x) < 3f) &&(Mathf.Abs(currentPos.y - tr.position.y) < 2f)){
				if(!((currentPos.x > tr.position.x) ^ isFacingLeft)){
					Flip();
				}
			}
		}
		public void CheckPosTarget(Transform target){
            currentPos = target.position;
			CheckPosTarget();
		}
//API
		public virtual void SafeMe(){
			Way.Clear();
			if((tr != null)&&(player != null))
				pathFinder.FindWay(pathFinder.GetNearPointToTarget(tr.position) , pathFinder.GetNearSafePoint(tr, player), Way);
			StartWay();
		}
		public void Death(){
			dir = new Vector2();
			if(iMovement != null)
				StopCoroutine(iMovement);
		}
		public void GetNearFindPoint(){
			player = (pathFinder.GetRandomNearPointNeigbours(tr.position)).Point;
			FindWayToTarget();
		}
		public void FindWayToTarget(){
			Way.Clear();

			if((tr != null)&&(player != null)){
				pathFinder.FindWay(pathFinder.GetNearPointToTarget(tr.position) , pathFinder.GetNearPointToTarget(player.position), Way);
				StartWay();
			}
		}
	}
}
