using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public void PlaySFX(string sfxName)
    {
        AudioManager.instance.PlaySoundEffect(sfxName);
    }
    public void PlayClick()
    {
        AudioManager.instance.PlaySoundEffect("click");
    }
}
