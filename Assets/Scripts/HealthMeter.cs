using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeter : MonoBehaviour, IInteractable {
	public GameObject meter;

	public string GetText() {
		return $"Health {(int)TrainController.instance.health}%";
	}

	public void Interact() {

	}

	public void Update() {
		float angle = World.Remap(TrainController.instance.health, 0, 100, -80, 80);

		meter.transform.localRotation = Quaternion.Euler(-90, 0, angle);
	}
}