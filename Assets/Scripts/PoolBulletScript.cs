using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pool{
	private GameObject master;
	public  GameObject Master{get => master;}
	private List<Bullet> bullets = new List<Bullet>();
	public  List<Bullet> Bullets{get => bullets;}
	public Pool(GameObject master){
		this.master = master;
	}
	public void Add(Bullet bullet){
		bullets.Add(bullet);
	}
}

public class PoolBulletScript : MonoBehaviour{
	private List<Pool> listPool = new List<Pool>();
    private Bullet bulletDone;
    private GameObject bullet;
    private Transform tr;
    void Awake(){
    	tr = GetComponent<Transform>();
    }
	public Bullet GetBullet(GameObject master, GameObject prefab, Vector3 position, Quaternion rotation, Vector2 direction, int damage, bool evil = false){
		//Find pool
		Pool currentPool = null;
		bulletDone = null;
		for(int i=0; i<listPool.Count; i++){
			if(listPool[i].Master == master){
				currentPool = listPool[i];
			}
		}
		if(currentPool == null){
			currentPool = new Pool(master);
			listPool.Add(currentPool);
		}

		//Find bullet in pool
		bool find = false;
		for(int i=0; i<currentPool.Bullets.Count; i++){
			if(currentPool.Bullets[i].GetDone()){
				find = true;
				bulletDone = currentPool.Bullets[i];
				break;
			}
		}
		if(!find){
            bullet = Instantiate(prefab, position, rotation, tr);
			bulletDone = bullet.GetComponent<BulletFlyBehaviourScript>().Active(master, position, rotation, direction, damage, evil);
            currentPool.Add(bulletDone);
		}else{
			bulletDone.Script.Active(position,direction, rotation, damage);		
		}
		return bulletDone;	
	}
	public void Clear(GameObject master){
		Pool currentPool = null;
		for(int i=0; i<listPool.Count; i++){
			if(listPool[i].Master == master){
				currentPool = listPool[i];
			}
		}
		if(currentPool != null){
			for(int i=0; i<currentPool.Bullets.Count; i++){
				Destroy(currentPool.Bullets[i].Script.gameObject);
			}
			listPool.Remove(currentPool);
		}
	}
}
