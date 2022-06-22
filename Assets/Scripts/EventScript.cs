using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Platformer{
	public class EventScript : MonoBehaviour{
		public UnityEvent gameEvent;
		public bool RepeatEvent = false;
			void Start(){
				GetComponent<CollisionTriggerScript>().RegisterOnCollision(EventCollision);
			}
		public void EventCollision(GameObject master){
			gameEvent.Invoke();
			if(!RepeatEvent){
				GetComponent<CollisionTriggerScript>().UnRegisterOnCollision(EventCollision);
			}
		}
	}
}
