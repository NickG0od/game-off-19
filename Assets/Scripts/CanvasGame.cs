using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGame : MonoBehaviour {
	public GameObject PauseObj;
	public Image _healthbar;
	public static bool isBackClickedPause;
	public static bool isBackClickedGame;

	void Start ()
	{
		PauseObj.SetActive(false);
		isBackClickedGame = false;
		isBackClickedPause = false;
	}

	void Update ()
	{
		if (isBackClickedGame) {
			OnButtonToGameClicked ();
			isBackClickedGame = false;
		}
		if (isBackClickedPause) {
			OnButtonPauseClicked ();
			isBackClickedPause = false;
		}

		HealthBarControl(); // Отрисовка healthbar игрока
	}

	public void OnButtonToGameClicked ()
	{
		// Откл паузу
		PauseObj.SetActive(false);
		Time.timeScale = 1;
		Main.gameStatus = 1;
	}

	public void OnButtonToMenuClicked ()
	{
		// выход в главное меню
		if (!Main.goToMenu) {
			Time.timeScale = 1;
			PauseObj.SetActive(false);
			Main.goToMenu = true;
			AudioController.goMenuMusic = true;
		}	
	}

	public void OnButtonPauseClicked ()
	{
		// Вкл - паузу
		PauseObj.SetActive(true);
		Time.timeScale = 0;
		Main.gameStatus = 2;
	}

	private void HealthBarControl(){
		float currentWidth = 140f * PlayerCntrl.health / Data.Instance.healthElement [Data.Instance.currentType];
		_healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2(currentWidth, 8f);
	}
}
