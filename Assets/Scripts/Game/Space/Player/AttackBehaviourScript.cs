using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackBehaviourScript : MonoBehaviour{

	private Transform tr;
	public GameObject prefabBullet;
	public float restTime;
	public bool isShotDone = true;
	public float damage = 1f;
	void Awake(){
		tr = transform.Find("PointGun").GetComponent<Transform>();
	}
	public void Attack(int weapon){
		if(isShotDone){
			isShotDone = false;
            StartCoroutine(IRestAttack(restTime));
		}
	}
	
	IEnumerator IRestAttack(float rest){
		float curTime = rest; 
		while(curTime > 0f){
			curTime -= Time.deltaTime;
			yield return null;
		}
		isShotDone = true;
	}

}
