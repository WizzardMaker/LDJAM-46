using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBurner : MonoBehaviour, IInteractable {
	public string GetText() {
		string text = "Press 'e' to add fuel";
		if (!PlayerController.instance.transportsFuel) {
			text = "You don't have fuel on hand";
		}

		return text;
	}

	public void Interact() {
		if (!PlayerController.instance.transportsFuel)
			return;

		TrainController.instance.fuel += 5;
		PlayerController.instance.transportsFuel = false;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
