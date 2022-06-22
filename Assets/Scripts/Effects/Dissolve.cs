using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour{
	Material material;
	bool isDissolving = false;
	public List<Dissolve> listDissole = new List<Dissolve>();
	float fade = 1f;
    void Start(){
    	material = GetComponent<SpriteRenderer>().material;
    	
    }
    public void StartDissolve(){
    	if(!isDissolving){
    		isDissolving = true;
			StartCoroutine( IDissolve() );
    		foreach (Dissolve diss in listDissole){
    			diss.StartDissolve();
    		}
    	}
    }

    IEnumerator IDissolve(){
		while(fade >= 0f){
			fade -= Time.deltaTime;
			if(fade <= 0f){
				fade = 0f;
			}
			material.SetFloat("_Fade", fade);
			if(fade == 0f){
				Destroy(gameObject);
			}
			yield return null;
		}
    }
}
