using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {
	public static TrainController instance;
	
	public float fuel = 100, fuelConsumption = 5, fuelConsOnFast = 10;

	public float health = 100;

	World.TrainSpeedSetting TrainSpeed {
		get => World.instance.trainSpeed;
		set { World.instance.trainSpeed = value; }
	}

	// Start is called before the first frame update
	void Start() {
		instance = this;
    }

    // Update is called once per frame
    void Update() {
		fuel -= (TrainSpeed == World.TrainSpeedSetting.Fast ? fuelConsOnFast : fuelConsumption) * Time.deltaTime;

		if(fuel <= 0) {
			TrainSpeed = World.TrainSpeedSetting.Stop;
		}
    }

	public void Hit(float damage) {

	}
}
