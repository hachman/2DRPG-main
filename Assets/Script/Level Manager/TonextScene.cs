using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TonextScene : MonoBehaviour
{
    private int nextSceneToLoad;
    FadeInOut fade;

    private void Start()
    { 
        fade = FindAnyObjectByType<FadeInOut>();
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public IEnumerator _Changescene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextSceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(_Changescene());
        
    }
}
