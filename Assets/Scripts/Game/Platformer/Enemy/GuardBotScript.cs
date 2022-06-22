using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer{
	public class GuardBotScript : EnemyControllerScript{

		Coroutine coroutineLoopAttack;
		public GameObject prefab;
		Transform positionStartAttack;

		protected void Start(){
			Debug.Log("yeee");
			positionStartAttack = transform.Find("Eyes").transform;
			coroutineLoopAttack = StartCoroutine(IGetOutGranade());
		}

		public float timeRest = 5f;
		public bool attack = true;
		[Range(0f, 90f)]
		public float angle;
		[Range(3f, 10f)]
		public float power; 
		Vector2 dirGranade = new Vector2(-1f, 1f);
		IEnumerator IGetOutGranade(){
			float timer = timeRest;
			while(attack){
				timer -= Time.deltaTime;
				if(timer <= 0f){
					timer = timeRest;
					GameObject bullet = Instantiate(prefab, positionStartAttack.position, transform.rotation);
					Vector3 playerPosition = GameObject.Find("Player").GetComponent<Transform>().position;
					float distX = Mathf.Abs(GetComponent<Transform>().position.x - playerPosition.x); 
					float distY = Mathf.Abs(GetComponent<Transform>().position.y - playerPosition.y); 
					power = Mathf.Sqrt((9.8f * distX)/(2 * Mathf.Sin(2 * angle * Mathf.PI/180)*Mathf.Sin(2 * angle * Mathf.PI/180))); 
					dirGranade.y = power * Mathf.Sin(angle * Mathf.PI/180);
					dirGranade.x = (-1)* power * Mathf.Cos(angle * Mathf.PI/180);
					bullet.GetComponent<Rigidbody2D>().velocity = dirGranade;
				}
				yield return null;
			}
		}
	}
}