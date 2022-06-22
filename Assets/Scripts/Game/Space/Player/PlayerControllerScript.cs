using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour{
	private Rigidbody2D           rb; // компонент для передвижения с помощью физики (системный)
    private Transform             tr; // компонент для позиции, вращения, размеров объекта
    private AttackBehaviourScript attackBehaviour; // компонент 
    private Player                playerNet;
    private SpriteRenderer        spriteRender;
    private Vector3               mouseLocalPosition;
    private Camera                mainCamera;
    void Awake(){
        GetComponents();
    }
    void GetComponents(){
        mainCamera      = Camera.main;
        rb              = GetComponent<Rigidbody2D>();
        tr              = GetComponent<Transform>();
        attackBehaviour = GetComponent<AttackBehaviourScript>();
        playerNet       = GetComponent<Player>();
        spriteRender    = GetComponent<SpriteRenderer>();
        OnStartLocalPlayer();
    }

    Vector3 positionPlayer = new Vector3();
    void Update(){
    	mouseLocalPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        positionPlayer.x   = mouseLocalPosition.x;
        positionPlayer.y   = mouseLocalPosition.y;
    	tr.position        = positionPlayer;
    	if(Input.GetMouseButton(0)){
    		Attack();
        }
    }
//Attack    
    void Attack(){
        attackBehaviour.Attack();
    }
}
