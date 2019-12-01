using UnityEngine;

public class Bonus1_antiTrap : MonoBehaviour {
	protected float speed;
	protected const float destroyHeight = 5.2f;
    protected SpriteRenderer spr;

	 virtual protected void Start () {
        spr = GetComponent<SpriteRenderer>();
		tag = "Bonus1";
		transform.position = new Vector3 (Random.Range(-2f, 2f), -5.5f, 5f);
	}
	virtual protected void Update () {
		speed = Main.speedOfGame;
		if (Main.gameStatus == 1) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.01f * speed * Time.deltaTime * 1.2f, transform.position.z);
			if (transform.position.y >= destroyHeight)
				Destroy (gameObject);
		}
    }
	virtual protected void OnCollisionEnter2D (Collision2D other){
		if (other.gameObject.tag == "Diamond")
			Destroy (other.gameObject);
    }
}