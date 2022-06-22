using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;
namespace Platformer{
	public class FlyBotScript : EnemyControllerScript{
		private EnemyGunScript gun;
		private TimerScript Timer;
		void Start(){
			base.Start();
			gun = transform.Find("Gun").GetComponent<EnemyGunScript>();
			gun.Register(PlayerInAttackArea, radiusAttack);
			Timer = HelpFuction.TimerScript.Timer;
			moveScript = GetComponent<FlyBotMovementScript>();
			currentCountBulletInAmmo = countBulletInAmmo;
		}
		public GameObject prefabBullet;
		public float timeForReloadAmmo = 3f;
		public float timeBetweenShot = 0.2f;
		public int countBulletInAmmo;
		private bool isDoneShot = true;
		public int currentCountBulletInAmmo;
		public bool reload = false;
		private FlyBotMovementScript moveScript;
		
		public void PlayerInAttackArea(){
			if(currentCountBulletInAmmo > 0){
				if(isDoneShot){
					gun.CreateBullet(gameObject, prefabBullet, 3);
					currentCountBulletInAmmo -= 1;
					isDoneShot = false;
					Timer.StartTimer(timeBetweenShot, NextBulletDone);
				}
			}else{
				if(!reload){
					ChangeBehaviour(CurrentBehaviour.Safe);
					reload = true;
					Timer.StartTimer(timeForReloadAmmo, ReloadAmmoDone);
				}
			}
		}
		public void NextBulletDone(){
			isDoneShot = true;
		}
		public void ReloadAmmoDone(){
			reload = false;
			currentCountBulletInAmmo = countBulletInAmmo;
			ChangeBehaviour(CurrentBehaviour.Follow);
		}
		public override void Death(){
			base.Death();
			gun.UnRegister(PlayerInAttackArea);
		}

	}
}
