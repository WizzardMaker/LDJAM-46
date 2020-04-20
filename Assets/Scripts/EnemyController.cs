using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
	public Vector3 desiredPos;

	public Animator anim;

	public GameObject head, arm;
	public Revolver revolver;

	public NavMeshAgent agent;

	public float roamRadius = 15f;

    // Start is called before the first frame update
    void Start() {
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponentInChildren<Animator>();
		agent.updateRotation = false;

		SetNewDesiredPos();

		StartCoroutine(ChangeDesiredPos());
		StartCoroutine(FireGun());
	}

	public IEnumerator FireGun() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(2, 8));
			if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > 30)
				continue;

			revolver.FireGun(false);

			if (revolver.bullets <= 0)
				revolver.Reload();
		}
	}

	private void SetNewDesiredPos(bool keepSide = false) {
		float x = Random.Range(-roamRadius, roamRadius);
		float z = Random.Range(-roamRadius, 0);

		if (keepSide) {
			x *= Mathf.Sign(x);
			x *= Mathf.Sign(transform.position.x);
		}

		x += x * Mathf.Sign(x) < 5 ? Mathf.Sign(x) * 7 : 0;
		z += z * Mathf.Sign(z) < 5 ? Mathf.Sign(z) * 7 : 0;

		desiredPos = new Vector3(x, 0, z);
		agent.SetDestination(desiredPos);
	}

	public IEnumerator ChangeDesiredPos() {
		while (true) {
			yield return new WaitUntil(() => Vector3.Distance(transform.position, desiredPos)< 1);
			yield return new WaitForSeconds(Random.Range(3, 8));

			if(World.instance.trainSpeed == World.TrainSpeedSetting.Fast) {
				yield break;
			}

			SetNewDesiredPos(true);
		}
	}

    // Update is called once per frame
    void Update() {
		if (PauseMenu.isPaused)
			return;

		head.transform.LookAt(PlayerController.instance.transform.position);
		arm.transform.LookAt(PlayerController.instance.transform.position);
		revolver.lookAtPos = PlayerController.instance.transform.position;

		if(World.instance.trainSpeed == World.TrainSpeedSetting.Fast) {
			desiredPos -= Vector3.forward * 5 * Time.deltaTime;
			agent.SetDestination(desiredPos);

			if (transform.position.z < -200) {
				Destroy(gameObject);
			}
		}

		anim.SetFloat("speed", World.Remap(World.instance.actualSpeed, 0, 20, 0, 2));
		if(World.instance.actualSpeed < 0.1f) {
			anim.SetFloat("speed", agent.velocity.magnitude / 5);
		}
	}
}
