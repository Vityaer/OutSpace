using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundScript : MonoBehaviour{
	private AudioSource audio;
	public AudioClip sound;
	void Awake(){
		audio = GetComponent<AudioSource>();
	}
	public void PlaySound(){
		PlaySound(sound);
	}  
	public void PlaySound(AudioClip clip){
		audio.PlayOneShot(clip);
	}
}
