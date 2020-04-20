using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadObstacle : MonoBehaviour {

	ConstantForce[] forces;
	public void Created() {
		StartCoroutine(StartDeath());
		forces = GetComponentsInChildren<ConstantForce>();

		foreach (Rigidbody f in GetComponentsInChildren<Rigidbody>()) {
			f.isKinematic = false;
		}
	}

	private void Update() {
		foreach (ConstantForce f in forces) {
			float multi = World.instance.trainSpeed != World.TrainSpeedSetting.Fast ? 1.5f : 1;
			f.force = -Vector3.forward * World.instance.actualSpeed * multi;
		}
	}

	bool wasDestroyed = false;

	IEnumerator StartDeath() {
		yield return new WaitForSeconds(4f);

		float t = 0;
		while(t < 1) {
			t += Time.deltaTime / .5f;

			foreach (ConstantForce f in forces) {
				f.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
			}
			yield return null;
		}

		Destroy(gameObject);
	}
}
