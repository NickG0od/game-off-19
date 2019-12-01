using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSshow : MonoBehaviour {
	int accumulator = 0;
	int counter = 0;
	float timer = 0f;
	public Text ttt;

	void Update() {
		accumulator++;
		timer += Time.deltaTime;

		if (timer >= 1) {
			timer = 0;
			counter = accumulator;
			accumulator = 0;
		}

		ttt.text = "FPS: " + counter;
	}
}