using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationFile : MonoBehaviour
{
    static public bool isLangChanged;
    static public int numShopOption; // 1-selected; 2-choose; 3-???
    static public string pointsShop;
    public Text confirmExit_Info;
    public Text confirmExit_Apply;
    public Text confirmExit_Disclaim;
    public Text endGame_Title;
    public Text endGame_Info;
    public Text pauseTitle;
    public Text pauseToMenu;
    public Text pauseToGame;
    public Text infoMenu_Title;
    public Text infoMenu_Devs;
    public Text infoMenu_Music;
    public Text settingsMenu_Title;
    public Text settingsMenu_Lang;
    public Text settingsMenu_Info;
    public Text socialMenu_Title;
    public Text upgradeMenu_Title;
    public Text upgradeMenu_Descript;
    public Text shopMenu_Health;
    public Text shopMenu_Energy;
    public Text shopMenu_Speed;
    public Text shopMenu_Cost;
    public Text shopMenu_PowerLock;
    public Text helpMenu_Title;
    public Text helpMenu_moneyDesc;
    public Text helpMenu_energyDesc;
    public Text helpMenu_enemyDesc;
    public Text helpMenu_enemyShieldDesc;
    public Text helpMenu_b1Desc;
    public Text helpMenu_b2Desc;
    public Text helpMenu_b3Desc;
    public Text helpMenu_b4Desc;
    public Text mainMenu_connectionInfo;

    private void Start(){
        isLangChanged = true;
        numShopOption = 1;
    }

    private void Update(){
        if (isLangChanged){
            ChangeLang(Main.langNum);
            isLangChanged = false;
        }
    }

    private void ChangeLang(int lang){
        switch(lang){
        case 1: // english
            confirmExit_Info.text = "EXIT THE GAME ?";
			confirmExit_Apply.text = "YES";
			confirmExit_Disclaim.text = "NO";
            endGame_Title.text = "You Won !";
            endGame_Info.text = "Congratulations, you helped the cube \"H\" to get rid of the framework that restrained his character, his strength.\nYou are very strong, patient, brave. Maybe now you should change something in your life.\n\nGood luck to you, person.";
            pauseTitle.text = "PAUSE";
			pauseToGame.text = "BACK";
			pauseToMenu.text = "MENU";
            infoMenu_Title.text = "- ABOUT THE GAME -";
            infoMenu_Devs.text = "Developers:";
            infoMenu_Music.text = "Music:";
            settingsMenu_Title.text = "- SETTINGS -";
            settingsMenu_Lang.text = "English";
            settingsMenu_Info.text = "About the game";
            socialMenu_Title.text = "Subscribe, bro :)";
            upgradeMenu_Title.text = "Upgrading \"Cube\"";
            upgradeMenu_Descript.text = "After watching the video, you will receive:\n\n- Increased health reserve;\n- Amplifier energy cube;\n- Multiplier coins and extra points.\n\nBonus works in one lifetime :)";
            shopMenu_Health.text = "HEALTH: ";
            shopMenu_Energy.text = "ENERGY: ";
            shopMenu_Speed.text = "SPEED: ";
            switch(numShopOption){
            case 1:
                shopMenu_Cost.text = "SELECTED";
                break;
            case 2:
                shopMenu_Cost.text = "CHOOSE";
                break;
            case 3:
                shopMenu_Cost.text = "???";
                break;
            default:
                break;
            }
            shopMenu_PowerLock.text = "Need:\n" + pointsShop + "\nEnergy";
            helpMenu_Title.text = "- HELP -";
            helpMenu_moneyDesc.text = "@ Collect coins in the game to update the cube’ skins in the store.";
            helpMenu_energyDesc.text = "@ Collect energy points by picking up coins and bonuses. Reach 1,000,000 points to complete the game.";
            helpMenu_enemyDesc.text = "@ Beware of enemies. To destroy them, collect coins and bonuses.";
            helpMenu_enemyShieldDesc.text = "@ Some of them have shields that are destroyed by activators.";
            helpMenu_b1Desc.text = "@ This bonus returns moving spikes to their original place.";
            helpMenu_b2Desc.text = "@ This bonus increases the health of the cube.";
            helpMenu_b3Desc.text = "@ This bonus slows down time (valid for a few seconds).";
            helpMenu_b4Desc.text = "@ This bonus increases coins and points several times.";
            mainMenu_connectionInfo.text = "Connection attempt...";
            break;
        case 2: // russian
            confirmExit_Info.text = "ВЫЙТИ ИЗ ИГРЫ ?";
			confirmExit_Apply.text = "ДА";
			confirmExit_Disclaim.text = "НЕТ";
            endGame_Title.text = "Победа !";
            endGame_Info.text = "Поздравляю, ты помог кубу по имени \"H\" избавиться от рамок, которые сдерживали его характер, его силу.\nТы очень силён, терпелив, храбр. Может теперь тебе стоит что-то поменять в своей жизни.\n\nУдачи тебе, личность.";
            pauseTitle.text = "ПАУЗА";
			pauseToGame.text = "НАЗАД";
			pauseToMenu.text = "В МЕНЮ";
            infoMenu_Title.text = "- ОБ ИГРЕ -";
            infoMenu_Devs.text = "Разработчики:";
            infoMenu_Music.text = "Музыка:";
            settingsMenu_Title.text = "- НАСТРОЙКИ -";
            settingsMenu_Lang.text = "Русский";
            settingsMenu_Info.text = "Об игре";
            socialMenu_Title.text = "Подпишись, бро :)";
            upgradeMenu_Title.text = "Улучшение \"Куба\"";
            upgradeMenu_Descript.text = "Посмотрев видео, вы получите:\n\n- Увеличенный запас здоровья;\n- Усилитель энергии куба;\n- Множитель монет и дополнительных очков.\n\nБонус работает в течение одной жизни :)";
            shopMenu_Health.text = "ЗДОРОВЬЕ: ";
            shopMenu_Energy.text = "ЭНЕРГИЯ: ";
            shopMenu_Speed.text = "СКОРОСТЬ: ";
            switch(numShopOption){
            case 1:
                shopMenu_Cost.text = "АКТИВЕН";
                break;
            case 2:
                shopMenu_Cost.text = "ВЫБРАТЬ";
                break;
            case 3:
                shopMenu_Cost.text = "???";
                break;
            default:
                break;
            }
            shopMenu_PowerLock.text = "Нужно:\n" + pointsShop + "\nЭнергии";
            helpMenu_Title.text = "- ПОМОЩЬ -";
            helpMenu_moneyDesc.text = "@ Собирай монеты в самой игре, чтобы обновить скины Куба в магазине.";
            helpMenu_energyDesc.text = "@ Собирай очки энергии, подбирая монеты и бонусы. Достигни 1,000,000 очков, чтобы пройти игру.";
            helpMenu_enemyDesc.text = "@ Остерегайся врагов. Чтобы их уничтожить, собирай монеты и бонусы.";
            helpMenu_enemyShieldDesc.text = "@ У некоторых из них есть щиты, которые уничтожаются с помощью активаторов.";
            helpMenu_b1Desc.text = "@ Данный бонус возвращает двигающиеся шипы на их первоначальное место.";
            helpMenu_b2Desc.text = "@ Данный бонус увеличивает здоровье куба.";
            helpMenu_b3Desc.text = "@ Данный бонус замедляет время (действует несколько секунд).";
            helpMenu_b4Desc.text = "@ Данный бонус увеличивает монеты и очки в несколько раз.";
            mainMenu_connectionInfo.text = "Попытка подключения...";
            break;
        default:
            break;
        }
    }
}
