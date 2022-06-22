using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer;
	public class FireGuardControllerScript : MonoBehaviour{
		private EnemyGunScript gunScript;
		ParticleSystem myParticleSystem;
	    ParticleSystem.EmissionModule emissionModule;
	    private bool isAttack = false;
        public bool IsAttack{get => isAttack;}
        private Transform player;
        private Transform tr;
        public float radiusAttack = 5f;
        void Awake(){
            tr = GetComponent<Transform>();
            player = GameObject.Find("Player").GetComponent<Transform>();
        }
		void Start(){
	        // Get the system and the emission module.
	        myParticleSystem = transform.Find("Gun/Fire").GetComponent<ParticleSystem>();
	        emissionModule = myParticleSystem.emission;
	        AttackMode(false);
    	}

    	[ContextMenu("on_off Attack")]
    	public void OnOffAttack(){
    		AttackMode(!isAttack);
    	}
    	private void AttackMode(bool flagAttack){
    		isAttack = flagAttack;
    		SetRate(isAttack ? 100f : 0f);
    	}
    	void SetRate(float rate){
        	emissionModule.rateOverTime = rate;
    	}
	}

