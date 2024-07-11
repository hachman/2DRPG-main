using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Introduction : MonoBehaviour
{
    [SerializeField] Camera camera;
    //[SerializeField] AudioSource BGM;
    [SerializeField] AudioSource Narator;
    [SerializeField] AudioClip[] VoiceOver;
    /*
    First Frame - The sound of laughter...hurriedto their next class    Frame E
    Second Frame + Third Frame - but for one boy...background static          F - G
    Fourth Frame - Jack was a loner...filling in                              H
    Fifth Frame - with his classmate...video games                            I
    Sixth Frame -> Eleventn Frame - One day... dream world                    J - K - L - M - N - O
    Twelveth Frame + Thirtinth Frame - The world...eye could see              P - Q
    Fourtinth Frame + Fiftinth Frame - Jack was confused...in this place      R - S
    Sixtinth Frame - ...dream world                                           T
    
    [Header("Frames")]
    [SerializeField] GameObject Frame1;
    [SerializeField] GameObject Frame2;
    [SerializeField] GameObject Frame3;
    [SerializeField] GameObject Frame4;
    [SerializeField] GameObject Frame5;
    [SerializeField] GameObject Frame6;
    [SerializeField] GameObject Frame7;
    [SerializeField] GameObject Frame8;
    [SerializeField] GameObject Frame9;
    [SerializeField] GameObject Frame10;
    [SerializeField] GameObject Frame11;
    [SerializeField] GameObject Frame12;
    [SerializeField] GameObject Frame13;
    [SerializeField] GameObject Frame14;
    [SerializeField] GameObject Frame15;
    [SerializeField] GameObject Frame16;
    [SerializeField] GameObject Frame17;
    [SerializeField] GameObject Frame18;
    */
    
    [SerializeField] float duration = 0;
    float multipleFrameToClipDuration = 0;

    public void skipScene()
    {

        SceneManager.LoadScene(2);
        Narator.Stop();
    }


    private void Start()
    {
        if (PlayerPrefs.HasKey("hasPlayedOnce"))
        {
            int hasPlayedOnce = PlayerPrefs.GetInt("hasPlayedOnce", 0);
            if (hasPlayedOnce == 1) skipScene();
        }
        
        if (PlayerPrefs.HasKey("BackgroundMusicVolume"))
        {
            Narator.volume = PlayerPrefs.GetFloat("BackgroundMusicVolume", 1f);
        }
        StartCoroutine(FrameA(.3f));
    }
    IEnumerator FrameA(float _duration)
    {
        camera.transform.position = new Vector3(-25, -17f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameB(.3f));
    }
    IEnumerator FrameB(float _duration)
    {
        camera.transform.position = new Vector3(-25, -40f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameC(.3f));
    }
    IEnumerator FrameC(float _duration)
    {
        camera.transform.position = new Vector3(-25, -62.40001f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameD(duration));
    }
    IEnumerator FrameD(float _duration)
    {
        camera.transform.position = new Vector3(-25, -85.3f, -10);
        yield return new WaitForSecondsRealtime(_duration);

        duration = VoiceOver[0].length;
        StartCoroutine(FrameE(duration));
    }
    IEnumerator FrameE(float _duration)
    {
        Narator.PlayOneShot(VoiceOver[0]);

        camera.transform.position = new Vector3(-25, -108f, -10);
        yield return new WaitForSecondsRealtime(duration);

        multipleFrameToClipDuration = VoiceOver[1].length / 2;
        StartCoroutine(FrameF(multipleFrameToClipDuration));
    }
    IEnumerator FrameF(float _duration)
    {
        Narator.PlayOneShot(VoiceOver[1]);
        camera.transform.position = new Vector3(-25, -131f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameG(multipleFrameToClipDuration));
    }
    IEnumerator FrameG(float _duration)
    {
        camera.transform.position = new Vector3(-25, -153.9f, -10);
        yield return new WaitForSecondsRealtime(_duration);

        duration = VoiceOver[2].length;
        StartCoroutine(FrameH(duration));
    }
    IEnumerator FrameH(float _duration)
    {
        Narator.PlayOneShot(VoiceOver[2]);
        camera.transform.position = new Vector3(-25, -176.8f, -10);
        yield return new WaitForSecondsRealtime(_duration);

        duration = VoiceOver[3].length;
        StartCoroutine(FrameI(duration));
    }
    IEnumerator FrameI(float _duration)
    {
        Narator.PlayOneShot(VoiceOver[3]);
        camera.transform.position = new Vector3(-25, -198.6f, -10);
        yield return new WaitForSecondsRealtime(_duration);

        multipleFrameToClipDuration = VoiceOver[4].length / 6;
        StartCoroutine(FrameJ(multipleFrameToClipDuration));
    }
    IEnumerator FrameJ(float _duration)
    {
        Narator.PlayOneShot(VoiceOver[4]);
        camera.transform.position = new Vector3(-25, -221.6f, -10);
        yield return new WaitForSecondsRealtime(_duration);

        StartCoroutine(FrameK(multipleFrameToClipDuration));
    }
    IEnumerator FrameK(float _duration)
    {
        camera.transform.position = new Vector3(-25, -247.9f, -10);
        yield return new WaitForSecondsRealtime(_duration);

        StartCoroutine(FrameL(multipleFrameToClipDuration));
    }
    IEnumerator FrameL(float _duration)
    {
        camera.transform.position = new Vector3(-25, -273.6f, -10);
        yield return new WaitForSecondsRealtime(_duration);

        StartCoroutine(FrameM(multipleFrameToClipDuration));
    }
    IEnumerator FrameM(float _duration)
    {
        camera.transform.position = new Vector3(-25, -298.6f, -10);
        yield return new WaitForSecondsRealtime(_duration);

        StartCoroutine(FrameN(multipleFrameToClipDuration));
    }
    IEnumerator FrameN(float _duration)
    {
        camera.transform.position = new Vector3(-25, -321.9f  , -10);
        yield return new WaitForSecondsRealtime(_duration);

        StartCoroutine(FrameO(multipleFrameToClipDuration));
    }
    IEnumerator FrameO(float _duration)
    {
        camera.transform.position = new Vector3(-25, -345.9f, -10);
        yield return new WaitForSecondsRealtime(_duration);

        multipleFrameToClipDuration = VoiceOver[5].length / 2;
        StartCoroutine(FrameP(multipleFrameToClipDuration));
    }
    IEnumerator FrameP(float _duration)
    {
        Narator.PlayOneShot(VoiceOver[5]);
        camera.transform.position = new Vector3(-25, -369.7f, -10);
        yield return new WaitForSecondsRealtime(_duration);

        StartCoroutine(FrameQ(multipleFrameToClipDuration));
    }
    IEnumerator FrameQ(float _duration)
    {
        camera.transform.position = new Vector3(-25, -394f, -10);
        yield return new WaitForSecondsRealtime(_duration);

        multipleFrameToClipDuration = VoiceOver[6].length / 2;
        StartCoroutine(FrameR(multipleFrameToClipDuration));
    }
    IEnumerator FrameR(float _duration)
    {
        Narator.PlayOneShot(VoiceOver[6]);
        camera.transform.position = new Vector3(-25, -418.4f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameS(multipleFrameToClipDuration));
    }
    IEnumerator FrameS(float _duration)
    {
        camera.transform.position = new Vector3(-25, -442.8f, -10);
        yield return new WaitForSecondsRealtime(_duration);

        duration = VoiceOver[7].length;
        StartCoroutine(FrameT(duration));
    }
    IEnumerator FrameT(float _duration)
    {
        camera.transform.position = new Vector3(-25, -467.2f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        Narator.Stop();

        skipScene();
        PlayerPrefs.SetInt("hasPlayedOnce", 1);
    }
}
