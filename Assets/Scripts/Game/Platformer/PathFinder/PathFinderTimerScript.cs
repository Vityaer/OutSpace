using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinder;
namespace Platformer{
	public class PathFinderTimerScript : MonoBehaviour{
		public delegate void Del();
		public Del dels;
		public void Register(Del d){
			dels += d;
		}
		public void UnRegister(Del d){
			dels -= d;
		}
		private void DoDelagate(){
			if(dels != null)
				dels();	
		}
		Coroutine coroutineTimer;
		private Transform player;
		Vector2 previousPosition;
	    void Start(){
	    	player = GameObject.Find("Player").GetComponent<Transform>();
	    	previousPosition = player.position;
	    	PlayTimer();
	    }
	    void PlayTimer(){
	        if(coroutineTimer != null){
	        	StopCoroutine(coroutineTimer);
	        	coroutineTimer = null;
	        }
	        isCalculate = true;
	        coroutineTimer = StartCoroutine(ITimer());

	    }
	    public float restTime;
	    public bool isCalculate; 
	    IEnumerator ITimer(){
	    	float timer = restTime;
	    	while(isCalculate){
				timer -= Time.deltaTime;
				if(timer < 0f){
					timer = restTime;
					if(Vector2.Distance(player.position, previousPosition) > 1.5f){
						DoDelagate();
						previousPosition = player.position;
					}

				}    		
				yield return null;
	    	}
	    }

	}
}
