using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer{
	public class GameItemControllerScript : MonoBehaviour{

		private bool masterNear = false;
		private GameObject master;
		private bool isCatchUp = false;
		private Transform boxTransform;
		private Rigidbody2D rb;
		void Awake(){
			boxTransform = transform.parent.transform;
			rb = boxTransform.GetComponent<Rigidbody2D>(); 
		}
	 	void OnTriggerEnter2D(Collider2D collider){
			masterNear = true;
			master = collider.gameObject;
		}
		void OnTriggerExit2D(Collider2D collider){
			masterNear = false;
			master = collider.gameObject;
		}
		void Update(){
			if(Input.GetKeyDown( KeyCode.T )){
				if(!isCatchUp){
					if(masterNear){
						CatchUpItem();
					} 
				}else{ 
					DropDownItem();
				}
			}
		}
		private void CatchUpItem(){
			if(master != null){
				boxTransform.SetParent(master.transform);
				rb.simulated = false;
				boxTransform.GetComponent<BoxCollider2D>().enabled = false;
				boxTransform.localPosition  = new Vector3();
				isCatchUp = true;
			}
		}
		private void DropDownItem(){
			Debug.Log("drop");
			boxTransform.SetParent(null);
			rb.simulated = true;
			boxTransform.GetComponent<BoxCollider2D>().enabled = true;
			isCatchUp = false;
		}
	}
}
