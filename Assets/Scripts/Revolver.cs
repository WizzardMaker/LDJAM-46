using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour {
	public GameObject bulletPrefab;

	public GameObject bulletSpawnPos;

	public AudioClip clickSound, shootSound;

	public int bullets, maxBullets = 6;
	Vector3 lookAtPos;

	// Start is called before the first frame update
	void Start() {
		bullets = 6;
    }

    // Update is called once per frame
    void Update() {
		if (Input.GetMouseButtonDown(0)) {
			FireGun();
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			Reload();
		}

		lookAtPos = bulletSpawnPos.transform.forward + bulletSpawnPos.transform.position;
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1));
		if (Physics.Raycast(ray, out RaycastHit info, Mathf.Infinity, LayerMask.GetMask("Default"))) {
			lookAtPos = info.point;
		}

		//transform.LookAt(lookAtPos);
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
