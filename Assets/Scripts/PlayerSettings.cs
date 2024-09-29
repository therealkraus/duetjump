using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class that saves and loads player preferences
public class PlayerSettings : MonoBehaviour
{

    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private AudioSource myAudio;
    public AudioClip splatClip;
    public AudioClip piano1;
    public AudioClip piano2;
    public AudioClip piano3;
    public AudioClip piano4;
    public AudioClip piano5;
    public AudioClip piano6;
    public AudioClip piano7;
    public AudioClip piano8;
    public AudioClip piano9;
    public AudioClip piano10;
    public AudioClip piano11;
    public AudioClip piano12;
    public AudioClip piano13;
    public AudioClip piano14;
    public AudioClip piano15;
    public AudioClip piano16;
    public AudioClip piano17;
    public AudioClip piano18;
    public AudioClip piano19;
    public AudioClip piano20;
    public AudioClip piano21;
    public AudioClip piano22;
    public AudioClip piano23;
    public AudioClip piano24;
    public AudioClip piano25;
    public AudioClip piano26;
    public AudioClip piano27;
    public AudioClip piano28;
    public AudioClip piano29;


    public void Awake()
    {
        // Checks to see if player prefs has music enabled, if it does it plays music
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
            toggle.isOn = true;
            myAudio.enabled = true;
            PlayerPrefs.Save();
        }
        // Checks to see if player prefs has music disabled, if it does not play music
        else
        {
            if (PlayerPrefs.GetInt("music") == 0)
            {
                myAudio.enabled = false;
                toggle.isOn = false;
            }
            else
            {
                myAudio.enabled = true;
                toggle.isOn = true;
            }
        }
    }

    //Toggles music on or off based on toggle click, also saves the choice to player prefs
    public void ToggleMusic()
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("music", 1);
            myAudio.enabled = true;
        }
        else
        {
            PlayerPrefs.SetInt("music", 0);
            myAudio.enabled = false;
        }
        PlayerPrefs.Save();
    }

    public void PlaySound()
    {
       // myAudio.volume = 0.6f;
     //   myAudio.pitch = 1;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(splatClip);
    }

    public void PlayPianoSound1()
    {
     //   myAudio.volume = 1f;
        myAudio.pitch = 1;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano1);
    }

    public void PlayPianoSound2()
    {
     //   myAudio.volume = 0.9f;
        myAudio.pitch = 1.10f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano1);
    }

    public void PlayPianoSound3()
    {
      //  myAudio.volume = 0.8f;
        myAudio.pitch = 1.20f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano1);
    }

    public void PlayPianoSound4()
    {
      //  myAudio.volume = 0.7f;
        myAudio.pitch = 1.30f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano1);
    }

    public void PlayPianoSound5()
    {
      //  myAudio.volume = 0.6f;
        myAudio.pitch = 1.40f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano1);
    }
    public void PlayPianoSound6()
    {
       // myAudio.volume = 0.5f;
        myAudio.pitch = 1.50f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano1);
    }
    public void PlayPianoSound7()
    {
       // myAudio.volume = 0.5f;
        myAudio.pitch = 1.60f;
        //  myAudio.pitch = 1;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano7);
    }
    public void PlayPianoSound8()
    {
       // myAudio.volume = 0.5f;
        myAudio.pitch = 1.70f;
        //myAudio.pitch = 1;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano8);
    }
    public void PlayPianoSound9()
    {
       // myAudio.volume = 0.5f;
        myAudio.pitch = 1.80f;
        //  myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano9);
    }
    public void PlayPianoSound10()
    {
       // myAudio.volume = 0.5f;
        myAudio.pitch = 1.90f;
        // myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano10);
    }
    public void PlayPianoSound11()
    {
        // myAudio.pitch += 0.1f;
        myAudio.pitch = 2f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano11);
    }
    public void PlayPianoSound12()
    {
        // myAudio.pitch += 0.1f;
        myAudio.pitch = 2.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano12);
    }
    public void PlayPianoSound13()
    {
        // myAudio.p myAudio.pitch = 1.90f;itch += 0.1f;
        myAudio.pitch = 2.2f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano13);
    }
    public void PlayPianoSound14()
    {
        //myAudio.pitch += 0.1f;
        myAudio.pitch = 2.3f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano14);
    }
    public void PlayPianoSound15()
    {
        //myAudio.pitch += 0.1f;
        myAudio.pitch = 2.4f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano15);
    }
    public void PlayPianoSound16()
    {
        //myAudio.pitch += 0.1f;
        myAudio.pitch = 2.5f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano16);
    }
    public void PlayPianoSound17()
    {
        //myAudio.pitch += 0.1f;
        myAudio.pitch = 2.6f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano17);
    }
    public void PlayPianoSound18()
    {
        //myAudio.pitch += 0.1f;
        myAudio.pitch = 2.7f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano18);
    }
    public void PlayPianoSound19()
    {
        myAudio.pitch = 2.8f;
        //myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano19);
    }
    public void PlayPianoSound20()
    {
        myAudio.pitch = 2.90f;
        //myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano20);
    }
    public void PlayPianoSound21()
    {
        myAudio.pitch = 3f;
        //myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano21);
    }
    public void PlayPianoSound22()
    {
        //myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano22);
    }
    public void PlayPianoSound23()
    {
        //myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano23);
    }
    public void PlayPianoSound24()
    {
        //myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano24);
    }
    public void PlayPianoSound25()
    {
        //myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano25);
    }
    public void PlayPianoSound26()
    {
        //myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano26);
    }
    public void PlayPianoSound27()
    {
        //myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano27);
    }
    public void PlayPianoSound28()
    {
        //myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano28);
    }
    public void PlayPianoSound29()
    {
        //myAudio.pitch += 0.1f;
        if (myAudio.enabled == true)
            myAudio.PlayOneShot(piano29);
    }
}
