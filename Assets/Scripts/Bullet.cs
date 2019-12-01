using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float timeDestroy;
    private GameObject player;
    private Vector3 direction;

    private void Start(){
        transform.position = new Vector3(transform.position.x, transform.position.y, 6f);
        player = GameObject.FindGameObjectWithTag("Player");
        if (player){
            var heading = player.transform.position - transform.position;
            var distance = heading.magnitude;
            direction = heading/distance;
        }
        speed = 400f;
        timeDestroy = speed/100f;
    }

    private void Update(){
        if (Main.gameStatus == 1){
            transform.position += new Vector3(direction.x*0.01f*speed*Time.deltaTime, direction.y*0.01f*speed*Time.deltaTime, 0f);
            timeDestroy -= Time.deltaTime;
            if (timeDestroy <= 0){Destroy(gameObject);}
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            if (!PlayerCntrl.tempShield){PlayerCntrl.isDamaged = true;}
            Destroy(gameObject);
        }
    }
}
