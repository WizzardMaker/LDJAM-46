using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float horSpeed = 1, verSpeed = 2, sensitivity = 5;

	public Vector3 lookAngle = Vector3.zero;

	Camera c;

    // Start is called before the first frame update
    void Start() {
		c = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update() {
		if (PauseMenu.isPaused)
			return;

		lookAngle.x += Input.GetAxis("Mouse Y") * verSpeed * sensitivity * Time.deltaTime;
		//lookUp.x = ClampAngle(lookUp.x, -60, 60);

		lookAngle.x = Mathf.Clamp(lookAngle.x, -60, 60);

		c.transform.localRotation = Quaternion.Euler(lookAngle);

		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * horSpeed * sensitivity * Time.deltaTime);
	}

}
