using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorldObject))]
public class Obstacle : MonoBehaviour
{
	public float trainHitPoint;
	public float damage = 5;

	void Update() {
        if(transform.position.z <= trainHitPoint) {
			Debug.Log("Obstacle Hit!");

			TrainController.instance.Hit(damage);

			Destroy(gameObject);
		}
    }
}
