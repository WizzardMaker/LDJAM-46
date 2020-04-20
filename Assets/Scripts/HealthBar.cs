using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
		healthBar = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update() {
		healthBar.fillAmount = World.Remap(PlayerController.instance.health, 0, 20, 0, 1);
    }
}
