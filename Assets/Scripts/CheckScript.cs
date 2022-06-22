using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CheckScript : NetworkBehaviour{
    public bool isMyClient = false;
    public override void OnStartLocalPlayer(){
		isMyClient = true; 
    	base.OnStartLocalPlayer();
    }
}
