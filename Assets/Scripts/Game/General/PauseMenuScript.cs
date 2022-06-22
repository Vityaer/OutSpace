using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenuScript : MonoBehaviour{
    bool isOpen = false;
    Canvas mainPanel;
    void Awake(){
    	mainPanel = GetComponent<Canvas>();
    }
    void Update(){
    	    if(Input.GetKeyDown( KeyCode.Escape )){
    	    	if(isOpen == false){ OpenMenu(); }else{ CloseMenu(); } 
    	    }
    }
    private void OpenMenu(){
    	Time.timeScale = 0f;
    	mainPanel.enabled = true;
    	isOpen = true;
    }

    public void CloseMenu(){
    	Time.timeScale = 1f;
    	mainPanel.enabled = false;
    	isOpen = false;
    }
}
