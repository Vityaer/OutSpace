using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;
using Platformer;
public class Bullet{
		private GameObject master;
		public  GameObject Master{get => master;}
		private int damage;
		public  int Damage{get => damage; set => damage = value;}
		private bool isPlayerShot = false;
		public bool IsPlayerShot{get => isPlayerShot; set => isPlayerShot = value;} 
		private BulletFlyBehaviourScript scriptController;
		public BulletFlyBehaviourScript Script{get => scriptController;}
		private bool move = true;
		public bool Move{get => move; set => move = value;}
		public bool GetDone(){return (!move);}

		public Bullet(GameObject master, BulletFlyBehaviourScript script, int damage){
			this.damage = damage;
			scriptController = script;
		}
		public void Disable(bool move){
			this.move = move;
		}
}
public class BulletFlyBehaviourScript : MonoBehaviour{
	private Rigidbody2D rb;
	private float timeFly  = 10f;
	public float speed;
	private Transform tr;
	private Bullet bullet;
    private TimerScript Timer;
    private GameTimer timer = null;
	void Awake(){
		GetComponentsObject();
	}
	void GetComponentsObject(){
		rb = GetComponent<Rigidbody2D>();
		tr = GetComponent<Transform>();
		Timer = HelpFuction.TimerScript.Timer;
	}
	public Bullet Active(GameObject whoShooter, Vector3 position, Quaternion rotation, Vector2 direction, int damage, bool evil){
		if(bullet == null){
			bullet = new Bullet(whoShooter, this, damage);
			bullet.IsPlayerShot = evil;
		}
		tr.position = position;
		tr.rotation = rotation;
		rb.velocity = direction * speed;
		timer = Timer.StartTimer(timeFly, Disable);
		bullet.Move = true;
		return bullet;
	}
	public void Active(Vector3 position, Vector2 direction, Quaternion rotation, int damage){
		tr.position = position;
		tr.rotation = rotation;
		rb.velocity = direction * speed;
		bullet.Move = true;
		timer = Timer.StartTimer(timeFly, Disable);
		bullet.Damage = damage;
	}
	public void Disable(){
		if(timer != null){
			Timer.StopTimer(timer);
		}
		bullet.Move = false;
		rb.velocity = new Vector2(0f, 0f);
		tr.position = new Vector3(1000f, 1000f, 1000f);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(bullet.Move){
			if(bullet.IsPlayerShot){
		        if(other.gameObject.tag == "Enemy"){
		        	other.gameObject.GetComponent<HPControllerScript>()?.GetDamage(bullet.Damage);
		        	Disable();
		        } 
			}else{
				if(other.gameObject.tag == "Player"){
					other.gameObject.GetComponent<PlayerHPControllerScript>()?.GetDamage(bullet.Damage);
		        	Disable();
		        }
			}
			if(other.gameObject.CompareTag("Ground")){
				Disable();
			}
	    }
	}
}
