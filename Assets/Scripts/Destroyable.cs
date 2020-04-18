using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour{
	public float health = 4;

	public void BulletHit() {
		health--;
		Debug.Log($"Hit! Object: {gameObject.name}");

		if (health <= 0)
			Destroy(gameObject);
	}
}
