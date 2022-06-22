using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy{
    [SerializeField]
    private string name;
    public string Name{ get => name;}
    [SerializeField]
    private float hp;
    public float HP{ get => hp;}
    [SerializeField]
    private float speed;
    public float Speed{ get => speed;}

    public Enemy(string _name, float _hp, float _speed){
    	name = _name;
    	hp = _hp;
    	speed = _speed;
    }
}
[System.Serializable]
public struct PrefabEnemy{
	public string Name;
	public GameObject Prefab;
}

namespace Space{
	public class EnemiesCotrollerScript : MonoBehaviour{
		private static EnemiesCotrollerScript instance = null;
		public static EnemiesCotrollerScript Instance { get { return instance; } }
		public List<PrefabEnemy> prefabEnemies = new List<PrefabEnemy>();
		public List<Enemy> listLevelEnemies = new List<Enemy>();
		public int curLevel = 0;
		void Awake(){
			if(instance == null){
				instance = this;
			}else{
				Destroy(gameObject);
			}
			LoadInfoLevelScript.ReadEnemiesLevel(listLevelEnemies, curLevel);
			StartLevel();
		}
		public int num;
		public void CmdCreateNewEnemy(){
			if(num < listLevelEnemies.Count){
				CreateEnemy(listLevelEnemies[num]);
				num++;
			}
		}
		public void StartLevel(){
			CmdCreateNewEnemy();
		}
		private void CreateEnemy(Enemy enemyCreating){
			GameObject prefab = FindPrefab(enemyCreating.Name);
			GameObject enemyCreated = Instantiate(prefab, transform.position, Quaternion.identity);
			enemyCreated.GetComponent<EnemyControllerScript>().OnEnemyCreated(enemyCreating);
		}
		private GameObject FindPrefab(string name){
			GameObject prefab = null;
			foreach(PrefabEnemy curEnemyPrefab in prefabEnemies){
				if(curEnemyPrefab.Name == name){
					prefab = curEnemyPrefab.Prefab;
					break;
				}
			}
			return prefab;	
		}
	}
}
