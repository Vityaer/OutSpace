using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Space{
	[System.Serializable]
	public struct LevelHelpPriority {
	    public float levelHP;
	    public int priority;
	 }
	public class EnemyBossControllerScript : EnemyControllerScript{
		public List<LevelHelpPriority> ListLevelHelp = new List<LevelHelpPriority>();
		private List<GameObject> enemiesHelp = new List<GameObject>();
		void Start(){
			GetComponent<HPControllerScript>()?.RegisterOnObserverHP(BossObserverHP);
		}
		public void BossObserverHP(float level){
			int priority = 0;
			foreach (LevelHelpPriority levelHelp in ListLevelHelp) {
				if(level <= levelHelp.levelHP){
					priority = levelHelp.priority; 
				}
			}
			DoOrderOnPriority(priority);
		}
		void DoOrderOnPriority(int priority){

		}
		void OnDestroy(){
			GetComponent<HPControllerScript>()?.UnRegisterOnObserverHP(BossObserverHP);
		}
	}
}
