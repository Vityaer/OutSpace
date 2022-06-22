using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Platformer{
	public class PlayerHPControllerScript : MonoBehaviour{
//Change HP		
		public delegate void DelChange(int HP);
		public DelChange dels;
		public void RegisterChangeHP(DelChange d){
			dels += d;
		}
		public void UnRegisterChangeHP(DelChange d){
			dels -= d;
		}
		private void MessageChangeHP(){
			if(dels != null)
				dels(HP);
		}
//Death
		public delegate void DelDeath();
		public DelDeath delsDeath;
		public void RegisterOnDeath(DelDeath d){
			delsDeath += d;
		}
		public void UnRegisterOnDeath(DelDeath d){
			delsDeath -= d;
		}
		private void MessageDeath(){
			if(delsDeath != null)
				delsDeath();
		}

		[SerializeField] private int HP = 100;
		public  int startHP = 100;
		private int shield = 0;
		public  int startShiled = 0;
//API 		
		public void GetDamage(int damage){
			if(shield >= damage){
				shield -= damage;
				damage = 0;
			}else{
				damage -= shield;
				shield = 0;
			}
			HP = (HP > damage) ? HP - damage : 0;
			if(HP == 0) DeathPlayer();	
			UpdateUI();
			MessageChangeHP();
		}
		public void AddHP(int bonus){
			HP     = (  (HP + bonus)   < 100) ? HP + bonus : 100;
			UpdateUI();
		}
		public void AddShield(int bonus){
			shield = ((shield + bonus) < 100) ? shield + bonus : 100;
			UpdateUI();
		}


		void DeathPlayer(){
			MessageDeath();
		}		
//UI
		private Slider hpStatus;
		public  Slider armorStatus;
		void Start(){
			hpStatus = GameObject.Find("HPSlider").GetComponent<Slider>();
			armorStatus = GameObject.Find("ArmorSlider").GetComponent<Slider>();
			UpdateUI();
		}
		void UpdateUI(){
			hpStatus.value = HP;
			armorStatus.value = shield;
		}
//Fire damage
		private int fireDamage = 1;
		public int powerFire = 0;
		private float timeRest = 0.3f;
		private Coroutine coroutineFireRest;
		public void GetFireDamage(int fire){
			powerFire += fire;
			if(coroutineFireRest == null){
				coroutineFireRest = StartCoroutine(IRestFire()); 
			}
			if(powerFire > 100)
				GetDamage(fireDamage); 
		}
		private bool fireRestWork = false;
		IEnumerator IRestFire(){
			fireRestWork = true;
			while(fireRestWork){
				powerFire -= 2; 
				if(powerFire > 18){
					powerFire -= 20;
					GetDamage(fireDamage * (powerFire % 5));
				}
				if(powerFire <= 0) {
					powerFire = 0;
					fireRestWork = false;
				}
				yield return new WaitForSeconds(timeRest);
			}
			coroutineFireRest = null;
		} 
	}
}
