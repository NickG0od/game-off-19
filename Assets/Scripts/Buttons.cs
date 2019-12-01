using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
    private bool flag;
	static public bool toFade;
	public Animation leftbutton;
	public Animation rightbutton;
	public Animation jumpbutton;

	public Image leftB;
	public Image rightB;
	public Image jumpB;
	static public bool flagPlayButton;

    private void Start()
    {
        flag = true;
		toFade = false;
		flagPlayButton = false;
    }

	private void Update() {
		if (toFade){
			if (leftbutton.isPlaying){
				leftbutton.Stop();
				rightbutton.Stop();
				jumpbutton.Stop();

				leftbutton.Play();
				rightbutton.Play();
				jumpbutton.Play();
			} else
			{
				leftbutton.Play();
				rightbutton.Play();
				jumpbutton.Play();
			}
			
			toFade = false;
		}
	}

    private IEnumerator wait_flag (float s)
    {
        yield return new WaitForSeconds(s);
        flag = true;
		flagPlayButton = false;
    }

	public void StartGame(){
		if (!Main.goToStart && flag && TurnerMenu.isMainMenu) {
			flagPlayButton = true;
			Main.goToStart = true;
			AudioController.goGameMusic = true;
            flag = false;
            StartCoroutine(wait_flag(1f));
		}
	}

    public void LeftButtonOnPressed (){
		PlayerCntrl.isLeft = true;
		PlayerCntrl.isRight = false;
		leftB.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
	}

	public void LeftButtonReleased (){
		PlayerCntrl.isLeft = false;
		leftB.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void RightButtonOnPressed (){
		PlayerCntrl.isLeft = false;
		PlayerCntrl.isRight = true;
		rightB.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
	}

	public void RightButtonReleased(){
		PlayerCntrl.isRight = false;
		rightB.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void JumpButtonPressed(){
		PlayerCntrl.isJump = true;
		jumpB.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
	}

	public void JumpButtonReleased(){
        //PlayerCntrl.isJump = true;
		jumpB.transform.localScale = new Vector3(1f, 1f, 1f);
	}
}