using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
	public GameObject t_up;
	public GameObject t_down;
	private SpriteRenderer spUp;
	private SpriteRenderer spDown;
	private float speed;

	void Start (){
		spUp = t_up.GetComponent<SpriteRenderer> ();
		spDown = t_down.GetComponent<SpriteRenderer> ();
		t_up.transform.position = new Vector3 (0f, 7.7f, t_up.transform.position.z);
		t_down.transform.position = new Vector3 (0f, -8.9f, t_down.transform.position.z);
	}

	void Update (){
		speed = Main.speedOfGame / 10.0f;
		if (Main.gameStatus == 1) {
			t_up.transform.position = new Vector3 (t_up.transform.position.x, t_up.transform.position.y - 0.01f * speed * Time.deltaTime, t_up.transform.position.z);
			t_down.transform.position = new Vector3 (t_down.transform.position.x, t_down.transform.position.y + 0.01f * speed * Time.deltaTime, t_down.transform.position.z);

		}

		if (Main.isRestartTrapPastb){
			t_up.transform.position = new Vector3 (0f, 7.7f, t_up.transform.position.z);
			t_down.transform.position = new Vector3 (0f, -8.9f, t_down.transform.position.z);
			Main.isRestartTrapPastb = false;
		}
		if (Main.isChangeColor){Main.isChangeColor = false;}
	}
}