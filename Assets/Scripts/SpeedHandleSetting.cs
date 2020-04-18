using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedHandleSetting : MonoBehaviour, IInteractable
{
	public World.TrainSpeedSetting setting;

	public string GetText() {
		return setting.ToString();
	}

	public void Interact() {
		World.instance.trainSpeed = setting;
	}

}
