using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioS;
    private bool in_menu;
    private bool isPause;
    public AudioClip _music_inmenu;
    public AudioClip[] _music_ingame;
    public AudioClip _sound_bonus;
    public AudioClip _sound_money;
    public AudioClip _sound_damage;
    public AudioClip _boss_hurted;
    public AudioClip _boss_died_1;
    static public bool goGameMusic;
    static public bool goMenuMusic;
    static public bool pauseAudio;
    static public bool playBonus;
    static public bool playMoney;
    static public bool playDamage;
    static public bool isBossHurted;
    static public bool isBossDied;
    static public int numBoss;
    private bool sound_damage_delay;
    private int musicNum;

    private void Start() {
        audioS = gameObject.GetComponent<AudioSource>();
        audioS.pitch = 1.0f;
        in_menu = true;
        isPause = false;
        pauseAudio = false;
        goGameMusic = false;
        goMenuMusic = false;
        playBonus = false;
        playMoney = false;
        playDamage = false;
        isBossHurted = false;
        isBossDied = false;
        numBoss = 1;
        sound_damage_delay = false;
        musicNum = 0;
    }

    private void Update() {
        audioS.volume = Main.musicValue;
        if (Main.gameStatus == 0){ // При смерти ГГ или в меню
            playDamage = false;
            playMoney = false;
            playBonus = false;
            if (!audioS.isPlaying && in_menu && Main.startScreenEnded){
                audioS.clip = _music_inmenu;
                audioS.Play();
            }
            if(pauseAudio)
                audioS.Pause();
            if (!in_menu)
                audioS.Stop();
        }
        if (Main.gameStatus == 1){ // В самой игре
            if(!audioS.isPlaying && !in_menu){
                if(isPause){
                    audioS.Play();
                    isPause = false;
                } else {
                    int rand = Random.Range(0,12);
                    if (rand == musicNum){
                        rand += 1;
                        if (rand == _music_ingame.Length){rand = 0;}
                    }
                    musicNum = rand;
                    switch(rand){
                        case 0:
                            audioS.clip = _music_ingame[0];
                            break;
                        case 1:
                            audioS.clip = _music_ingame[1];
                            break;
                        case 2:
                            audioS.clip = _music_ingame[2];
                            break;
                        case 3:
                            audioS.clip = _music_ingame[3];
                            break;
                        case 4:
                            audioS.clip = _music_ingame[4];
                            break;
                        case 5:
                            audioS.clip = _music_ingame[5];
                            break;
                        case 6:
                            audioS.clip = _music_ingame[6];
                            break;
                        case 7:
                            audioS.clip = _music_ingame[7];
                            break;
                        case 8:
                            audioS.clip = _music_ingame[8];
                            break;
                        case 9:
                            audioS.clip = _music_ingame[9];
                            break;
                        case 10:
                            audioS.clip = _music_ingame[10];
                            break;
                        case 11:
                            audioS.clip = _music_ingame[11];
                            break;
                        default:
                            break;
                    }
                    audioS.Play();
                }
            }
        }
        if(Main.gameStatus == 2){
            audioS.Pause();
            isPause = true;
        }
        if(goMenuMusic){
            audioS.Stop();
            in_menu = true;
            goMenuMusic = false;
        }
        if(goGameMusic){
            audioS.Stop();
            in_menu=false;
            isPause = false;
            goGameMusic = false;
        }
        // Функционал звуков в игре (бонусы, монеты, ГГ ранения)
        if(playBonus && Main.gameStatus == 1){
            audioS.PlayOneShot(_sound_bonus, Main.soundValue/1.4f);
            playBonus = false;
        }
         if(playMoney && Main.gameStatus == 1){
            audioS.PlayOneShot(_sound_money, Main.soundValue/1.4f);
            playMoney = false;
        }
        if(playDamage && Main.gameStatus == 1 && !sound_damage_delay){
            audioS.PlayOneShot(_sound_damage, Main.soundValue/1.4f);
            playDamage = false;
            StartCoroutine(SoundDamageDelay());
        }
        if (sound_damage_delay && playDamage){
            playDamage = false;
        }

        if (isBossHurted){
            switch(numBoss){
                case 1:
                    //audioS.PlayOneShot(_boss_hurted, Main.soundValue/1.4f);
                    break;
                case 2:
                    break;
                default:
                    break;
            }
            isBossHurted = false;
        }
        if (isBossDied && Main.gameStatus == 1){
            switch(numBoss){
                case 1:
                    audioS.PlayOneShot(_boss_died_1, Main.soundValue/1.4f);
                    break;
                case 2:
                    break;
                default:
                    break;
            }
            isBossDied = false;
        }
    }

    private IEnumerator SoundDamageDelay(){
        sound_damage_delay = true;
        yield return new WaitForSeconds(0.1f);
        sound_damage_delay = false;
    }
}
