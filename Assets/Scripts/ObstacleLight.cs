using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleLight : MonoBehaviour {
	public Light light;

    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(ManageLight());
		light.intensity = 0;

	}

	IEnumerator ManageLight() {
		while (true) {
			yield return new WaitUntil(() => World.instance.obstacles.Count > 0 && World.instance.obstacles.FirstOrDefault()?.transform.position.z <120);

			float t = 0;
			while(t < 1) {
				t += Time.deltaTime / 0.5f;

				light.intensity = Mathf.Lerp(0, 2, t);

				yield return null;
			}

			yield return new WaitUntil(() => World.instance.obstacles.Count == 0);

			t = 0;
			while (t < 1) {
				t += Time.deltaTime / 0.5f;

				light.intensity = Mathf.Lerp(2, 0, t);

				yield return null;
			}
		}
	}
}
