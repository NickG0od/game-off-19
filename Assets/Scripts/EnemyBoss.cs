using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public static float health;
    private float maxHP;
    private float speed;
    private float shootReloading;
    private bool isPathDefined;
    private float pathX, pathY;
    private float waitingTime;
    public static int statusCollision; // 0: нет, -1: касание об шип, 1: об корпус.
    private float timeToChangeStatus;
    public static float hpLost;
    private int moveDirect; // 0:no; -1:left; 1:right
    public GameObject healthbar;
    public GameObject box;
    public GameObject[] thorns; // up; left; right
    public GameObject bullet;
    public ParticleSystem explodeParticles;
    public ParticleSystem hurtedParticles;
    public Animation[] anAfterCollisions; // box, face, thorns
    private bool isHurtedParticles;
    private void Start(){
        AudioController.numBoss = 1;     
        tag = "EnemyBoss";
        health = PlayerCntrl.powerEff_copy*(Random.Range(3, 6)+(Main.difficultyFactor-1));
        maxHP = health;
        hpLost = 0f;
        healthbar.gameObject.SetActive(false);
        shootReloading = 3f;
        transform.position = new Vector3(0f, 6f, 2f);
        isPathDefined = false;
        waitingTime = 0.5f;
        statusCollision = 0;
        moveDirect = 0;
        timeToChangeStatus = 2f;
        isHurtedParticles = true;

        for (int i = 0; i<thorns.Length; i++){thorns[i].gameObject.SetActive(false);}
        int typeThorns = Random.Range(0, 4);
        switch(typeThorns){
            case 0:
                thorns[0].gameObject.SetActive(true);
                break;
            case 1:
                thorns[0].gameObject.SetActive(true);
                thorns[1].gameObject.SetActive(true);
                break;
            case 2:
                thorns[0].gameObject.SetActive(true);
                thorns[2].gameObject.SetActive(true);
                break;
            case 3:
                thorns[1].gameObject.SetActive(true);
                thorns[2].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void Update(){
        if (Main.gameStatus == 1){
            speed = Main.speedOfGame * 1.1f;

            if (!isPathDefined){
                waitingTime -= Time.deltaTime;
                if (waitingTime <= 0){
                    waitingTime = Random.Range(1, 4);
                    pathX = Random.Range(-2f, 2f);
                    pathY = Random.Range(-4f, 4f);
                    isPathDefined = true;
                }
            }
            if(isPathDefined){
                if (transform.position.x < pathX){
                    moveDirect = 1;
                    if (transform.position.y < pathY){transform.position = new Vector3(transform.position.x + 0.01f * speed * Time.deltaTime, transform.position.y + 0.01f * speed * Time.deltaTime, transform.position.z);}
                    if (transform.position.y > pathY){transform.position = new Vector3(transform.position.x + 0.01f * speed * Time.deltaTime, transform.position.y - 0.01f * speed * Time.deltaTime, transform.position.z);}
                }
                if (transform.position.x > pathX){
                    moveDirect = -1;
                    if (transform.position.y < pathY){transform.position = new Vector3(transform.position.x - 0.01f * speed * Time.deltaTime, transform.position.y + 0.01f * speed * Time.deltaTime, transform.position.z);}
                    if (transform.position.y > pathY){transform.position = new Vector3(transform.position.x - 0.01f * speed * Time.deltaTime, transform.position.y - 0.01f * speed * Time.deltaTime, transform.position.z);}
                }
            }
            if ((transform.position.x >= pathX - 0.2f) && (transform.position.x <= pathX + 0.2f) && (transform.position.y >= pathY - 0.2f) && (transform.position.y <=pathY + 0.2f)){
                moveDirect = 0;
                isPathDefined = false;
            }

            if (moveDirect == 1){box.transform.Rotate(0f, 0f, 2f);}
            if (moveDirect == -1){box.transform.Rotate(0f, 0f, -2f);}

            if (statusCollision != 0){
                if (statusCollision == 1 && isHurtedParticles && health > 0){
                    Instantiate(hurtedParticles, transform.position, transform.rotation);
                    AudioController.isBossHurted = true;
                    isHurtedParticles = false;
                }
                for(int i = 0; i<anAfterCollisions.Length;i++){anAfterCollisions[i].Play();}
                timeToChangeStatus -= Time.deltaTime;
                if (timeToChangeStatus <=0){
                    statusCollision = 0;
                    for(int i = 0; i<anAfterCollisions.Length;i++){anAfterCollisions[i].Stop();}
                    transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                    timeToChangeStatus = 2f;
                    isHurtedParticles = true;
                }
            }

            if (moveDirect == 0 && statusCollision == 0){
                shootReloading -= Time.deltaTime;
                if (shootReloading <= 0){
                    Instantiate(bullet, transform.position, transform.rotation);
                    shootReloading = Random.Range(0.5f, 1f);
                }
            }

            if (hpLost != 0f){
                health -= hpLost;
                hpLost = 0f;
            }
            if (health < maxHP){
            healthbar.gameObject.SetActive(true);
            healthbar.GetComponent<Transform>().localScale = new Vector2(health / maxHP, 0.08f);
            }
            if (health <= 0){
                health = 0;
                Instantiate(explodeParticles, transform.position, transform.rotation);
                AudioController.isBossDied = true;
                Main.isEnemyBossDied = true;
                Destroy(gameObject);
            }
        }
    }
}