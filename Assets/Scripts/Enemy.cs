using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 * Все настройки для любого типа врага
*/
public class Enemy : MonoBehaviour {
	private float speed;
    private float timeToDestroy;
    float rand;
    bool _isLeft;

	private void Start(){
		tag = "Enemy";
        transform.position = new Vector3(Random.Range(-2.5f, 2.5f), -5.5f, 3f);
        rand = Random.Range(0f, 1f);
        _isLeft = true;
	}

    private void Update(){
		if (Main.gameStatus == 1) {
            speed = Main.speedOfGame;
            
            if(rand > 0.51f){
                if (_isLeft && transform.position.x > -2.1f){
                    transform.position = new Vector3(transform.position.x - 0.03f*speed*Time.deltaTime, transform.position.y + 0.01f*speed*Time.deltaTime, transform.position.z);
                } if(!_isLeft && transform.position.x < 2.1f){
                    transform.position = new Vector3(transform.position.x + 0.03f*speed*Time.deltaTime, transform.position.y + 0.01f*speed*Time.deltaTime, transform.position.z);
                }
            } else{
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f*speed*Time.deltaTime, transform.position.z);
            }
            if(transform.position.x <= -2.1f) _isLeft = false;
            if(transform.position.x >= 2.1f) _isLeft = true;

            if(transform.position.y >= 5.4f) Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D (Collider2D other){
        if (other.gameObject.tag == "Player"){
            if (!PlayerCntrl.tempShield){PlayerCntrl.isDamaged = true;}
            Destroy(gameObject);
        }
    }
}
