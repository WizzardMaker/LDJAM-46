using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour {
	public static AmmoUI instance;
	public GameObject[] ammoElements;

	public void Fire() {
		StartCoroutine(RemoveAmmo(PlayerController.instance.revolver.bullets));
	}

	public void Reload() {
		StopAllCoroutines();
		StartCoroutine(ReloadAmmo());
	}

	IEnumerator RemoveAmmo(int index) {
		float t = 0;
		while (t < 1) {
			t += Time.deltaTime / 0.1f;
			ammoElements[index].transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
			yield return null;
		}
	}

	IEnumerator ReloadAmmo() {
		float t = 0;
		while (t < 1) {
			t += Time.deltaTime / 0.1f;
			foreach(var ammo in ammoElements) {
				if(ammo.transform.localScale != Vector3.one)
					ammo.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
			}

			yield return null;
		}
	}

	// Start is called before the first frame update
	void Start() {
		instance = this;
    }

    // Update is called once per frame
    void Update() {
        
    }


}
