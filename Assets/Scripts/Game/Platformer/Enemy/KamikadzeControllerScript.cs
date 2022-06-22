using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer{
	public class KamikadzeControllerScript : EnemyControllerScript{
		protected override void Start(){
			base.Start();
			transform.Find("AttackPoint").GetComponent<DangerControllerScript>()?.Register(Boom);
		}
		public float power = 40f;
		public int damage = 5;
		private bool isboom = false;
		public bool isFloor = true;
		public void Boom(GameObject player){
			if(!isboom){
				isboom = true;	
				Vector2 dir = new Vector2(player.transform.position.x - tr.position.x, player.transform.position.y - tr.position.y);
				dir.Normalize();
				dir = dir * power * damage;
				player.GetComponent<PlayerPlatformerScript>()?.SetStun();
				player.GetComponent<PlayerHPControllerScript>()?.GetDamage(damage);
				player.GetComponent<Rigidbody2D>().velocity = dir;
				transform.Find("AttackPoint").GetComponent<DangerControllerScript>().UnRegister(Boom);
				Destroy(gameObject);
			}
		}
		[ContextMenu("Change Gravity")]
		public void ChangeGravity(){
			rb.gravityScale = -rb.gravityScale; 
			Vector3 theScale = transform.localScale;
            theScale.y *= -1;
            transform.localScale = theScale;
            isFloor = !isFloor;
		}
		public void ChangeGravity(float scale){
			if(scale != rb.gravityScale){
				ChangeGravity();
			}
		}
		public override void Death(){
			Destroy(gameObject);
		}	
	}
}