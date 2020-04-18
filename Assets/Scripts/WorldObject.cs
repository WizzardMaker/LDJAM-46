using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour {
	public bool returnToOtherSide;

    // Start is called before the first frame update
    void Start()
    {
		World.instance.objects.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < -World.instance.worldLength/2) {
			if (returnToOtherSide) {
				transform.position += Vector3.forward * World.instance.worldLength;
			} else {
				Destroy(gameObject);
			}

		}
    }
	private void OnDestroy() {
		World.instance.objects.Remove(this);
	}
}
