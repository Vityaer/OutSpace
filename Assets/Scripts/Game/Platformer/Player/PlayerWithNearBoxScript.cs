using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer{
	public class PlayerWithNearBoxScript : MonoBehaviour{
		private Animator anim;
		private Rigidbody2D rb;
		void Awake(){
			rb = GetComponent<Rigidbody2D>();
			anim = GetComponent<Animator>();
		}
		void Start(){
			anim.SetBool("isOnPosition", true);
			anim.SetFloat("Vertical", 0f); 
		}
		void Update(){
			if(Input.GetKeyDown( KeyCode.W )){
				Debug.Log("up");
				anim.SetFloat("Vertical", 1f); 	
			}
			if(Input.GetKeyDown( KeyCode.S )){
				Debug.Log("down");
				anim.SetFloat("Vertical", -1f); 	
			}	
		}
		void OnDestroy(){
			anim.SetBool("isOnPosition", false);
		}
	}
}
