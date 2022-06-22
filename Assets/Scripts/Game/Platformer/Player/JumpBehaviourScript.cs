using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviourScript : MonoBehaviour{
	protected Rigidbody2D rb;
    protected Transform groundCheck;
    protected float groundRadius = 0.05f;
    public LayerMask whatIsGround;
    public float jumpPower = 6f;
	void Awake(){
		whatIsGround  = LayerMask.GetMask("Ground");
		rb = GetComponent<Rigidbody2D>();
		groundCheck = transform.Find("GroundCheck").gameObject.GetComponent<Transform>();
	}
	protected Vector2 dir;
	public virtual Vector2 Jump(){
		dir.x = rb.velocity.x;
		dir.y = CheckGroundUnderPlayer() ? jumpPower : rb.velocity.y;
		return dir;
	}
	protected bool CheckGroundUnderPlayer(){
		return Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		
	}
}
