using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveable : Platform {
	private bool isRight;

    protected override void Start (){
		base.Start ();
		isRight = true;
		tag = "Ground";
	}

    protected override void Update (){
		base.Update ();
	}

    protected override void MovePlatform (){
		if (isRight){
			if (transform.position.x < 2.0f){transform.position = new Vector3 (transform.position.x + 0.01f * speed * Time.deltaTime, transform.position.y + 0.01f * speed * Time.deltaTime, transform.position.z);}
			else{isRight = false;}	
		}
		if (!isRight){
			if (transform.position.x > -2.0f){transform.position = new Vector3 (transform.position.x - 0.01f * speed * Time.deltaTime, transform.position.y + 0.01f * speed * Time.deltaTime, transform.position.z);}				
			else{isRight = true;}
		}
	}

    protected override void OnCollisionEnter2D(Collision2D other){
        base.OnCollisionEnter2D(other);
    }

    protected override void CheckColor (int c){
        base.CheckColor(c);
    }
}