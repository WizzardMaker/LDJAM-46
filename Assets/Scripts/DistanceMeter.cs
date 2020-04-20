using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceMeter : MonoBehaviour, IInteractable
{
	public string GetText() {
		return $"{(int)World.instance.distanceTraveled}m traveled";
	}

	public void Interact() {

	}
}
