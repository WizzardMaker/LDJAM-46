using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour {
	public GameObject bulletPrefab;

	public GameObject bulletSpawnPos;

	public AudioClip clickSound, shootSound;

	public int bullets, maxBullets = 6;
	public Vector3 lookAtPos;

	// Start is called before the first frame update
	void Start() {
		bullets = 6;
    }

    // Update is called once per frame
    void Update() {

    }

	public void FireGun() {
		if(bullets <= 0) {
			StartSound(clickSound);
			return;
		}

		StartSound(shootSound);
		bullets--;
		GameObject bullet = Instantiate(bulletPrefab);

		bullet.transform.position = bulletSpawnPos.transform.position;
		bullet.transform.rotation = Quaternion.LookRotation(lookAtPos- bulletSpawnPos.transform.position );
	}

	public void StartSound(AudioClip clip) {

	}

	public void Reload() {
		bullets = 6;
	}
}
