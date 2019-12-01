using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings_in_menu : MonoBehaviour {
	public Scrollbar scrbMusic;
	public Scrollbar scrbSound;
	public Text musicVal;
	public Text soundVal;
	
	void Update()
	{
        // переход из главного меню в настройки
        if (TurnerMenu.isChangeToSets)
        {
            scrbMusic.value = Main.musicValue;
            scrbSound.value = Main.soundValue;
            TurnerMenu.isChangeToSets = false;
        }
		Main.musicValue = scrbMusic.value;
		musicVal.text = (int)(Main.musicValue*100) + "%";
		Main.soundValue = scrbSound.value;
		soundVal.text = (int)(Main.soundValue*100) + "%";
    }
}
