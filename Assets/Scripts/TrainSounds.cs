using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSounds : MonoBehaviour {
	public AudioClip clip;
	bool hasSpawned = false;
	public int distance = 5;

	void Update() {
		if (((int)World.instance.distanceTraveled % distance) == 0 && !hasSpawned) {
			hasSpawned = true;
			StartCoroutine(HandleSound(clip));

		} else if(((int)World.instance.distanceTraveled % distance) != 0) {
			hasSpawned = false;
		}
	}

	IEnumerator HandleSound(AudioClip clip) {
		GameObject g = new GameObject(clip.name);
		g.transform.position = transform.position;

		AudioSource a = g.AddComponent<AudioSource>();
		a.spatialize = true;
		a.spatialBlend = 1;
		
		a.minDistance = 30;

		a.pitch = Random.Range(0.9f, 1.1f);

		a.clip = clip;
		a.Play();

		//yield return new WaitUntil(() => !a.isPlaying);
		yield return null;
		//Destroy(g);
	}

}
