using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRewardedAds : Bonus1_antiTrap
{
    private SpriteRenderer sprRend;
    public static bool isRestart;
    protected override void Start(){
        base.Start();
        sprRend = GetComponent<SpriteRenderer>();
        transform.position = new Vector3 (2f, -0.3f, 5f);
        isRestart = false;
        sprRend.color = new Color(sprRend.color.r, sprRend.color.g, sprRend.color.b, 0f);
        tag = "BonusRewardedAds";
    }
    protected override void Update(){
        speed = Main.speedOfGame;
		if (Main.gameStatus == 1) {
            transform.position = new Vector3 (transform.position.x, transform.position.y + 0.01f * speed * Time.deltaTime, transform.position.z);
            if (isRestart){
                transform.position = new Vector3 (2f, -0.3f, 5f);
                isRestart = false;
            }
            if (Main.statusRewardedAds == 1){sprRend.color = new Color(sprRend.color.r, sprRend.color.g, sprRend.color.b, 1f);}
            if (transform.position.y >= destroyHeight || Main.statusRewardedAds == 0 || Main.statusRewardedAds == 3){
                sprRend.color = new Color(sprRend.color.r, sprRend.color.g, sprRend.color.b, 0f);
                gameObject.SetActive(false);
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other){
        base.OnCollisionEnter2D(other);
    }
}
