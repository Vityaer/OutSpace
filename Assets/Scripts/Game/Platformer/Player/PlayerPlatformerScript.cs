using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer{
    public class PlayerPlatformerScript : MonoBehaviour{

        private bool isAlive = true;

    	private Rigidbody2D rb;
        private bool isFacingRight = false;
        public float runForwardSpeed = 2f;
        public float runBackSpeed = 1f;
        public JumpBehaviourScript jumpBehaviour;
        private Transform groundCheck;
        public float radiusGroundCheck = 0.05f;
        private Transform tr;
    	void Awake(){
            tr = GetComponent<Transform>();
            boxCollider = GetComponent<BoxCollider2D>();
            circleCollider = GetComponent<CircleCollider2D>();
            jumpBehaviour = gameObject.AddComponent<DoubleJumpScript>();
    		rb = GetComponent<Rigidbody2D>();
            groundCheck = transform.Find("GroundCheck").gameObject.GetComponent<Transform>();
    	}
    	float moveX = 0f;
        Vector2 direction = new Vector2();
        void Start(){
            GetComponent<PlayerHPControllerScript>().RegisterOnDeath( HPEquals0 );
        }
        GameObject objectUnderLegs;
        void Update(){
            if(CanMoving()){
                if(Input.GetKey( KeyCode.A )){
                    moveX = -1f * (isFacingRight ? runBackSpeed : runForwardSpeed);
                }
                if(Input.GetKey( KeyCode.D )){
                    moveX = 1f  * (isFacingRight ? runForwardSpeed : runBackSpeed);
                }
                if(Input.GetKeyUp( KeyCode.A ) || Input.GetKeyUp( KeyCode.D )){
                    moveX = 0f;
                }
                
                if(Input.GetKeyDown(KeyCode.S)){
                    objectUnderLegs = MyPhysics2D.RaycastGetFirstLayerCollision(groundCheck.position, Vector2.down,  radiusGroundCheck, 9);
                    if (objectUnderLegs != null){
                        CheckPosibleDownFall(objectUnderLegs);
                    }
                }
                if(Input.GetKeyDown( KeyCode.W )){
                	rb.velocity = jumpBehaviour.Jump();
                }
            }
        }
        void FixedUpdate(){
            if(!playerUnderStun){
                direction.x = moveX;
                direction.y = rb.velocity.y;
                rb.velocity = direction;
            }
        }
        public void Flip(bool isFacingRight){
            this.isFacingRight = isFacingRight; 
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        private void CheckPosibleDownFall(GameObject hit){
            if(hit.GetComponent<GroundAtopControllerScript>()){
                ChangePlayer(isHard: false);
            }
        }
        BoxCollider2D boxCollider;
        CircleCollider2D circleCollider;
        public void ChangePlayer(bool isHard){
            boxCollider.isTrigger    = !isHard;
            circleCollider.isTrigger = !isHard;
        }
        
        bool result = true;
        bool CanMoving(){
            if(playerUnderStun == true ) result = false;
            if(isAlive         == false) result = false;
            return result;
        }
        void HPEquals0(){
            isAlive = false;
        }
// Stun
        public bool playerUnderStun = false;
        public void SetStun(float timeStun = 1f){
            playerUnderStun = true;
            HelpFuction.TimerScript.Timer.StartTimer(timeStun, ClearStun);         
        }
        void ClearStun(){
            playerUnderStun = false;
        }
    }
    

}
