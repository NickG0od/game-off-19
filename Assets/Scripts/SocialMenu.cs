using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialMenu : MonoBehaviour
{
    public Text txtTitle;

    private void Update(){
       if (TurnerMenu.isChangeToSocial){
           TurnerMenu.isChangeToSocial = false;
       }
    }

    public void OnButtonFacebookClicked(){
       Debug.Log("link to facebook");
       Application.OpenURL("https://www.facebook.com/groups/wishwakegames/");
    }
    public void OnButtonInstaClicked(){
       Debug.Log("link to instagram");
       Application.OpenURL("https://www.instagram.com/wishwake/");
    }
    public void OnButtonTwitterClicked(){
        Debug.Log("link to twitter");
        Application.OpenURL("https://twitter.com/wishwakegames");
    }
    public void OnButtonVKClicked(){
        Debug.Log("link to VK");
        Application.OpenURL("https://vk.com/wishwakegames");
    }
    public void OnButtonYoutubeClicked(){
        Debug.Log("link to youtube");
        Application.OpenURL("https://www.youtube.com/channel/UCjkMmHRAnJzpIfBJmgpR2sw");
    }
}
