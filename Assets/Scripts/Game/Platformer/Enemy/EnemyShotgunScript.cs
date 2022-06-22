using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer{
	public class EnemyShotgunScript : EnemyGunScript{
		public int countBullet;
		public override void CreateBullet(GameObject master, GameObject prefabBullet, int damage){
			for(int i=1; i <= countBullet; i++){
				dir = target.position - tr.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-1.5f, 1.5f));
				poolBullet.GetBullet(master, prefabBullet, rayPoint.position, tr.rotation, dir.normalized , damage);
			}
            GetComponent<AudioSource>()?.PlayOneShot(noiseShot);
		}
	}
}
