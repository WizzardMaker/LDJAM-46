using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour {
	public GameObject bulletPrefab;

	public GameObject bulletSpawnPos;
	public GameObject collisionPrevention;

	public AudioClip clickSound, shootSound;

	public int bullets, maxBullets = 6;
	public Vector3 lookAtPos;

	public bool isPlayer = false;

	public AnimationCurve shot;

	public bool canFire = true, isColliding = false;

	public Vector3 startPos, startRot;

	Vector3 originalPos;
	Quaternion originalRot;

	// Start is called before the first frame update
	void Start() {
		bullets = 6;
		originalPos = transform.localPosition;
		originalRot = transform.localRotation;

		if (collisionPrevention == null)
			return;
		startPos = collisionPrevention.transform.localPosition + transform.localPosition;
		startRot = collisionPrevention.transform.localRotation.eulerAngles;
	}

    void LateUpdate() {
		KeepColliderStationary();
		//isColliding = false;
	}

	void KeepColliderStationary() {
		if (collisionPrevention == null)
			return;

		collisionPrevention.transform.localRotation = Quaternion.Inverse(transform.localRotation);
		collisionPrevention.transform.position = transform.parent.TransformPoint(startPos);
	}

	#region Animations
	bool animRunning;

	public IEnumerator FireAnimation() {
		yield return new WaitWhile(()=>animRunning);
		animRunning = true;

		canFire = false;

		Vector3 newPos = originalPos - Vector3.forward * 0.45f;

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
			//collisionPrevention.transform.localRotation = Quaternion.Inverse(transform.localRotation);

			yield return null;
		}
		canFire = true;
		animRunning = false;
	}
	public IEnumerator ReloadAnimation() {
		//yield return new WaitWhile(() => animRunning);
		animRunning = true;
		canFire = false;

		Vector3 newPos = originalPos - Vector3.up * 0.5f;
		Quaternion newRot = originalRot * Quaternion.Euler(0, 0, -90f);

		float time = 0, speed = 0.15f;
		while (time < 1) {
			time += Time.deltaTime / speed;
			transform.localPosition = Vector3.Lerp(originalPos, newPos, time);
			transform.localRotation = Quaternion.Lerp(originalRot, newRot, time);
			//collisionPrevention.transform.localRotation = Quaternion.Inverse(transform.localRotation);

			yield return null;
		}

		bullets = 6;

		time = 0; speed = 0.15f;
		while (time < 1) {
			time += Time.deltaTime / speed;
			transform.localPosition = Vector3.Lerp(newPos, originalPos, time);
			transform.localRotation = Quaternion.Lerp(newRot, originalRot, time);
			//collisionPrevention.transform.localRotation = Quaternion.Inverse(transform.localRotation);

			yield return null;
		}

		canFire = true;
		//if(!isColliding)
		animRunning = false;
	}

	public IEnumerator CantFireAnimation() {
		yield return new WaitWhile(() => animRunning);

		if (isColliding == false)
			yield break;

		//canFire = false;

		animRunning = true;
		Quaternion originalRot = transform.localRotation;
		Quaternion newRot = originalRot * Quaternion.Euler(-60, 0,0);

		float time = 0, speed = 0.075f;
		while (time < 1) {
			time += Time.deltaTime / speed;
			transform.localRotation = Quaternion.Lerp(originalRot, newRot, time);
			animRunning = true;
			//collisionPrevention.transform.localRotation = Quaternion.Inverse( transform.localRotation);
			yield return null;
		}
		
		yield return new WaitUntil(() => isColliding == false);

		time = 0; speed = 0.075f;
		while (time < 1) {
			time += Time.deltaTime / speed;
			transform.localRotation = Quaternion.Lerp(newRot, originalRot, time);
			animRunning = true;
			//collisionPrevention.transform.localRotation = Quaternion.Inverse(transform.localRotation);
			yield return null;
		}

		collisionPrevention.transform.localRotation = Quaternion.Inverse(transform.localRotation);
		animRunning = false;
		canFire = true;
	}
	#endregion

	public bool FireGun(bool isPlayer) {
		if (PauseMenu.isPaused)
			return false;
		if (!canFire || isColliding)
			return false;

		if(bullets <= 0) {
			StartSound(clickSound);
			return false;
		}

		StartSound(shootSound);
		bullets--;
		GameObject bullet = Instantiate(bulletPrefab);

		bullet.GetComponent<Bullet>().isPlayerFired = isPlayer;

		bullet.transform.position = bulletSpawnPos.transform.position;
		bullet.transform.LookAt(lookAtPos);// = Quaternion.LookRotation(- bulletSpawnPos.transform.position );

		StartCoroutine(FireAnimation());
		return true;
	}

	public void StartSound(AudioClip clip) {
		StartCoroutine(HandleSound(clip));
	}

	IEnumerator HandleSound(AudioClip clip) {
		GameObject g = new GameObject(clip.name);
		g.transform.position = transform.position;

		AudioSource a = g.AddComponent<AudioSource>();
		if (!isPlayer) {
			a.spatialize = true;
			a.spatialBlend = 1;
		}
		a.minDistance = 30;

		a.pitch = Random.Range(0.8f, 1.2f);

		a.clip = clip;
		a.Play();

		yield return new WaitUntil(() => !a.isPlaying);

		Destroy(g);
	}

	public bool Reload() {
		if (PauseMenu.isPaused)
			return false;
		if (!canFire)
			return false;

		StartCoroutine(ReloadAnimation());
		return true;
	}

	private void OnTriggerStay(Collider other) {
		StartCoroutine(CantFireAnimation());
		isColliding = true;
	}
	private void OnTriggerExit(Collider other) {
		isColliding = false;
	}
}
