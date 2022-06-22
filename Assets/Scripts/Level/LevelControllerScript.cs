using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControllerScript : MonoBehaviour{
//Singleton	
	private static LevelControllerScript instance = null;
	public static LevelControllerScript Instance { get { return instance; } }
	void Awake(){
		if(instance == null) instance = this;
	}
// Corenrs	
	[Header("Corners")]
	public Transform cornerTopLeft;
	public Transform cornerBottomRight;
}
