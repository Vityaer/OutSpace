using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControllerScript : MonoBehaviour{

	public enum MoveType{
		Linear,
		Parabolic,
		Hyperbola
	}
	public MoveType moveType;
	public float parametrA = 1f, parametrB = 1f, parametrC  = 0f;
	public float speedX, speedY;
	public float delta = 0.01f;
	private float pX;
	private Rigidbody2D rb;
	private Transform tr;
	void Awake(){
		tr = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
	}
	public Vector2 direction = new Vector2();
	void Update(){
			if(moveType == MoveType.Linear){
				direction.x = parametrA * delta;
				direction.y = parametrB * delta;
			}
			if(moveType == MoveType.Parabolic){
				pX = 2 * parametrA * tr.position.x;
				direction.x = delta;
				direction.y = parametrB * pX * delta;
			}
			rb.velocity = direction; 
	}
}
