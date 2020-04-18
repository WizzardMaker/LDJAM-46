using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceFuelMeter : MonoBehaviour, IInteractable
{
	public string GetText() {
		return $"{(int)TrainController.instance.fuel}m until next Station";
	}

	public void Interact() {

	}
}
