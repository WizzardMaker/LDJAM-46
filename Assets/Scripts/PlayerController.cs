using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public static PlayerController instance;

	public float health = 20;

	[HideInInspector]
	public CharacterController c;

	public GameObject fuelObject;

	public Revolver revolver;

	public float speed, sprintMod = 2;

	public bool transportsFuel = false;

	public enum HitSide
	{
		Left,
		Right,
		Down,
		Up,
	}

	public GameObject[] hitSides;
	public GameObject hitMarker;


	// Start is called before the first frame update
	void Start() {
		instance = this;
		c = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate() {
		Vector3 movement = new Vector3();
		
		movement += transform.right * Input.GetAxis("Horizontal");
		movement += transform.forward * Input.GetAxis("Vertical");

		c.SimpleMove(movement * speed * (Input.GetKey(KeyCode.LeftShift) ? sprintMod:1));

		fuelObject.SetActive(transportsFuel);
	}

	private void Update() {
		if (PauseMenu.isPaused)
			return;

		if (Input.GetMouseButtonDown(0)) {
			if(revolver.FireGun(true))
				AmmoUI.instance.Fire();
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			if (revolver.Reload())
				AmmoUI.instance.Reload();
		}

		c.height = Input.GetKey(KeyCode.LeftControl) ? 1f : 2;

		health += Time.deltaTime * 1f;
		health = Mathf.Clamp(health, 0, 20);

		revolver.lookAtPos = revolver.bulletSpawnPos.transform.forward + revolver.bulletSpawnPos.transform.position;
		//Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1));
		//if (Physics.Raycast(ray, out RaycastHit info, Mathf.Infinity, LayerMask.GetMask("Default"))) {
		//	revolver.lookAtPos = info.point;
		//}
		//transform.LookAt(lookAtPos);
	}

	public void BulletHit(Vector3 hitPos) {
		int leftRight, forwardBack;

		Vector3 localHitPos = transform.InverseTransformPoint(hitPos);

		Vector3 dir = localHitPos;
		leftRight = Mathf.CeilToInt(Mathf.Clamp(dir.x, 0, 1));
		forwardBack = Mathf.CeilToInt(Mathf.Clamp(dir.z,0,1));

		if (dir.x < 0.1f && dir.x > -0.1) { //is not to the side!
			StartCoroutine(ShowHitSide(2 + forwardBack));
		} else { //to the side
			StartCoroutine(ShowHitSide(0 + leftRight));
		}

		health -= 4;
	}

	public IEnumerator ShowHitSide(int index) {
		int sign = index == 0 || index == 3 ? -1 : 1;

		bool isSide = index < 2;

		Vector3 old = new Vector3(1, sign, 1)*2;
		if (isSide)
			old = new Vector3(sign, 1, 1)*2;

		Vector3 newScale = new Vector3(0, 0, 0);


		GameObject hitSide = hitSides[index];
		hitSide.transform.localScale = old;

		float time = 0, speed = 0.4f;
		while (time < 1) {
			time += Time.deltaTime / speed;
			hitSide.transform.localScale = Vector3.Lerp(old, newScale, time);

			yield return null;
		}

	}
	public IEnumerator ShowHitMarker() {

		Vector3 old = new Vector3(1, 1, 1);
		Vector3 newScale = new Vector3(0, 0, 0);

		hitMarker.transform.localScale = old;

		float time = 0, speed = 0.3f;
		while (time < 1) {
			time += Time.deltaTime / speed;
			hitMarker.transform.localScale = Vector3.Lerp(old, newScale, time);

			yield return null;
		}

	}

	public void HasHitTarget() {
		StartCoroutine(ShowHitMarker());
	}
}
