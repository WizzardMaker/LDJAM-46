using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour {
	public TextMeshPro text;

	public IInteractable interactable;

	public static float interactionDistance = 3f;

	public bool highlighted = false;

	// Start is called before the first frame update
	void Start() {
		interactable = GetComponentInChildren<IInteractable>();
	}

	// Update is called once per frame
	void Update() {
		highlighted = false;

		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1));
		if (Physics.Raycast(ray, out RaycastHit info, interactionDistance)) {
			if(info.collider.GetComponentInParent<Interactable>() == this) {
				highlighted = true;

				if (Input.GetKeyDown(KeyCode.E)) {
					interactable.Interact();
				}
			}
		}

		if(text == null) {
			return;
		}

		text.text = interactable.GetText();

		if (highlighted) {
			text.transform.localScale = Vector3.Lerp(text.transform.localScale, Vector3.one, Time.deltaTime * 3);
		} else {
			text.transform.localScale = Vector3.Lerp(text.transform.localScale, Vector3.zero, Time.deltaTime * 5);
			if(text.transform.localScale.x < 0.1f) {
				text.transform.localScale = Vector3.zero;
			}
		}
	}
}
