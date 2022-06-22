using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Space{
	public class EnemyControllerScript : MonoBehaviour{

		
		private Transform tr;
		public EnemyMovementScript movementBehaviour;
		public EnemyAttackScript attackBehaviour;
		public HPControllerScript HPController;

		void Awake(){
	    	GetComponents();    
		}
		void GetComponents(){
			tr = GetComponent<Transform>();
			HPController = GetComponent<HPControllerScript>();
		}
	    void Start(){
	    	movementBehaviour.StartMovement();
	    	attackBehaviour.StartAttack();
	    }
	    
	    public void Death(){
			EnemiesCotrollerScript.Instance.CmdCreateNewEnemy();
	    	if(attackBehaviour) attackBehaviour.DestroyBullets();
			Destroy(gameObject);
	    }
	    public void OnEnemyCreated(Enemy enemyCreated){
	    	HPController.OnCreateEnemy(enemyCreated.HP);
	    	movementBehaviour.OnCreateEnemy(enemyCreated.Speed);
	    }
	}
}
