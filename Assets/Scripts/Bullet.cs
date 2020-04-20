using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float speed;
	Collider c;

	public GameObject hitSparks;

	public float lifetime = 8;
	public float timer;

	public bool isPlayerFired = false;

	int layerMask;

    // Start is called before the first frame update
    void Start() {
		c = GetComponentInChildren<Collider>();
		timer = Time.time + lifetime;

		layerMask = GetLayerMask();

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
    void FixedUpdate() {
		if (PauseMenu.isPaused)
			return;

		transform.position += transform.forward * speed * Time.fixedDeltaTime;

		if(Physics.Raycast(transform.position - transform.forward* speed * Time.fixedDeltaTime, transform.forward,out RaycastHit info, speed * Time.fixedDeltaTime, layerMask)) {
			Hit(info.collider, info.point, info.normal);
		}

		if(Time.time > timer) {
			Destroy(gameObject);
		}
    }

	private void Hit(Collider collider, Vector3 hitPos, Vector3 hitNormal) {
		Destroyable a = collider.GetComponentInParent<Destroyable>();
		a?.BulletHit();

		PlayerController p = collider.GetComponentInParent<PlayerController>();
		p?.BulletHit(hitPos);

		if (!isPlayerFired) {
			TrainController t = collider.GetComponentInParent<TrainController>();
			t?.Hit(1);
		}

		if (a != null) {
			PlayerController.instance.HasHitTarget();
		}

		GameObject g = Instantiate(hitSparks);
		g.transform.position = hitPos;
		g.transform.rotation = Quaternion.Euler(hitNormal);

		Destroy(gameObject);
	}
}
