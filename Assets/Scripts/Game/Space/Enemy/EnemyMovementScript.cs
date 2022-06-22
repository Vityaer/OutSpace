using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyMovementScript : MonoBehaviour{
	
    public enum TypeMove{
        RandomPoints,
        horizontal,
        Vertical,
        Kamikadze,
        Task,
        Fear,
        ProtectObject
    }
    public TypeMove typeMove;
    private TypeMove startTypeMove; 
	private Rigidbody2D rb;
	private Transform tr;
	public float speed;
	public bool isMove = false;
	private Vector2 startPosition;
	public Vector2 targetPosition = new Vector2(0, 4);
    private LevelControllerScript LevelController;
       
    void Awake(){
        startTypeMove = typeMove;
    	GetComponents();
    }
    void GetComponents(){
		rb = GetComponent<Rigidbody2D>();
		tr = GetComponent<Transform>();

    }
    void Start(){
        LevelController = LevelControllerScript.Instance;
        GetLimits();
    }
    public void OnCreateEnemy(float _speed){
        speed = _speed;
    }
    public void StartMovement(){
        startPosition = tr.position; 
        isMove = true;
    }

    void Update(){
        if(isMove){
            if(Vector3.Distance(targetPosition, tr.position) > 0.5f){
                Vector2 vec2 = targetPosition - startPosition;
                vec2.Normalize();
                rb.velocity = vec2 * speed * Time.deltaTime * 10;
            }else{
                GetNewTarget();
            }
        }
    }

// Limits and target
    [Header("Limits")]
    private Vector3 cornerTopLeft;
    private Vector3 cornerBottomRight;
    public float peaceTerritory = 1f; 
    void GetLimits(){
        // Vector3 scale = tr.localScale/2;
        Vector3 scale = new Vector3(1, 1, 1)/2;
        scale.y = -scale.y;
        cornerTopLeft     = LevelController.cornerTopLeft.position;
        cornerTopLeft     += scale;

        cornerBottomRight   = LevelController.cornerBottomRight.position;
        cornerBottomRight.y = cornerBottomRight.y + (cornerTopLeft.y - cornerBottomRight.y) * (1 - peaceTerritory);  
        cornerBottomRight -= scale;
    }
    void GetNewTarget(){
        startPosition = tr.position;
        Vector2 result = new Vector2(0, 0);
        switch(typeMove){
            case TypeMove.RandomPoints:
                result.x = Random.Range(cornerTopLeft.x, cornerBottomRight.x);
                result.y = Random.Range(cornerBottomRight.y, cornerTopLeft.y);
                break;
            case TypeMove.horizontal:
                result.x = Random.Range(cornerTopLeft.x, cornerBottomRight.x);
                result.y = tr.position.y;
                break;
            case TypeMove.Vertical:
                result.x = tr.position.x;
                result.y = Random.Range(cornerBottomRight.y, cornerTopLeft.y);
                break;
            case TypeMove.Kamikadze:
                result = GetPosNearPlayer();
                break;
            case TypeMove.Task:
                result.x = taskPoint.x;
                result.y = taskPoint.y;
                break;
            case TypeMove.Fear:
                if(tr.position.y > dangerPoint.y){
                    if(tr.position.x > dangerPoint.x){
                        result.x = Mathf.Min(tr.position.x + Random.Range(1f, cornerBottomRight.x - tr.position.x), cornerBottomRight.x);
                    }else{
                        result.x = Mathf.Max(tr.position.x - Random.Range(1f, tr.position.x - cornerTopLeft.x), cornerTopLeft.x);
                    }
                    result.y = Mathf.Min(tr.position.y + Random.Range(1f, cornerTopLeft.y - tr.position.y), cornerTopLeft.y);
                }else{
                    result = targetPosition;
                }
                typeMove = startTypeMove;
                break;
            case TypeMove.ProtectObject:
                result.x = objectProtect.position.x;
                result.y = objectProtect.position.y + Random.Range(1.5f, 2.5f);
                break;                    
        }
        targetPosition = result; 
    }
    Vector2 GetPosNearPlayer(){
        Vector2 result = new Vector2();
        return result;
    }

    private Vector2 taskPoint;
    public void Task(Vector3 taskPoint){
        this.taskPoint = new Vector2(taskPoint.x, taskPoint.y);
        typeMove = TypeMove.Task;
        GetNewTarget();
    }


    private Vector2 dangerPoint;
    public void FearPoint(Vector2 dangerPoint){
        typeMove = TypeMove.Fear;
        this.dangerPoint = dangerPoint;
        GetNewTarget();
        typeMove = startTypeMove;

    }

    private Transform objectProtect;
    public void ProtectThis(GameObject objectProtect){
        this.objectProtect = objectProtect.transform;
        typeMove = TypeMove.ProtectObject;
        GetNewTarget();
    }

}
