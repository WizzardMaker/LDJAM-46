using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelMeter : MonoBehaviour, IInteractable
{
	public string GetText() {
		return $"Remaining fuel {(int)TrainController.instance.fuel}%";
	}

	public void Interact() {

	}
}
