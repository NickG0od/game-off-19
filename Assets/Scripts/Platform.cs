using UnityEngine;

public class Platform : MonoBehaviour {
	public float speed;
	const float destroyHeight = 5.3f;
	private SpriteRenderer platColor;
    private Color32 colorOld, colorNew;
	private float timeColor;
	private bool isUpdateColor;

	virtual protected void Start(){
		tag = "Ground";
		platColor = GetComponent<SpriteRenderer> ();
		if (Main.countObjPlats < 4)
			Main.countObjPlats++;
		if (Main.countObjPlats == 1)
			transform.position = new Vector3 (0f, 0f, 4f);
		else if (Main.countObjPlats == 2)
			transform.position = new Vector3 (-2f, -1f, 4f);
		else if (Main.countObjPlats == 3)
			transform.position = new Vector3 (2f, -1f, 4f);
		else{transform.position = new Vector3(Random.Range(-2.2f, 2.2f), -5.5f, 4f);}
		transform.localScale = new Vector2 (transform.localScale.x + Random.Range(-0.05f, 0.1f), transform.localScale.y/2);
	}

	virtual protected void Update(){
		speed = Main.speedOfGame;
		if (Main.gameStatus == 1){
			MovePlatform ();
            if (isUpdateColor){
				timeColor += Time.deltaTime / 1.5f;
				platColor.color = Color.Lerp(colorOld, colorNew, timeColor);
				if (timeColor >= 1){isUpdateColor = false;}
			}

			if (transform.position.y >= destroyHeight){Destroy (gameObject);}
		}
		CheckColor (Main.colorRandom);
	}

	virtual protected void MovePlatform(){
		transform.position = new Vector3 (transform.position.x, transform.position.y + 0.01f * speed * Time.deltaTime, transform.position.z);
	}

    virtual protected void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "Enemy"){
            Destroy(other.gameObject);
        }
    }

    virtual protected void CheckColor (int c){
		colorOld =  platColor.color;
        switch (c){
            case 1: // белый
                colorNew = new Color32(230, 230, 230, 255);
                break;
            case 2: // красный
				colorNew = new Color32(230, 50, 0, 255);
                break;
            case 3: // зеленый
				colorNew = new Color32(50, 230, 0, 255);
                break;
            case 4: // синий
				colorNew = new Color32(0, 50, 230, 255);
                break;
            case 5: // жёлтый
				colorNew = new Color32(230, 230, 0, 255);
                break;
            default:
                break;
        }
		timeColor = 0f;
		isUpdateColor = true;
	}
}