using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour {
	public GameObject bulletPrefab;

	public GameObject bulletSpawnPos;

	public AudioClip clickSound, shootSound;

	public int bullets, maxBullets = 6;
	public Vector3 lookAtPos;

	public AnimationCurve shot;

	public bool canFire = true, isColliding = false;

	// Start is called before the first frame update
	void Start() {
		bullets = 6;
    }

    void LateUpdate() {
		//isColliding = false;
	}

	#region Animations
	public IEnumerator FireAnimation() {
		canFire = false;

		Vector3 originalPos = transform.localPosition;
		Vector3 newPos = originalPos - Vector3.forward * 0.45f;

		Quaternion originalRot = transform.localRotation;
		Quaternion newRot = originalRot * Quaternion.Euler(-45,0,0);

		float time = 0, speed = 0.05f;
		while (time < 1) {
			time += Time.deltaTime / speed;
			transform.localPosition = Vector3.Lerp(originalPos, newPos, time);
			transform.localRotation = Quaternion.Lerp(originalRot, newRot, time);

			yield return null;
		}

		time = 0; speed = 0.1f;
		while (time < 1) {
			time += Time.deltaTime / speed;
			transform.localPosition = Vector3.Lerp(newPos, originalPos, time);
			transform.localRotation = Quaternion.Lerp(newRot, originalRot, time);

			yield return null;
		}
		canFire = true;
	}
	public IEnumerator ReloadAnimation() {
		canFire = false;

		Vector3 originalPos = transform.localPosition;
		Vector3 newPos = originalPos - Vector3.up * 0.5f;

		Quaternion originalRot = transform.localRotation;
		Quaternion newRot = originalRot * Quaternion.Euler(0, 0, -45f);

		float time = 0, speed = 0.15f;
		while (time < 1) {
			time += Time.deltaTime / speed;
			transform.localPosition = Vector3.Lerp(originalPos, newPos, time);
			transform.localRotation = Quaternion.Lerp(originalRot, newRot, time);

			yield return null;
		}

		bullets = 6;

		time = 0; speed = 0.15f;
		while (time < 1) {
			time += Time.deltaTime / speed;
			transform.localPosition = Vector3.Lerp(newPos, originalPos, time);
			transform.localRotation = Quaternion.Lerp(newRot, originalRot, time);

			yield return null;
		}

		canFire = true;
	}
	#endregion

	public void FireGun() {
		if (!canFire || isColliding)
			return;

		if(bullets <= 0) {
			StartSound(clickSound);
			return;
		}

		StartSound(shootSound);
		bullets--;
		GameObject bullet = Instantiate(bulletPrefab);

		bullet.transform.position = bulletSpawnPos.transform.position;
		bullet.transform.LookAt(lookAtPos);// = Quaternion.LookRotation(- bulletSpawnPos.transform.position );

		StartCoroutine(FireAnimation());
	}

	public void StartSound(AudioClip clip) {

	}

	public void Reload() {
		if (!canFire)
			return;

		StartCoroutine(ReloadAnimation());
	}

	private void OnTriggerStay(Collider other) {
		isColliding = true;
	}
	private void OnTriggerExit(Collider other) {
		isColliding = false;
	}
}
