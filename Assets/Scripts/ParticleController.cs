using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem psEffect;
    static public bool isParticleSysActivated;
    private bool flag;
    private int colorCopy;
    private float alpha;
    private int randSeed;

    void Start(){
        isParticleSysActivated = false;
        flag = true;
        colorCopy = 0;
        randSeed = 0;
    }

    void Update(){
        if (Main.colorRandom != colorCopy){
            psEffect.Clear();
            colorCopy = Main.colorRandom;
            CheckColor(colorCopy);
        }
        if(isParticleSysActivated && flag){
            psEffect.Play();
            colorCopy = 0;
            flag = false;
        }
        if (!isParticleSysActivated && !flag){
            psEffect.Stop();
            psEffect.Clear();
            flag = true;
        }
    }

    private void CheckColor (int c){
        var main = psEffect.main;
        var emission = psEffect.emission;
        alpha = 1f;
        randSeed = Random.Range(2, 6);
        main.startSpeed = new ParticleSystem.MinMaxCurve(randSeed-1, randSeed);
        emission.rateOverTime = 10*randSeed;
        switch (c){
            case 1: // Белый
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.78f, 0.78f, 0.78f, alpha), new Color(0.9f, 0.9f, 0.9f, alpha));
                break;
            case 2: // Красный
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.78f, 0.2f, 0f, alpha), new Color(0.9f, 0.2f, 0f, alpha));
                break;
            case 3: // Зеленый
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.2f, 0.78f, 0f, alpha), new Color(0.2f, 0.9f, 0f, alpha));
                break;
            case 4: // Синий
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(0f, 0.2f, 0.78f, alpha), new Color(0f, 0.2f, 0.9f, alpha));
                break;
            case 5: // Жёлтый
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.78f, 0.78f, 0f, alpha), new Color(0.9f, 0.9f, 0f, alpha));
                break;
            default:
                break;
        }
    }
}
