using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class introcutscene : MonoBehaviour
{
    [SerializeField] Button Play;
    [SerializeField] Button Skip;
    [SerializeField] GameObject ComicStrip;
    [SerializeField] TMP_Text Subtitle;
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource Narator;

    [Header("Audio Clips")]
    //1
    [SerializeField] AudioClip FrameAClipA;
    [SerializeField] AudioClip FrameAClipB;
    //2
    [SerializeField] AudioClip FrameBClip;
    //3
    [SerializeField] AudioClip FrameCClipA;
    [SerializeField] AudioClip FrameCClipB;
    //4
    [SerializeField] AudioClip FrameDClipA;
    [SerializeField] AudioClip FrameDClipB;
    [SerializeField] AudioClip FrameDClipC;


    private void Start()
    {

        float duration = FrameAClipA.length;
        StartCoroutine(FrameA(duration));
    }
    private void Update()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            BGM.volume = PlayerPrefs.GetFloat("MusicVolume") / 2f;
            Narator.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
    }
    IEnumerator FrameA(float duration)
    {
        Narator.PlayOneShot(FrameAClipA);

        ComicStrip.transform.position = new Vector3(0, -16.4f, 0);
        Subtitle.SetText("After hours of celebrating the creation of ultimate chip");

        yield return new WaitForSecondsRealtime(duration);

        duration = FrameAClipB.length;
        StartCoroutine(FrameA2(duration));
    }
    IEnumerator FrameA2(float duration)
    {
        Narator.PlayOneShot(FrameAClipB);

        Subtitle.SetText("Ozzi, Chiko, and Bibi are heading back to the kidlies headquarters  to take a whole day of rest");

        yield return new WaitForSecondsRealtime(duration);

        duration = FrameBClip.length;
        StartCoroutine(FrameB(duration));
    }
    IEnumerator FrameB(float duration)
    {
        Narator.PlayOneShot(FrameBClip);

        ComicStrip.transform.position = new Vector3(0, -5.65f, 0);
        Subtitle.SetText("they opened the door just to find out that their place has been vandalized by an unknown creature");

        yield return new WaitForSecondsRealtime(duration);

        duration = FrameCClipA.length;
        StartCoroutine(FrameC(duration));
    }
    IEnumerator FrameC(float duration)
    {
        Narator.PlayOneShot(FrameCClipA);

        ComicStrip.transform.position = new Vector3(0, 5f, 0);
        Subtitle.SetText("Shocked! When they find out that the ultimate chip is missing from the  vitrin.");

        yield return new WaitForSecondsRealtime(duration);

        duration = FrameCClipB.length;
        StartCoroutine(FrameC2(duration));
    }
    IEnumerator FrameC2(float duration)
    {
        Narator.PlayOneShot(FrameCClipB);

        Subtitle.SetText("It looks like someone has stolen it");

        yield return new WaitForSecondsRealtime(duration);

        duration = FrameDClipA.length;
        StartCoroutine(FrameD(duration));
    }
    IEnumerator FrameD(float duration)
    {
        Narator.PlayOneShot(FrameDClipA);

        ComicStrip.transform.position = new Vector3(0, 16f, 0);
        Subtitle.SetText("Chiko remembered that there are security cameras in the facility");

        yield return new WaitForSecondsRealtime(duration);

        duration = FrameDClipB.length;
        StartCoroutine(FrameD2(duration));
    }
    IEnumerator FrameD2(float duration)
    {
        Narator.PlayOneShot(FrameDClipB);

        Subtitle.SetText("that might help them know who took the ultimate chip");

        yield return new WaitForSecondsRealtime(duration);

        duration = FrameDClipC.length;
        StartCoroutine(FrameD3(duration));
    }
    IEnumerator FrameD3(float duration)
    {
        Narator.PlayOneShot(FrameDClipC);

        Subtitle.SetText("But first they must crack the code on their computer to view the footage of the incident");

        yield return new WaitForSecondsRealtime(duration);

        Play.gameObject.SetActive(true);
        Skip.gameObject.SetActive(false);
    }

}

