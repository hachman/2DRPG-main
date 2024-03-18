using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string bgmName;
    void Start()
    {
        AudioManager.instance.PlayBackgroundMusic(bgmName);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
