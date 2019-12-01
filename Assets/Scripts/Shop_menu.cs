using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_menu : MonoBehaviour {

    public Text _money;
    public Text _powerps;

    public GameObject forLockCubes;
    public Image texturePl;
    public Animation animChangePlayer;
    public Animation animChangePlayerBox;
    public Animation animChangePlayerFace;
    public Text txtCost;
    public Image imgHealthUp;
    public Image imgHealthDown;
    public Image imgDamageUp;
    public Image imgDamageDown;
    public Image imgSpeedUp;
    public Image imgSpeedDown;
    public Button btnBuy;
    private int elementCursor;
    private bool isAnimationStp;

    private IEnumerator changeColor(float time)
    {
        yield return new WaitForSeconds(time);
        texturePl.GetComponent<Image>().sprite = Resources.Load("Player/playerTexture-" + elementCursor, typeof(Sprite)) as Sprite;
    }

    private void Start()
    {
        isAnimationStp = true;
    }

    private void Update () {
        if (!animChangePlayer.isPlaying)
            isAnimationStp = true;
        else
            isAnimationStp = false;

        _money.text = "  x " + PlayerCntrl.moneyAll.ToString();
        _powerps.text = "  x " + PlayerCntrl.energyPoints.ToString();

        if (TurnerMenu.isChangeToShop)
        {
            elementCursor = Data.Instance.currentType;
            ChangeSets(0.0f);
            TurnerMenu.isChangeToShop = false;
        }
	}

    public void OnButtonRightShop()
    {
        if (isAnimationStp)
        {
            animChangePlayer.CrossFade("changePlayerL");
            animChangePlayerBox.Play();
            animChangePlayerFace.Play();
            elementCursor++;
            if (elementCursor >= Data.Instance.countElements)
                elementCursor = 0;
            ChangeSets(0.15f);
        }
    }

    public void OnButtonLeftShop()
    {
        if (isAnimationStp)
        {
            animChangePlayer.CrossFade("changePlayerR");
            animChangePlayerBox.Play();
            animChangePlayerFace.Play();
            elementCursor--;
            if (elementCursor < 0)
                elementCursor = Data.Instance.countElements - 1;
            ChangeSets(0.15f);
        }
    }

    public void OnButtonShopBuyClicked()
    {
        if (Data.Instance.isBought[elementCursor] == 0)
        {
            if (PlayerCntrl.moneyAll >= Data.Instance.costElement[elementCursor] && PlayerCntrl.energyPoints >= Data.Instance.powerCostElement[elementCursor])
            {
                PlayerCntrl.moneyAll -= Data.Instance.costElement[elementCursor];
                Data.Instance.isBought[elementCursor] = 1;
                Data.Instance.currentType = elementCursor;
                PlayerPrefs.SetInt("Money", PlayerCntrl.moneyAll);
                PlayerPrefs.SetInt("typeElement", Data.Instance.currentType);
            }
        }
        else
        {
            if (elementCursor != Data.Instance.currentType)
            {
                Data.Instance.currentType = elementCursor;
                PlayerPrefs.SetInt("typeElement", Data.Instance.currentType);
            }
        }
        ChangeSets(0.1f);
    }

    public void OnButtonBuyPointerDown(){
        txtCost.color = new Color32(225, 225, 225, 200);
    }

    public void OnButtonBuyPointerUp(){
        txtCost.color = new Color32(225, 225, 225, 255);
    }

    private void ChangeSets(float t)
    {
        // изменить цвет куба
        StartCoroutine(changeColor(t));

        float healthChosenDelta = 150f / Data.Instance.health_Max;
        float damageChosenDelta = 150f / Data.Instance.powerEff_Max;
        float speedChosenDelta = 150f / Data.Instance.speed_Max;
        if (Data.Instance.healthElement[elementCursor] >= Data.Instance.healthElement[Data.Instance.currentType]){
            imgHealthDown.color = new Color32(90, 240, 40, 255);
            imgHealthUp.color = new Color32(255, 255, 255, 255);
            imgHealthUp.rectTransform.sizeDelta = new Vector2(healthChosenDelta * Data.Instance.healthElement[Data.Instance.currentType], 12f);
            imgHealthDown.rectTransform.sizeDelta = new Vector2(healthChosenDelta * Data.Instance.healthElement[elementCursor], 12f);
        } else{
            imgHealthUp.color = new Color32(255, 255, 255, 255);
            imgHealthDown.color = new Color32(240, 40, 40, 255);
            imgHealthDown.rectTransform.sizeDelta = new Vector2(healthChosenDelta * Data.Instance.healthElement[Data.Instance.currentType], 12f);
            imgHealthUp.rectTransform.sizeDelta = new Vector2(healthChosenDelta * Data.Instance.healthElement[elementCursor], 12f);
        } if (Data.Instance.powerEffElement[elementCursor] >= Data.Instance.powerEffElement[Data.Instance.currentType]){
            imgDamageDown.color = new Color32(90, 240, 40, 255);
            imgDamageUp.color = new Color32(255, 255, 255, 255);
            imgDamageUp.rectTransform.sizeDelta = new Vector2(damageChosenDelta * Data.Instance.powerEffElement[Data.Instance.currentType], 12f);
            imgDamageDown.rectTransform.sizeDelta = new Vector2(damageChosenDelta * Data.Instance.powerEffElement[elementCursor], 12f);
        } else{
            imgDamageUp.color = new Color32(255, 255, 255, 255);
            imgDamageDown.color = new Color32(240, 40, 40, 255);
            imgDamageDown.rectTransform.sizeDelta = new Vector2(damageChosenDelta * Data.Instance.powerEffElement[Data.Instance.currentType], 12f);
            imgDamageUp.rectTransform.sizeDelta = new Vector2(damageChosenDelta * Data.Instance.powerEffElement[elementCursor], 12f);
        } if (Data.Instance.speedElement[elementCursor] >= Data.Instance.speedElement[Data.Instance.currentType]){
            imgSpeedDown.color = new Color32(90, 240, 40, 255);
            imgSpeedUp.color = new Color32(255, 255, 255, 255);
            imgSpeedUp.rectTransform.sizeDelta = new Vector2(speedChosenDelta * Data.Instance.speedElement[Data.Instance.currentType], 12f);
            imgSpeedDown.rectTransform.sizeDelta = new Vector2(speedChosenDelta * Data.Instance.speedElement[elementCursor], 12f);
        } else{
            imgSpeedUp.color = new Color32(255, 255, 255, 255);
            imgSpeedDown.color = new Color32(240, 40, 40, 255);
            imgSpeedDown.rectTransform.sizeDelta = new Vector2(speedChosenDelta * Data.Instance.speedElement[Data.Instance.currentType], 12f);
            imgSpeedUp.rectTransform.sizeDelta = new Vector2(speedChosenDelta * Data.Instance.speedElement[elementCursor], 12f);
        }

        if (Data.Instance.isBought[elementCursor] == 0){
            btnBuy.interactable = true;
            txtCost.text = Data.Instance.costElement[elementCursor].ToString();
            if (PlayerCntrl.moneyAll < Data.Instance.costElement[elementCursor])
                btnBuy.GetComponent<Image>().color = new Color32(255, 255, 255, 135);
            else
                btnBuy.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } else{
            btnBuy.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if (elementCursor == Data.Instance.currentType){
                LocalizationFile.numShopOption = 1;
                LocalizationFile.isLangChanged = true; 
                btnBuy.interactable = false;
            } else{
                LocalizationFile.numShopOption = 2;
                LocalizationFile.isLangChanged = true; 
                btnBuy.interactable = true;
            }
        }
        if (Data.Instance.powerCostElement[elementCursor] <= PlayerCntrl.energyPoints || Data.Instance.isBought[elementCursor] == 1)
            Data.Instance.isPowerLimit[elementCursor] = 1;
        if (Data.Instance.isPowerLimit[elementCursor] == 0){
            LocalizationFile.pointsShop = Data.Instance.powerCostElement[elementCursor].ToString();
            btnBuy.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            LocalizationFile.numShopOption = 3;
            LocalizationFile.isLangChanged = true; 
            forLockCubes.gameObject.SetActive(true);
        } else{
            forLockCubes.gameObject.SetActive(false);
        }
    }
}
