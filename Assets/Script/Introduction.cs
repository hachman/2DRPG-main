using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Introduction : MonoBehaviour
{
    [SerializeField] Camera camera;

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

    [SerializeField] float duration = 0;

    public void skipScene()
    {
        SceneManager.LoadScene(2);
    }


    private void Start()
    {
        StartCoroutine(FrameA(duration));
    }
    IEnumerator FrameA(float _duration)
    {
        camera.transform.position = new Vector3(-25, -17f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameB(duration));
    }
    IEnumerator FrameB(float _duration)
    {
        camera.transform.position = new Vector3(-25, -40f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameC(duration));
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
        StartCoroutine(FrameE(duration));
    }
    IEnumerator FrameE(float _duration)
    {
        camera.transform.position = new Vector3(-25, -108f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameF(duration));
    }
    IEnumerator FrameF(float _duration)
    {
        camera.transform.position = new Vector3(-25, -131f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameG(duration));
    }
    IEnumerator FrameG(float _duration)
    {
        camera.transform.position = new Vector3(-25, -153.9f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameH(duration));
    }
    IEnumerator FrameH(float _duration)
    {
        camera.transform.position = new Vector3(-25, -176.8f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameI(duration));
    }
    IEnumerator FrameI(float _duration)
    {
        camera.transform.position = new Vector3(-25, -198.6f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameJ(duration));
    }
    IEnumerator FrameJ(float _duration)
    {
        camera.transform.position = new Vector3(-25, -221.6f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameK(duration));
    }
    IEnumerator FrameK(float _duration)
    {
        camera.transform.position = new Vector3(-25, -247.9f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameL(duration));
    }
    IEnumerator FrameL(float _duration)
    {
        camera.transform.position = new Vector3(-25, -273.6f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameM(duration));
    }
    IEnumerator FrameM(float _duration)
    {
        camera.transform.position = new Vector3(-25, -298.6f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameN(duration));
    }
    IEnumerator FrameN(float _duration)
    {
        camera.transform.position = new Vector3(-25, -321.9f  , -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameO(duration));
    }
    IEnumerator FrameO(float _duration)
    {
        camera.transform.position = new Vector3(-25, -345.9f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameP(duration));
    }
    IEnumerator FrameP(float _duration)
    {
        camera.transform.position = new Vector3(-25, -369.7f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameQ(duration));
    }
    IEnumerator FrameQ(float _duration)
    {
        camera.transform.position = new Vector3(-25, -394f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameR(duration));
    }
    IEnumerator FrameR(float _duration)
    {
        camera.transform.position = new Vector3(-25, -418.4f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameS(duration));
    }
    IEnumerator FrameS(float _duration)
    {
        camera.transform.position = new Vector3(-25, -442.8f, -10);
        yield return new WaitForSecondsRealtime(_duration);
        StartCoroutine(FrameT(duration));
    }
    IEnumerator FrameT(float _duration)
    {
        camera.transform.position = new Vector3(-25, -467.2f, -10);
        yield return new WaitForSecondsRealtime(_duration);
    }
}
