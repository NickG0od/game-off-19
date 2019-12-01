using UnityEngine;

public class CoinSet : MonoBehaviour {
	private float speed;
	const float destroyHeight = 5.3f;

	void Start () {
		tag = "Coin";
		transform.position = new Vector3 (Random.Range(-2f, 2f), -5.5f, 5f);
	}

	void Update () {
		speed = Main.speedOfGame;
		if (Main.gameStatus == 1) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.01f * speed * Time.deltaTime * 1.4f, transform.position.z);
		    if (transform.position.y >= destroyHeight)
			    Destroy (gameObject);
        }
	}
}
