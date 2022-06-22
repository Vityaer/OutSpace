using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAttackScript : MonoBehaviour{

	private Transform tr;
	public float timeRest = 3f;
	private Transform pointGun;
	public float damage = 1f;
	private float timeBeforeAttack;
	private bool isAttack;
	// private List<GameObject> poolBullet = new List<GameObject>();
	public GameObject prefabBullet;


	void Awake(){
    	GetComponents();
    }
    void GetComponents(){
		tr = GetComponent<Transform>();
		pointGun = transform.Find("PointGun").GetComponent<Transform>();
    }
    public void StartAttack(){
    	timeBeforeAttack = timeRest;
    }

    void Update(){
    	if(timeBeforeAttack > 0f){
    		timeBeforeAttack -= Time.deltaTime; 
    	}else{
    		CmdAttack(0);
    	}
    }
    private void Attack(int weapon){
    	// GetBullet();
    	timeBeforeAttack = timeRest;
    }
 //    void GetBullet(){
 //    		GameObject bulletDone = null;
	// 		bool find = false;
	// 		for(int i=0; i < poolBullet.Count; i++){
	// 			if(!poolBullet[i].GetComponent<BulletFlyBehaviourScript>().GetDone()){
	// 				find = true;
	// 				bulletDone = poolBullet[i];
	// 			}
	// 		}
	// 		if(!find){
	//             bulletDone = Instantiate(prefabBullet, pointGun.position, Quaternion.identity);
	//             poolBullet.Add(bulletDone);
	// 		}
	// 		// bulletDone.GetComponent<BulletFlyBehaviourScript>().Active("Enemy" ,pointGun.position,new Vector2(0, -1f), damage);
	// }
	public void DestroyBullets(){
	// 	for(int i = 0; i < poolBullet.Count; i++){
	// 		Destroy(poolBullet[i]);
	// 	}
	}
//Attack
    void CmdAttack(int weapon){
    	Attack(0);
    }
}
