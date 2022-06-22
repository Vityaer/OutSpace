using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer;
public class GetUpItemScript : MonoBehaviour{
    void Awake(){
    	GetComponent<CollisionTriggerScript>().RegisterOnCollision(GetBonus);
    }
    public enum BonusType{
    	health,
    	shield,
    	ammo
    }
    public BonusType bonus;
    public int bonusPoint;
    public void GetBonus(GameObject master){
    	switch (bonus) {
    		case BonusType.health:
    			GetHealt(master);
    			break;
    		case BonusType.ammo:
    			GetAmmo(master);
    			break;	
    		case BonusType.shield:
    			GetArmor(master);
    			break;
    	}
    }
    private void GetHealt(GameObject master){
    	master.GetComponent<PlayerHPControllerScript>().AddHP(bonusPoint);
    	Death();
    }
    private void GetAmmo(GameObject master){
    	master.transform.Find("Gun")?.GetComponent<GunControllerScript>()?.AddAmmo(bonusPoint);
    	Death();
    }
    private void GetArmor(GameObject master){
    	master.GetComponent<PlayerHPControllerScript>().AddShield(bonusPoint);
    	Death();
    }


    private void Death(){
        GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<CollisionTriggerScript>().UnRegisterOnCollision(GetBonus);
        GetComponent<PlaySoundScript>()?.PlaySound();
		Destroy(gameObject, 1);
    }
}
