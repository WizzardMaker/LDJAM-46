using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float horSpeed = 1, verSpeed = 2, sensitivity = 5;

	Camera c;

    // Start is called before the first frame update
    void Start() {
		c = GetComponentInChildren<Camera>();
		Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
		Vector3 lookUp;

		lookUp = c.transform.localRotation.eulerAngles;

		lookUp.x += Input.GetAxis("Mouse Y") * verSpeed * sensitivity * Time.deltaTime;
		//lookUp.x = ClampAngle(lookUp.x, -60, 60);

		c.transform.localRotation = Quaternion.Euler(lookUp);

		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * horSpeed * sensitivity * Time.deltaTime);
	}

}
