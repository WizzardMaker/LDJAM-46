using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainController : MonoBehaviour {
	public static TrainController instance;

	public ParticleSystem smoke;

	public GameObject platformDoorLeft, platformDoorRight;

	public float fuel = 100, fuelConsumption = 5, fuelConsOnFast = 10;

	public float health = 100;

	World.TrainSpeedSetting TrainSpeed {
		get => World.instance.trainSpeed;
		set { World.instance.trainSpeed = value; }
	}

	// Start is called before the first frame update
	void Start() {
		instance = this;

		//StartCoroutine(ManageDoors());
    }

	public IEnumerator ManageDoors() {
		Quaternion closedRot = Quaternion.Euler(-90, 90, -90);
		Quaternion openRotL = Quaternion.Euler(35, 90, -90);
		Quaternion openRotR = Quaternion.Euler(-220, 90, -90);

		while (true) {
			World.TrainSpeedSetting oldSetting = TrainSpeed;

			yield return new WaitUntil(()=>TrainSpeed == World.TrainSpeedSetting.Stop || TrainSpeed != World.TrainSpeedSetting.Stop);

			Quaternion fromL = closedRot;
			Quaternion fromR = closedRot;
			Quaternion toL = openRotL;
			Quaternion toR = openRotR;


			if (TrainSpeed != World.TrainSpeedSetting.Stop) {
				fromL = openRotL;
				fromR = openRotR;
				toL = closedRot;
				toR = closedRot;
			}

			if (platformDoorLeft.transform.localRotation == toL)
				continue;

			float t = 0;
			while (t < 1) {
				t += Time.deltaTime / 0.5f;

				platformDoorLeft.transform.localRotation = Quaternion.Lerp(fromL, toL, t);
				platformDoorRight.transform.localRotation = Quaternion.Lerp(fromR, toR, t);

				yield return null;
			}
		}
	}

	// Update is called once per frame
	void Update() {
		if (PauseMenu.isPaused)
			return;
		if (TrainSpeed != World.TrainSpeedSetting.Stop) {
			fuel -= (TrainSpeed == World.TrainSpeedSetting.Fast ? fuelConsOnFast : fuelConsumption) * Time.deltaTime;
		}

		if (TrainSpeed == World.TrainSpeedSetting.Stop) {
			health += Time.deltaTime * .75f;
		}
		if (TrainSpeed == World.TrainSpeedSetting.Fast) {
			health -= Time.deltaTime * 1.5f;
		}

		fuel = Mathf.Clamp(fuel, 0, 100);
		health = Mathf.Clamp(health, 0, 100);

		var emission = smoke.emission;
		emission.enabled = true;

		if (fuel <= 0) {
			TrainSpeed = World.TrainSpeedSetting.Stop;
			emission.enabled = false;
		}
	}

	public void Hit(float damage) {
		health -= damage;
	}
}
