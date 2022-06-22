using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer{
	public class FireScript : MonoBehaviour{

		private ParticleSystem ps;
	    public List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
	    public List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
		private PlayerHPControllerScript playerHP;
		void Awake(){
			ps = GetComponent<ParticleSystem>();
		}
		void Start(){
			playerHP = GameObject.Find("Player").GetComponent<PlayerHPControllerScript>();
		}
	    // void OnParticleCollision(GameObject other){
	    //     if (other.CompareTag("Player")){
	    //     } 
	    // }
	    int numEnter = 0;
	    void OnParticleTrigger(){
	        numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
	        if(numEnter > 0)
		        playerHP.GetFireDamage(numEnter);
	    }
	}
}
