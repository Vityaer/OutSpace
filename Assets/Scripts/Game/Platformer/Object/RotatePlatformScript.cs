using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatformScript : MonoBehaviour{
	[Range(0f, 100f)]
	public float speed;
	private Rigidbody2D rb;
    void Start(){
    	rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        rb.AddTorque(speed);
    }
}
