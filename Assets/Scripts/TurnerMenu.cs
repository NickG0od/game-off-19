using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnerMenu : MonoBehaviour {
    // Главное меню + переключение между другими меню
	public GameObject menu; 
	public GameObject shop;
	public GameObject sets;
	public GameObject info;
	public GameObject endGame;
	public GameObject socialMenu;
    public Animation anim;
    private bool fromMenuToShop;
	private bool fromMenuToSets;
	private bool fromSetsToInfo;
	private bool fromMenuToEndGame;
	private bool fromMenuToSocial;
	private bool flag;
    static public bool isMainMenu;
    static public bool isChangeToShop;
    static public bool isChangeToSets;
	static public bool isChangeToInfo;
	static public bool isChangeToEndGame;
	static public bool isChangeToSocial;
    static public bool isBackAndroidClicked;
	public Text txtLang;
	public Text txtInfo;

	void Start ()
	{
		menu.SetActive(true);
		shop.SetActive (false);
        sets.SetActive(false);
		info.SetActive(false);
		endGame.SetActive(false);
		socialMenu.SetActive(false);
		fromMenuToShop = false;
		fromMenuToSets = false;
		fromSetsToInfo = false;
		fromMenuToEndGame = false;
		fromMenuToSocial = false;
		flag = true;
        isMainMenu = true;
        isBackAndroidClicked = false;
        isChangeToShop = false;
        isChangeToSets = false;
		isChangeToInfo = false;
		isChangeToEndGame = false;
		isChangeToSocial = false;
        // загрузка данных
        Data.Instance.currentType = PlayerPrefs.GetInt ("typeElement", 0);
		for (int i = 0; i < Data.Instance.countElements; i++) {
			Data.Instance.isBought[i] = PlayerPrefs.GetInt ("isBought-"+ i, 0);
			Data.Instance.isPowerLimit [i] = PlayerPrefs.GetInt ("isPower-" + i, 0);
		}
		Data.Instance.isBought [0] = 1;
		Data.Instance.isPowerLimit [0] = 1;
	}

	void Update ()
	{
		if (! flag && !anim.isPlaying)
			flag = true;
		
        if (isBackAndroidClicked)
        {
            OnButtonBackClicked();
            isBackAndroidClicked = false;
        }
		if (Main.isEndAndWall){
			OnEndGameActivated();
			Main.isEndAndWall = false;
		}
	}

	void Conversion() // Переход из главного меню в магазин и обратно.
	{
		if (fromMenuToShop) {
			menu.SetActive (false);
			shop.SetActive (true);
			sets.SetActive (false);
			info.SetActive(false);
			endGame.SetActive(false);
			socialMenu.SetActive(false);
            isMainMenu = false;
		} else if (fromMenuToSets) {
			menu.SetActive (false);
			shop.SetActive (false);
			sets.SetActive (true);
			info.SetActive(false);
			endGame.SetActive(false);
			socialMenu.SetActive(false);
            isMainMenu = false;
        } else if (fromSetsToInfo) {
			menu.SetActive(false);
			shop.SetActive(false);
			sets.SetActive(false);
			info.SetActive(true);
			endGame.SetActive(false);
			socialMenu.SetActive(false);
			isMainMenu = false;
		} else if (fromMenuToEndGame){
			menu.SetActive(false);
			shop.SetActive(false);
			sets.SetActive(false);
			info.SetActive(false);
			endGame.SetActive(true);
			socialMenu.SetActive(false);
			isMainMenu = false;
		} else if (fromMenuToSocial){
			menu.SetActive(true);
			shop.SetActive(false);
			sets.SetActive(false);
			info.SetActive(false);
			endGame.SetActive(false);
			socialMenu.SetActive(true);
			isMainMenu = false;
		} else {
			menu.SetActive (true);
			shop.SetActive (false);
			sets.SetActive (false);
			info.SetActive (false);
			endGame.SetActive(false);
			socialMenu.SetActive(false);
            isMainMenu = true;
        }
	}

    public void OnButtonShopClicked()
    {
        if (flag && !Buttons.flagPlayButton){
			isMainMenu = false;
            fromMenuToShop = true;
            anim.Play();
            Invoke("Conversion", 0.5f);
        }
        flag = false;
        isChangeToShop = true;
    }

    public void OnButtonSetsClicked ()
	{
		if (flag && !Buttons.flagPlayButton) {
			isMainMenu = false;
			fromMenuToSets = true;
			anim.Play ();
			Invoke ("Conversion", 0.5f);
		}
		flag = false;
        isChangeToSets = true;
	}

	public void OnButtonInfoClicked (){
		if (flag && !Buttons.flagPlayButton) {
			isMainMenu = false;
			fromSetsToInfo = true;
			fromMenuToSets = false;
			anim.Play();
			Invoke("Conversion", 0.5f);
		}
		flag = false;
		isChangeToInfo = true;
	}

	public void OnButtonInfoPointerDown(){
		txtInfo.color = new Color32(225, 225, 225, 200);
	}
	
	public void OnButtonInfoPointerUp(){
		txtInfo.color = new Color32(225, 225, 225, 255);
	}

	public void OnButtonSocialClicked(){
		if (flag && !Buttons.flagPlayButton){
			isMainMenu = false;
			fromMenuToSocial = true;
			Invoke("Conversion", 0.0f);
		}
		flag = false;
		isChangeToSocial = true;
	}

	public void OnEndGameActivated(){
		if (flag && !Buttons.flagPlayButton){
			isMainMenu = false;
			fromMenuToEndGame = true;
			Invoke("Conversion", 0.1f);
		}
		flag = false;
		isChangeToEndGame = true;
	}

	public void OnButtonLangClicked(){
		Main.langNum++;
		LocalizationFile.isLangChanged = true;
		if (Main.langNum > 2) // 2 - кол-во языков в игре
			Main.langNum = 1;
		PlayerPrefs.SetInt ("Lang", Main.langNum);
	}

	public void OnButtonLangPointerDown(){
		txtLang.color = new Color32(225, 225, 225, 200);
	}

	public void OnButtonLangPointerUp(){
		txtLang.color = new Color32(225, 225, 225, 255);
	}

	public void OnButtonBackClicked () // Назад в главное меню
	{
		if (fromMenuToShop) {
			if (flag && !Buttons.flagPlayButton) {
				fromMenuToShop = false;
				anim.Play ();
				Invoke("Conversion", 0.5f);
			}
			flag = false;
			// сохранениие при выходе из магазина
			PlayerPrefs.SetInt ("typeElement", Data.Instance.currentType);
			for (int i = 0; i < Data.Instance.countElements; i++) {
				PlayerPrefs.SetInt ("isBought-" + i, Data.Instance.isBought[i]);
				PlayerPrefs.SetInt ("isPower-" + i, Data.Instance.isPowerLimit[i]);
			}
		}
		if (fromMenuToSets) {
			if (flag) {
				fromMenuToSets = false;
				anim.Play ();
				Invoke("Conversion", 0.5f);
			}
			flag = false;
		}
		if (fromSetsToInfo) {
			if(flag) {
				fromSetsToInfo = false;
				fromMenuToSets = true;
				anim.Play();
				Invoke("Conversion", 0.5f);
			}
			flag = false;
		}
		if (fromMenuToSocial){
			if (flag){
				fromMenuToSocial = false;
				Invoke("Conversion", 0.0f);
			}
			flag = false;
		}
		if (fromMenuToEndGame){
			if (flag){
				fromMenuToEndGame = false;
				anim.Play();
				Invoke("Conversion", 0.5f);
			}
			flag = false;
			isChangeToEndGame = false;
		}
	}
}
