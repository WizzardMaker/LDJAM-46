using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	public static string deathReason = "Lazyness was you killer!";
	public GameObject deathReasonObject;

	public bool PlayMusic { get => MusicVolume.playMusic; set => MusicVolume.playMusic = value; }

	int page = 0;
	public GameObject[] pages;
	public GameObject nextPageButton;
	public GameObject prevPageButton;

	public void Start() {
		if(deathReasonObject != null) {
			deathReasonObject.GetComponent<TextMeshProUGUI>().text = deathReason;
		}
	}

	public void Update() {
		Cursor.lockState = CursorLockMode.None;
	}

	public void ExitGame() {
		Application.Quit();
	}

	public void StartGame() {
		SceneManager.LoadScene("Game");
	}

	public void Tutorial() {
		SceneManager.LoadScene("Tutorial");
	}
	public void MainMenu() {
		SceneManager.LoadScene("MainMenu");
	}

	public void NextPage() {
		pages[page].SetActive(false);
		page++;
		pages[page].SetActive(true);

		nextPageButton.SetActive(page < pages.Length-1);
		prevPageButton.SetActive(page > 0);
	}

	public void PrevPage() {
		pages[page].SetActive(false);
		page--;
		pages[page].SetActive(true);

		prevPageButton.SetActive(page > 0);
		nextPageButton.SetActive(page < pages.Length - 1);
	}
}
