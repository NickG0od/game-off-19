using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main: MonoBehaviour {
	public GameObject platform;
	public GameObject platformMove;
	public GameObject coin;
	public GameObject bonus1;
    public GameObject bonus2;
    public GameObject bonus3;
    public GameObject bonus4;
    public GameObject enemy;
    public GameObject enemyBoss;
    public GameObject player;
	public Canvas canMenu;
	public Canvas canGame;
	public Canvas canStartScr;
	static public bool startScreenEnded;
	public Text textDist;
    public Text textDistRec;
	public Text textMoney;
	public Text textMoneyAll;
	public Text textEnergy;
	public Animation an; // затухание экрана переход от меню к игре и обратно
	public Animation an2; // при переходах из главного меню в другие.
    public Animation anDistRec; // анимация затухания рекорда в начале игры
    public static float speedOfGame; // Глобальная скорость игры - то есть всех элементов
	private float timeForSpawn;
    private float timeSpnPlatform;
    private float timeSpnCoin;
    private float timeSpnBonus1;
    private float timeSpnBonuses;
    private float timeSpnEnemy;
	static public int countObjPlats; // кол - во платформ.
	private bool isMenuActive; // Главное меню активировано
	static public int gameStatus; // Статус игры : 0 - конец; 1 - игра; 2 - пауза
	static public bool goToMenu; // флаг на функцию "в главное меню"
	static public bool goToStart; // флаг на функцию "в игровое меню"
	static public bool isRestartTrapPastb; // флаг на обновление координат "пасти"
	static public bool isChangeColor;
    private int coefDistance; // каждые n секунд 
	static public int colorRandom; // Случайный цвет из некоторого списка
	static public int langNum; // Язык интерфейса (1 - en, 2 - ru, )
	static public float musicValue; // знач-е фоновой музыки
	static public float soundValue; // знач-ие звука
    static public bool isEnemyBossDied; 
    private float timeSpnEnemyBoss;
    static public int multiPly;
    static public float timer_multiply;
	static public int isEndGame; // Переменная - за логику игры при 1000000 очков энергии. 0 - нет, 1 - да
	static public bool isEndAndWall;
	private float randNumToChangeColor;
	static public bool isUpgradeCube;
	public Image showBonus_2x;
	public Image showBonus_3x;
	static public int isFirstRun; // 1-yes; 0-no
	public GameObject bonusRewardedAds;
	static public int statusRewardedAds; //-1 - требуется проверка, 0 - not ready, 1 - is ready, 2 - ГГ взял бонус
	private Color32 colorOld, colorNew;
	private float timeColor;
	private bool isUpdateColor;
	public static float difficultyFactor;

    private IEnumerator ChangeStartScr (float sec)
	{
		yield return new WaitForSeconds (sec);
		canStartScr.planeDistance = -1f;
		canMenu.gameObject.SetActive (true);
		canGame.gameObject.SetActive (true);
		canStartScr.gameObject.SetActive (false);
        startScreenEnded = true;
	}

	private IEnumerator ActiveAnim (float sec)
	{
		yield return new WaitForSeconds (sec);
		an2.Play ();
	}

	private IEnumerator DisableButtons (float sec) {
		yield return new WaitForSeconds(sec);
	}

    private void Start () {
		transform.position = new Vector3(0f, 0f, -10f); // Позиция основной камеры
		isMenuActive = true;
		goToMenu = false;
		goToStart = false;
		gameStatus = 0;
		canStartScr.gameObject.SetActive (true);
		startScreenEnded = false;
		canMenu.gameObject.SetActive (false);
		canGame.gameObject.SetActive (false);
		canStartScr.planeDistance = 2f;
		canMenu.planeDistance = 3f;
		canGame.planeDistance = 5f;
		PlayerCntrl.moneyAll = PlayerPrefs.GetInt ("Money", 0);
		PlayerCntrl.energyPoints = PlayerPrefs.GetInt ("EnergyPs", 0);
		isEndGame = PlayerPrefs.GetInt("IsEndGame",0);
		langNum = PlayerPrefs.GetInt ("Lang", 1);
		musicValue = PlayerPrefs.GetFloat ("Music", 1f);
		soundValue = PlayerPrefs.GetFloat ("Sound", 1f);
		an2.Stop ();
		StartCoroutine ((ActiveAnim(2f))); // затухание экрана : время равно = х - .5
		StartCoroutine (ChangeStartScr(2.5f)); // заставка в начале х секунд
		randNumToChangeColor = 30.0f;
		isFirstRun = PlayerPrefs.GetInt("FirstRun", 1);
		difficultyFactor = PlayerPrefs.GetFloat("diffFactor", 1f);

	}// end of Start()

	private void Update () {
		if (goToStart){
			if (isMenuActive){
				an.Play ();
                Invoke ("OnToStart", 0.75f); // функция "Invoke" запускает метод через n секунд
			}
			goToStart = false;
		}
		if (goToMenu) {
			if (!isMenuActive){
				an.Play ();
                Invoke ("OnToMenu", 0.75f);
			}
			goToMenu = false;
		}
		//------------------------------------------------------------------------------------------------------
		// Режим : "В меню"
		if (isMenuActive) {
			textMoneyAll.text = " x " + PlayerCntrl.moneyAll;
			textEnergy.text = " x " + PlayerCntrl.energyPoints;
			canGame.enabled = false;
        } else
		// Режим : "В игре"
		if (!isMenuActive) {
			canGame.enabled = true;
			if (gameStatus == 1) {
				timeForSpawn = 140.0f / speedOfGame;
				timeSpnPlatform -= Time.deltaTime; // Создание платформ каждые TimeForSpawn секунды
                if (timeSpnPlatform <= 0){
					float chanceToMove = Random.Range (0.0f, 1.0f);
                    if (chanceToMove >= 0.3f){Instantiate(platform);}
                    else{Instantiate(platformMove);}
					timeSpnPlatform = timeForSpawn;
                }
				timeSpnCoin -= Time.deltaTime; // Создание монет
				if (timeSpnCoin <= 0){
					Instantiate(coin);
                    timeSpnCoin = timeForSpawn / 1.5f + Random.Range(0.5f, 1f);
				}
				timeSpnBonus1 -= Time.deltaTime; // Создание первого бонуса : Возвращение ловушки в нач. положение
				if (timeSpnBonus1 <= 0) {       
					Instantiate(bonus1);             
					timeSpnBonus1 = timeForSpawn / 2f + Random.Range(8f, 12f);
				}
                timeSpnBonuses -= Time.deltaTime; // Создание 4 разных бонусов с разными вероятностями
                if (timeSpnBonuses <= 0){
                    int chanceToSpawnObj = Random.Range(2, 5);
                    if (chanceToSpawnObj == 2 && PlayerCntrl.distance > 20){Instantiate(bonus2);}
                    else if (chanceToSpawnObj == 3 && PlayerCntrl.distance > 10){Instantiate(bonus3);}
                	else if (chanceToSpawnObj == 4 && PlayerCntrl.distance > 30){Instantiate(bonus4);}
					timeSpnBonuses = timeForSpawn + Random.Range(2f, 8f);
				}
				timeSpnEnemy -= Time.deltaTime; // Создание врага - (колючка)
				if (timeSpnEnemy <= 0) {
					Instantiate (enemy);
					timeSpnEnemy = Random.Range(4, 6);
                }
                if (isEnemyBossDied){
                    timeSpnEnemyBoss -= Time.deltaTime;
                    if (timeSpnEnemyBoss <= 0){
                        Instantiate(enemyBoss);
                        timeSpnEnemyBoss = Random.Range(6, 12);
						isEnemyBossDied = false;
                    }
                }
                // Проверки на работу бонусов:
                if (timer_multiply > 0){
					timer_multiply -= Time.deltaTime;
					if (multiPly == 2){
						showBonus_2x.gameObject.SetActive(true);
						showBonus_3x.gameObject.SetActive(false);
					}
					if (multiPly == 3){
						showBonus_3x.gameObject.SetActive(true);
						showBonus_2x.gameObject.SetActive(false);
					}
				} else{
					multiPly = 1;
					showBonus_2x.gameObject.SetActive(false);
					showBonus_3x.gameObject.SetActive(false);
				}
                // Изменение текста на gui
                textDist.text = PlayerCntrl.distance.ToString ();
				float tempMoney = PlayerCntrl.moneyAll + PlayerCntrl.money; 
				textMoney.text = " x " + tempMoney;
				// Конец игры + достижение
				if (isEndAndWall) {
					goToMenu = true;
					GPServices.PostAchievement(GPGSIds.achievement__congratulations__end_of_the_game);
				}
				// Проверка на подключение к интернету + создание бонуса.
				//if (statusRewardedAds == 1){bonusRewardedAds.gameObject.SetActive(true);}
				//else {bonusRewardedAds.gameObject.SetActive(false);}
            //-----------------------------------------------------------------
			} else if (gameStatus == 0) {
				gameStatus = 1;
				Invoke("OnDiedPlayer", 0.2f);
			}
			if (PlayerCntrl.distance == 0) {
				coefDistance = 1;
				colorRandom = 1;
				isChangeColor = true;
				CheckColor(colorRandom);
				randNumToChangeColor = 20 * coefDistance;
			}
			if (PlayerCntrl.distance > randNumToChangeColor) {
				coefDistance++;
				speedOfGame += Random.Range(10f, 20f)*difficultyFactor;
				colorRandom = Random.Range (1, 6);
				isChangeColor = true;
				CheckColor(colorRandom);
				randNumToChangeColor = 20 * coefDistance;
			}
			if (isUpdateColor){
				timeColor += Time.deltaTime / 1.5f;
				GetComponent<Camera>().backgroundColor = Color.Lerp(colorOld, colorNew, timeColor);
				if (timeColor >= 1){isUpdateColor = false;}
			}
		}
		//--------------------------------------------------------------------------------------------------------
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor) {
			// активация паузы
			if (Input.GetKeyDown (KeyCode.Escape)) {
				if (!isMenuActive && gameStatus == 1) {
					// Вкл - паузу
					CanvasGame.isBackClickedPause = true;
				}
				else if (!isMenuActive && gameStatus == 2) {
					// Откл паузу
					CanvasGame.isBackClickedGame = true;
				} else if (isMenuActive){
                    if (TurnerMenu.isMainMenu)
                    {
                        ConfirmExit.isExitting = true; // Вызов блока: ( Выход из игры )
                    } else
                    {
                        TurnerMenu.isBackAndroidClicked = true; // Выход в главное меню через кнопку
                    }
				}
			}
			if (Application.platform == RuntimePlatform.WindowsEditor){
				if (Input.GetKeyDown(KeyCode.R)){
					Debug.Log("> Очищены все данные !");
					ResetData();
				}
				if (Input.GetKeyDown(KeyCode.Q)){
					Debug.Log("Добавлятся 5000 монет и 4500 очков энергии");
					PlayerCntrl.moneyAll += 5000;
					PlayerCntrl.energyPoints += 4500;
				}
			}
		}
	}// end of Update()

	private void CheckColor (int c){
		colorOld = GetComponent<Camera>().backgroundColor;
        switch (c){
            case 1: // белый
                colorNew = new Color32(60, 60, 60, 255);
                break;
            case 2: // красный
				colorNew = new Color32(60, 0, 0, 255);
                break;
            case 3: // зеленый
				colorNew = new Color32(0, 60, 0, 255);
                break;
            case 4: // синий
				colorNew = new Color32(0, 0, 60, 255);
                break;
            case 5: // жёлтый
				colorNew = new Color32(60, 60, 0, 255);
                break;
            default:
                break;
        }
		timeColor = 0f;
		isUpdateColor = true;
	}

	private void OnApplicationQuit(){
		// сохранение при выходе из игры.
		PlayerPrefs.SetInt ("typeElement", Data.Instance.currentType);
		for (int i = 0; i < Data.Instance.countElements; i++)
			PlayerPrefs.SetInt ("isBought-" + i, Data.Instance.isBought[i]);
		// сохр-е настроек языка и звука.
		PlayerPrefs.SetInt ("Lang", langNum);
		PlayerPrefs.SetFloat ("Music", musicValue);
		PlayerPrefs.SetFloat ("Sound", soundValue);
		GPServices.isExittingApp = true;
		isUpgradeCube = false;
	}

	private void OnToStart(){
		canMenu.planeDistance = -2f; // canvas - меню на задний фон
		Instantiate(player);
        gameStatus = 1;
        isMenuActive = false;
		Instantiate (platform);
		Instantiate (platform);
		Instantiate (platform);
		textDistRec.GetComponent<Graphic>().color = new Color32(140, 0, 250, 255);
		textDistRec.text = PlayerPrefs.GetInt("recDist", 0).ToString();
        anDistRec.Play();
		GPServices.PostAchievement(GPGSIds.achievement_welcome_to_cubeh);
		InitValues();

		GPServices.Instance.LoadData();
	}

	private void OnToMenu (){
		SaveData ();
		ClearObjects();

        gameStatus = 0;
		canMenu.planeDistance = 2f; // canvas - меню на передний фон
		isMenuActive = true;
		InitValues();
		ParticleController.isParticleSysActivated = false;
	}

	private void OnDiedPlayer(){
		SaveData ();
		ClearObjects();
		
		Instantiate(player);
		ParticleController.isParticleSysActivated = false;
        gameStatus = 1;
		isMenuActive = false;
		Instantiate (platform);
		Instantiate (platform);
		Instantiate (platform);

		textDistRec.GetComponent<Graphic>().color = new Color32(140, 0, 250, 255);
		textDistRec.text = PlayerPrefs.GetInt("recDist", 0).ToString();

        anDistRec.Play();
		InitValues();

		GPServices.Instance.LoadData();
    }

	private void ClearObjects(){
		GameObject[] gameObjGround = GameObject.FindGameObjectsWithTag ("Ground");
		for (int i = 0; i < gameObjGround.Length; i++){Destroy (gameObjGround[i]);}
        GameObject[] gameObjCoin = GameObject.FindGameObjectsWithTag ("Coin");
		for (int i = 0; i < gameObjCoin.Length; i++){Destroy (gameObjCoin[i]);}
        GameObject[] gameObjBonus1 = GameObject.FindGameObjectsWithTag ("Bonus1");
		for (int i = 0; i < gameObjBonus1.Length; i++){Destroy (gameObjBonus1[i]);}
        GameObject[] gameObjBonus2 = GameObject.FindGameObjectsWithTag("Bonus2");
		for (int i = 0; i < gameObjBonus2.Length; i++){Destroy (gameObjBonus2[i]);}
        GameObject[] gameObjBonus3 = GameObject.FindGameObjectsWithTag("Bonus3");
		for (int i = 0; i < gameObjBonus3.Length; i++){Destroy (gameObjBonus3[i]);}
        GameObject[] gameObjBonus4 = GameObject.FindGameObjectsWithTag("Bonus4");
		for (int i = 0; i < gameObjBonus4.Length; i++){Destroy (gameObjBonus4[i]);}
        GameObject[] gameObjBonus5 = GameObject.FindGameObjectsWithTag("Bonus5");
		for (int i = 0; i < gameObjBonus5.Length; i++){Destroy (gameObjBonus5[i]);}
        GameObject[] gameObjEnemy = GameObject.FindGameObjectsWithTag ("Enemy");
		for (int i = 0; i < gameObjEnemy.Length; i++){Destroy (gameObjEnemy[i]);}
        GameObject[] gameObjEnemyB = GameObject.FindGameObjectsWithTag("EnemyBoss");
		for (int i = 0; i < gameObjEnemyB.Length; i++){Destroy (gameObjEnemyB[i]);}
		GameObject[] gameObjBullets = GameObject.FindGameObjectsWithTag("Bullet");
		for (int i = 0; i < gameObjBullets.Length; i++){Destroy (gameObjBullets[i]);}
        GameObject[] gameObjPlayer = GameObject.FindGameObjectsWithTag ("Player");
		for (int i = 0; i < gameObjPlayer.Length; i++){Destroy (gameObjPlayer[i]);}
		GameObject[] gameObjExplode = GameObject.FindGameObjectsWithTag("explosion");
		for (int i = 0; i < gameObjExplode.Length; i++){Destroy (gameObjExplode[i]);}
	}

    private void InitValues(){
		transform.position = new Vector3(0f, 0f, -10f);
        speedOfGame = 50f;
        isRestartTrapPastb = true;
        timeSpnPlatform = 0.1f;
        timeSpnCoin = 0.5f;
        timeSpnBonus1 = 2f;
        timeSpnBonuses = 1f;
        timeSpnEnemy = Random.Range(4, 8);
        timeSpnEnemyBoss = Random.Range(6, 12);
        isEnemyBossDied = true;
        countObjPlats = 0;
        Time.timeScale = 1;
        multiPly = 1;

		Buttons.toFade = true;
		isEndAndWall = false;
		showBonus_2x.gameObject.SetActive(false);
		showBonus_3x.gameObject.SetActive(false);
		timer_multiply = 0;
		ParticleController.isParticleSysActivated = true;
		isUpgradeCube = false;
		statusRewardedAds = -1;
		bonusRewardedAds.gameObject.SetActive(true);
		BonusRewardedAds.isRestart = true;
		isUpdateColor = false;
    }
	
	private void SaveData(){
		if (PlayerCntrl.distance > 60f){difficultyFactor += 0.1f;}
		else {difficultyFactor -= 0.1f;}
		if (difficultyFactor > 5f){difficultyFactor = 5f;}
		if (difficultyFactor < 1f){difficultyFactor = 1f;}
		PlayerPrefs.SetFloat("diffFactor", difficultyFactor);

		PlayerCntrl.moneyAll += PlayerCntrl.money;
		PlayerPrefs.SetInt ("Money", PlayerCntrl.moneyAll);

		int[] coefs1 = {1, 1, 2, 2, 1, 3};
		int coef1 = coefs1 [Random.Range (0, coefs1.Length)];
		int[] coefs2 = {1, 1, 2, 2, 1, 3, 4, 1, 3, 2};
		int coef2 = coefs2 [Random.Range (0, coefs2.Length)];
		PlayerCntrl.energyPoints += (PlayerCntrl.money*coef2 + PlayerCntrl.distance*coef1);
		PlayerPrefs.SetInt ("EnergyPs", PlayerCntrl.energyPoints);
		GPServices.Instance.SaveData();
		GPServices.PostToLeaderBoard(PlayerCntrl.energyPoints);

		PlayerCntrl.distRec = PlayerPrefs.GetInt("recDist", 0);
		if (PlayerCntrl.distance > PlayerCntrl.distRec){
			PlayerCntrl.distRec = PlayerCntrl.distance;
			PlayerPrefs.SetInt("recDist", PlayerCntrl.distRec);
		}

		if (PlayerCntrl.distance > 100) GPServices.PostAchievement(GPGSIds.achievement_reach_100_points_in_the_game);
		if (PlayerCntrl.distance > 200) GPServices.PostAchievement(GPGSIds.achievement_reach_200_points_in_the_game);
		if (PlayerCntrl.distance > 300) GPServices.PostAchievement(GPGSIds.achievement_reach_300_points_in_the_game);
		if (PlayerCntrl.distance > 400) GPServices.PostAchievement(GPGSIds.achievement_reach_400_points_in_the_game);
		if (PlayerCntrl.distance > 500) GPServices.PostAchievement(GPGSIds.achievement_reach_500_points_in_the_game);
		if (PlayerCntrl.distance > 600) GPServices.PostAchievement(GPGSIds.achievement_reach_600_points_in_the_game);
		if (PlayerCntrl.distance > 700) GPServices.PostAchievement(GPGSIds.achievement_reach_700_points_in_the_game);
		if (PlayerCntrl.distance > 800) GPServices.PostAchievement(GPGSIds.achievement_reach_800_points_in_the_game);
		if (PlayerCntrl.distance > 900) GPServices.PostAchievement(GPGSIds.achievement_reach_900_points_in_the_game);
		if (PlayerCntrl.distance > 1000) GPServices.PostAchievement(GPGSIds.achievement_reach_1000_points_in_the_game);
	
		if (PlayerCntrl.energyPoints >= 1000000){
			isEndGame = 1;
			PlayerPrefs.SetInt("IsEndGame", isEndGame);
			GPServices.PostAchievement(GPGSIds.achievement____collect_1000000_energy_points);
			GPServices.PostAchievement(GPGSIds.achievement_end_of_the_game__find_a_way_out);
		}
	}

	static public void ResetData(){
		PlayerCntrl.moneyAll = 0;
		PlayerPrefs.SetInt ("Money", PlayerCntrl.moneyAll);
        PlayerCntrl.distRec = 0;
        PlayerPrefs.SetInt("recDist", PlayerCntrl.distRec);
		PlayerCntrl.energyPoints = 0;
		PlayerPrefs.SetInt ("EnergyPs", PlayerCntrl.energyPoints);
		Data.Instance.currentType = 0;
		PlayerPrefs.SetInt ("typeElement", Data.Instance.currentType);
		for (int i = 0; i < Data.Instance.countElements; i++) {
			Data.Instance.isBought [i] = 0;
			Data.Instance.isPowerLimit [i] = 0;
		}
		Data.Instance.isBought [0] = 1;
		Data.Instance.isPowerLimit[0] = 1;
		for (int i = 0; i < Data.Instance.countElements; i++) {
			PlayerPrefs.SetInt ("isBought-" + i, Data.Instance.isBought[i]);
			PlayerPrefs.SetInt ("isPower-" + i, Data.Instance.isPowerLimit[i]);
		}
		isEndGame = 0;
		PlayerPrefs.SetInt("IsEndGame", isEndGame);
		AdsControll.isResettingAds = true;
		PlayerPrefs.SetInt("FirstRun", 1);
		PlayerPrefs.SetFloat("diffFactor", 1f);
	} // end of ResetData()
} // end of class Main
