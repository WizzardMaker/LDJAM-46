using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public static bool isPaused;
	public GameObject pauseMenu;

	public bool PlayMusic { get => MusicVolume.playMusic; set => MusicVolume.playMusic = value; }

	public void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			isPaused = !isPaused;
		}

		Time.timeScale = isPaused ? 0 : 1;
		pauseMenu.SetActive(isPaused);


		Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
	}

	public void Exit() {
		Application.Quit();
	}

	public void Resume() {
		isPaused = false;
	}
}
