using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour{
    public Transform target;
    private Transform tr;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3F;
    public Vector3 delta = new Vector3(3f, 2f, 10f);
    Vector3 targetPosition = new Vector3();
    void Awake(){
    	tr = GetComponent<Transform>();
    }
    void Update(){
        targetPosition = target.position + delta;
        tr.position = Vector3.SmoothDamp(tr.position, targetPosition, ref velocity, smoothTime);
    }
}
