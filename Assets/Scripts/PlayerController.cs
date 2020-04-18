using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public static PlayerController instance;

	[HideInInspector]
	public CharacterController c;

	public GameObject fuelObject;

	public Revolver revolver;

	public float speed, sprintMod = 2;

	public bool transportsFuel = false;

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
		if (Input.GetMouseButtonDown(0)) {
			revolver.FireGun();
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			revolver.Reload();
		}

		revolver.lookAtPos = revolver.bulletSpawnPos.transform.forward + revolver.bulletSpawnPos.transform.position;
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1));
		if (Physics.Raycast(ray, out RaycastHit info, Mathf.Infinity, LayerMask.GetMask("Default"))) {
			revolver.lookAtPos = info.point;
		}
		//transform.LookAt(lookAtPos);
	}
}
