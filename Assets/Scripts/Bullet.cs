using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float speed;
	Collider c;

	public float lifetime = 8;
	public float timer;

    // Start is called before the first frame update
    void Start() {
		c = GetComponentInChildren<Collider>();
		timer = Time.time + lifetime;
    }

	public int GetLayerMask() {
		int myLayer = gameObject.layer;
		int layerMask = 0;

		for (int i = 0; i < 32; i++) {
			if (!Physics.GetIgnoreLayerCollision(myLayer, i)) {
				layerMask = layerMask | 1 << i;
			}
		}

		return layerMask;
	}

    // Update is called once per frame
    void Update() {
		transform.position += transform.forward * speed * Time.deltaTime;

		if(Physics.Raycast(transform.position, transform.forward,out RaycastHit info, speed * Time.deltaTime * 2, GetLayerMask())) {
			Hit(info.collider);
		}

		if(Time.time > timer) {
			Destroy(gameObject);
		}
    }

	private void Hit(Collider collider) {
		Debug.Log($"Hit! Collider: {collider.gameObject.name}");

		Destroyable a = collider.GetComponentInParent<Destroyable>();
		a?.BulletHit();

		Destroy(gameObject);
	}
}
