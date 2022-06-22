using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelpFuction;
using UnityEngine.UI;
namespace Platformer{
    public class GunControllerScript : MonoBehaviour{
        Camera mainCamera;
        Vector3 mouseLocalPosition;
        Transform tr;
        private bool isFacingRight = false;
        private PoolBulletScript poolBullet;
        private PlayerPlatformerScript playerController;
        Vector2 direction = new Vector2();
        
        [Header("Clip")]
        public GameObject prefabBullet;
        public int damage = 2; 
        public int capacityClip = 8;
        public float timeReload = 4f;
        private int currentBulletInClip = 0;
        private int reserveAmmo = 12;
        private Text textCurrentBulletInClip;
        private Text textReserveAmmo;
        private bool refreshingAmmo = false;

        public AudioClip noiseShot;
        public AudioClip noiseRefreshAmmo;
        private PlaySoundScript audio;

        private TimerScript Timer;
        void Awake(){
            audio                    = GetComponent<PlaySoundScript>();
            poolBullet               = GameObject.Find("PlayerPoolBullet").GetComponent<PoolBulletScript>();
            playerController         = transform.parent.GetComponent<PlayerPlatformerScript>(); 
            textReserveAmmo          = GameObject.Find("ReserveBullet").GetComponent<Text>(); 
            textCurrentBulletInClip  = GameObject.Find("CountBullet").GetComponent<Text>(); 
            Timer                    = HelpFuction.TimerScript.Timer;
            ReloadAmmoClip();
        }
        void Start(){
        	tr = GetComponent<Transform>();
        	mainCamera = Camera.main;
        }
        void Update(){
        	mouseLocalPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        	direction.x = mouseLocalPosition.x - tr.position.x;
            if(((direction.x > 0.5f) && !isFacingRight) || ((direction.x < -0.5f) && isFacingRight)){
                isFacingRight = !isFacingRight;
                playerController.Flip(isFacingRight);
            }
        	direction.y = mouseLocalPosition.y - tr.position.y;
            if(Mathf.Abs(direction.x) > 1f){
                tr.rotation = Quaternion.Euler(0, 0, Mathf.Atan(direction.y/direction.x)*(180/Mathf.PI));
            }
            if(Input.GetKeyDown( KeyCode.R )){
                RefreshAmmo();
            }   
            if(Input.GetMouseButtonDown(0)){
                Attack();
            }  
        }
        void Attack(){
            if(currentBulletInClip > 0){
                poolBullet.GetBullet(gameObject, prefabBullet, tr.position, tr.rotation, direction.normalized, damage, evil: true);
                audio.PlaySound(noiseShot);
                currentBulletInClip -= 1;
                UpdateUI();
                if((currentBulletInClip == 0) && (reserveAmmo > 0)){
                    Timer.StartTimer(timeReload, RefreshAmmo);
                } 
            }

        }
        void ReloadAmmoClip(){
            currentBulletInClip = (reserveAmmo >= capacityClip) ? capacityClip : reserveAmmo;
            reserveAmmo -= currentBulletInClip;
            refreshingAmmo = false;
            UpdateUI();
        }
        void UpdateUI(){
            textReserveAmmo.text = reserveAmmo.ToString();
            textCurrentBulletInClip.text = currentBulletInClip.ToString();
        }
        public void AddAmmo(int bonus){
            if((currentBulletInClip == 0) &&(reserveAmmo == 0)){
                Timer.StartTimer(timeReload, ReloadAmmoClip);
            }
            reserveAmmo += bonus;
            UpdateUI();
        }
        void RefreshAmmo(){
            if(reserveAmmo > 0){
                if(!refreshingAmmo){
                    refreshingAmmo = true;
                    audio.PlaySound(noiseRefreshAmmo);
                    reserveAmmo += currentBulletInClip; 
                    currentBulletInClip = 0;
                    Timer.StartTimer(timeReload, ReloadAmmoClip);
                    UpdateUI();
                }
            }
        }
    }
}
