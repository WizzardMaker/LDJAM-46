using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelContainer : MonoBehaviour, IInteractable {
	public string GetText() {
		string text = "Press 'e' to take fuel";
		if (PlayerController.instance.transportsFuel) {
			text = "You alread carry fuel";
		}

		return text;
	}

	public void Interact() {
		PlayerController.instance.transportsFuel = true;
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
