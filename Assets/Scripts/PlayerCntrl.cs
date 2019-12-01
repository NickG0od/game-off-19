using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerCntrl : MonoBehaviour {
	static public float health; // кол - во жизней в режиме бесконечности
    private float max_health;
	private float speed;
	[SerializeField] private float jumpStr;
    private int powerEff;
    static public int powerEff_copy; // постоянный урон по enemyMoveable
	private Rigidbody2D rb;
	static public int money; // кол - во денег в режиме бесконечности
	static public int distance; // кол - во расстояния / очков в режиме бесконечности
    static public int distRec; // рекорд очков
	static public int energyPoints;
	static public int moneyAll;
	float timeAddDist; // Время для добавления очков (дистанция = очки)
	// для управления -- флаги на кнопки
	static public bool isLeft;
	static public bool isRight;
	static public bool isJump;
	private bool isDeathLine;
    public GameObject face;
    Sprite sprFaceIdle;
    Sprite sprFaceJump;
    Sprite sprFaceDamaged;
    public GameObject texture;
    Sprite sprTexture;
	public static bool isDamaged;
    private bool isFalling;
    private int countJumps;
    private int currentJumps;
	private float timerJump;
    static public bool tempShield;
    private float time_shield;
    public Animation effect;
    private int x2Upgrade;
    public GameObject upradeEffect;

    private IEnumerator WaitJumper (float t){
        yield return new WaitForSeconds(t);
        currentJumps = countJumps;
    }
    private IEnumerator JumpDelay (float t){
        yield return new WaitForSeconds(t);
        currentJumps -= 1;
    }

	void Start () {
		isDeathLine = false;
		isRight = false;
		isLeft = false;
        isJump = false; // static
        timerJump = 0.05f;
        countJumps = 3;
        currentJumps = countJumps;
        rb = GetComponent<Rigidbody2D> ();
		sprFaceIdle = Resources.Load("Player/faceIdle", typeof(Sprite)) as Sprite;
        sprFaceJump = Resources.Load("Player/faceJump", typeof(Sprite)) as Sprite;
        sprFaceDamaged = Resources.Load("Player/faceDamaged", typeof(Sprite)) as Sprite;
        sprTexture = Resources.Load("Player/playerTexture-" + Data.Instance.currentType, typeof(Sprite)) as Sprite;
        tag = "Player";
		money = 0;
		distance = 0;
		transform.position = new Vector3 (0f, 2.7f, 1f);
		timeAddDist = 0.5f;
        texture.GetComponent<SpriteRenderer>().sprite = sprTexture;
		face.GetComponent<SpriteRenderer>().sprite = sprFaceIdle;
		health = Data.Instance.healthElement [Data.Instance.currentType];
        max_health = health;
        powerEff = Data.Instance.powerEffElement[Data.Instance.currentType]; // мощь ГГ против врагов
        powerEff_copy = powerEff;
		speed = Data.Instance.speedElement [Data.Instance.currentType];
		isDamaged = false;
        tempShield = false;
        time_shield = 1f;
        isFalling = true;
        effect.gameObject.SetActive(false);
        if (Main.isUpgradeCube){
            upradeEffect.SetActive(true);
            x2Upgrade = 2;
        }
        else {
            upradeEffect.SetActive(false);
            x2Upgrade = 1;
        }
        jumpStr = 17f;
	}

	void Update () {
		if (Main.gameStatus == 1){
            if (Main.isUpgradeCube){upradeEffect.SetActive(true);}
            else {upradeEffect.SetActive(false);}

			timeAddDist -= Time.deltaTime; // Искуственный счётчик очков
			if (timeAddDist <= 0) {
				distance++;
				timeAddDist = 1.0f;
			}

			if (isJump) { // прыжок ГГ
                StartCoroutine(JumpDelay(0.02f));
                if (currentJumps > 1)
                    rb.velocity = new Vector2(rb.velocity.x, jumpStr);
				isJump = false;
			}
			if (isDamaged){
                if(Main.gameStatus == 1){CamerShake.Shake (0.2f, 0.3f);}
                health -= max_health/(Random.Range(8f, 12f)-(Main.difficultyFactor-1));
                AudioController.playDamage = true;
                tempShield = true;
                isDamaged = false;
			}

            Controlling(); // Функция управления кубом : клавиатура + touch
            
            if (health <= 0 || isDeathLine){ // смерть - уничтожение кубика
				health = 0;
				isDeathLine = false;
				Main.gameStatus = 0;
                AdsControll.distPlayerDeath = distance + AdsControll.distToAdd;
				Destroy (gameObject);
			}
            if (tempShield){ // Откл-е временной неуязвимости:
                time_shield -= Time.deltaTime;
                face.GetComponent<SpriteRenderer>().sprite = sprFaceDamaged;
                texture.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 0.7f);
                if (time_shield <= 0){
                    tempShield = false;
                    time_shield = 1f;
                    texture.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 1f);
                }
            } else{
                if (isFalling){face.GetComponent<SpriteRenderer>().sprite = sprFaceJump;}
                else{face.GetComponent<SpriteRenderer>().sprite = sprFaceIdle;}
            }
		}
	}

	private void Controlling(){
        if (Input.GetKeyDown (KeyCode.LeftArrow)){isLeft = true;}
        if (Input.GetKeyUp (KeyCode.LeftArrow)){isLeft = false;}
        if (Input.GetKeyDown (KeyCode.RightArrow)){isRight = true;}
        if (Input.GetKeyUp (KeyCode.RightArrow)){isRight = false;}
		if (Input.GetKeyUp(KeyCode.UpArrow)){isJump = true;}
		
        if (isRight){rb.velocity = new Vector2 (speed, rb.velocity.y);}
		if (isLeft){rb.velocity = new Vector2 (-speed, rb.velocity.y);}
	}

	private void OnCollisionEnter2D (Collision2D other){
		if (other.gameObject.tag == "TrapDown") {
			isDeathLine = true; // Мгновенная смерть.
		}
	}

    private void OnCollisionStay2D(Collision2D other){
        if (other.gameObject.tag == "Ground"){
            isFalling = false;
            StartCoroutine(WaitJumper(timerJump));
        }   
    }

    private void OnCollisionExit2D (Collision2D other){
		if (other.gameObject.tag == "Ground") {
            isFalling = true;
		}
	}

	private void OnTriggerEnter2D (Collider2D other){
        if (other.gameObject.name == "wall_left")
            if (Main.isEndGame == 0){transform.position = new Vector3 (-(other.transform.position.x+1.02f), transform.position.y+0.03f, transform.position.z);}
            else {Main.isEndAndWall = true;}
		if (other.gameObject.name == "wall_right")
            if (Main.isEndGame == 0){transform.position = new Vector3 (-(other.transform.position.x-1.02f), transform.position.y+0.03f, transform.position.z);}
            else {Main.isEndAndWall = true;}

        if (other.gameObject.tag == "Coin"){
            AudioController.playMoney = true;
            money += Random.Range(1, 5)*Main.multiPly*x2Upgrade;
            effect.gameObject.SetActive(true);
            effect.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 0, 255);
            effect.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Bonus1"){ // Подобран бонус: открытие ловушки:
            AudioController.playBonus = true;
			Main.isRestartTrapPastb = true;
            effect.gameObject.SetActive(true);
            effect.gameObject.GetComponent<SpriteRenderer>().color = new Color32(230, 0, 255, 255);
            effect.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Bonus2"){ // Подобран бонус: увеличение здоровья:
            AudioController.playBonus = true;
            float tempHP = (max_health/(Random.Range(6f, 10f)+(Main.difficultyFactor-1)))*x2Upgrade;
            if ((health + tempHP) > max_health){health = max_health;}     
            else{health += tempHP;}
            effect.gameObject.SetActive(true);
            effect.gameObject.GetComponent<SpriteRenderer>().color = new Color32(230, 0, 255, 255);
            effect.Play();
            Destroy(other.gameObject);
        }
        // Подобран бонус: замедление времени:
        /*if (other.gameObject.tag == "Bonus3")
        {
            AudioController.playBonus = true;
            effect.gameObject.SetActive(true);
            effect.gameObject.GetComponent<SpriteRenderer>().color = new Color32(230, 0, 255, 255);
            effect.Play();
            Destroy(other.gameObject);
        }*/
        if (other.gameObject.tag == "Bonus3"){ // Подобран бонус: 2х мультипликатор
            AudioController.playBonus = true;
            Main.timer_multiply = 10f; // время жизни бонуса
            Main.multiPly = 2;
            effect.gameObject.SetActive(true);
            effect.gameObject.GetComponent<SpriteRenderer>().color = new Color32(230, 0, 255, 255);
            effect.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Bonus4"){ // Подобран бонус: 3х мультипликатор
            AudioController.playBonus = true; 
            Main.timer_multiply = 10f; // время жизни бонуса
            Main.multiPly = 3;
            effect.gameObject.SetActive(true);
            effect.gameObject.GetComponent<SpriteRenderer>().color = new Color32(230, 0, 255, 255);
            effect.Play();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "BonusRewardedAds"){
            if (Main.statusRewardedAds == 1){Main.statusRewardedAds = 2;}
        }
        // Функционал, связанный с EnemyBoss:
        if (other.gameObject.tag == "EnemyBoss" && EnemyBoss.statusCollision == 0){
            EnemyBoss.statusCollision = 1;
            EnemyBoss.hpLost = powerEff*x2Upgrade;
        }
        if (other.gameObject.tag == "Thorn" && EnemyBoss.statusCollision == 0){
            EnemyBoss.statusCollision = -1;
            if (!tempShield){isDamaged = true;}
        }
    }

    private void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "TrapDown" || other.gameObject.tag == "TrapUp"){
			health -= Main.difficultyFactor;
            if(Main.gameStatus == 1){CamerShake.Shake (0.2f, 0.3f);}
            AudioController.playDamage = true;
            tempShield = true;
		}
	}
}
