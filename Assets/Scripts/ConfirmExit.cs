using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmExit : MonoBehaviour {

	public GameObject cnmExit;
	static public bool isExitting;
	public Button bToPlay;
	public Button bToShop;
	public Button bToSets;
	public Button bToAchievs;
	public Button bToStats;
	public Button bSocialize;
	public Button bAdsOff;
	
	void Start()
	{
		bToPlay.gameObject.SetActive (true);
		bToShop.gameObject.SetActive (true);
		bToSets.gameObject.SetActive (true);
		bToAchievs.gameObject.SetActive(true);
		bToStats.gameObject.SetActive(true);
		bSocialize.gameObject.SetActive(true);
		bAdsOff.gameObject.SetActive(true);
		isExitting = false;
		cnmExit.SetActive (false);
	}

	void Update()
	{
		if (isExitting) {
			isExitting = false;
			bToPlay.gameObject.SetActive (false);
			bToShop.gameObject.SetActive (false);
			bToSets.gameObject.SetActive (false);
			bToAchievs.gameObject.SetActive(false);
			bToStats.gameObject.SetActive(false);
			bSocialize.gameObject.SetActive(false);
			bAdsOff.gameObject.SetActive(false);
			AudioController.pauseAudio = true;
			cnmExit.SetActive (true);
		}
	}

	public void OnButtonExApplyClicked()
	{
		Application.Quit();
	}

	public void OnButtonExDisclaimClicked()
	{
		cnmExit.SetActive (false);
		bToPlay.gameObject.SetActive (true);
		bToShop.gameObject.SetActive (true);
		bToSets.gameObject.SetActive (true);
		bToAchievs.gameObject.SetActive(true);
		bToStats.gameObject.SetActive(true);
		bSocialize.gameObject.SetActive(true);
		bAdsOff.gameObject.SetActive(true);
		AudioController.pauseAudio = false;
	}
}
