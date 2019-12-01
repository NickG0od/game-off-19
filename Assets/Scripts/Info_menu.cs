using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info_menu : MonoBehaviour
{
 
  private void Update() {

  }

  public void OnButtonMusicClicked () {
      Application.OpenURL("http://www.nihilore.com/license");
  }
}
