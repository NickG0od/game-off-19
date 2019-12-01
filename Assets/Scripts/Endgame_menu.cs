using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Endgame_menu : MonoBehaviour
{
    public ParticleSystem ps;

    void Update()
    {
        if (TurnerMenu.isChangeToEndGame)
        {
            if (ps.isStopped)
                ps.Play();
        } else {
            if (ps.isPlaying)
                ps.Stop();
        }
    }
}
