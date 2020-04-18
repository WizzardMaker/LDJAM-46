using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public static PlayerController instance;

	[HideInInspector]
	public CharacterController c;

	public GameObject fuelObject;

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
}
