using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelMeter : MonoBehaviour, IInteractable
{
	public GameObject meter;

	public string GetText() {
		return $"Remaining fuel {(int)TrainController.instance.fuel}%";
	}

	public void Interact() {

	}

	public void Update() {
		float angle = World.Remap(TrainController.instance.fuel, 0, 100, -80, 80);

		meter.transform.localRotation = Quaternion.Euler(-90, 0, angle);
	}
}
