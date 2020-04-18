using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedHandle : MonoBehaviour
{
	public Transform handlePivot;
	public float handleAngle = 30f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
		float finalAngle = 0;

		switch (World.instance.trainSpeed) {
			case World.TrainSpeedSetting.Stop:
			finalAngle = -handleAngle;
			break;
			case World.TrainSpeedSetting.Slow:
			finalAngle = 0;
			break;
			case World.TrainSpeedSetting.Fast:
			finalAngle = handleAngle;
			break;
		}
		handlePivot.rotation = Quaternion.Lerp(handlePivot.rotation, Quaternion.Euler(Vector3.right * finalAngle), Time.deltaTime  *5);
    }
}
