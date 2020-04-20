using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : MonoBehaviour {
	public static bool playMusic = true;

	AudioSource music;
	private void Start() {
		music = GetComponent<AudioSource>();
	}


	void Update() {
		music.mute = !playMusic;
    }
}
