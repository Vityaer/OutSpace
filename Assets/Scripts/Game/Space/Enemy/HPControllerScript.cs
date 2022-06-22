using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPControllerScript : MonoBehaviour{
	public delegate void DelDeath();
	public delegate void DelObserverHP(float hp);
	private DelDeath delsDeath;
	private DelObserverHP delsObserverHP;

	public void RegisterOnDeath(DelDeath d){
		delsDeath += d;
	}
	public void UnRegisterOnDeath(DelDeath d){
		delsDeath -= d;
	}
	private void Death(){
		if(delsDeath != null)
			delsDeath();	
	}
	public void RegisterOnObserverHP(DelObserverHP d){
		delsObserverHP += d;
	}
	public void UnRegisterOnObserverHP(DelObserverHP d){
		delsObserverHP -= d;
	}
	private void ChangeHP(float hp){
		if(delsObserverHP != null)
			delsObserverHP(hp);
	}
	[SerializeField] private float startHP = 10f; 
	[SerializeField] private float HP = 10f;
	[SerializeField] private float armor = 0f;
	void Awake(){
		HP = startHP; 
	}
	public void OnCreateEnemy(float _HP){
		startHP = _HP;
		HP = _HP;
	}
	public void GetDamage(float damage){
		damage = (damage > armor) ? damage - armor : 0f;
		HP = (HP > damage) ? HP - damage : 0f;  
		ChangeHP(HP);
		if(HP <= 0) Death();
	}
}
