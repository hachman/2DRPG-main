using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Sprite sfxMutedSprite, sfxDefaultSprite;
    [SerializeField] Sprite bgmMutedSprite, bgmDefaultSprite;

    [SerializeField] Image bgmImage, sfxImage;
    private void Start()
    {

        ConiditonBGM();


    }
    private void ConiditonBGM()
    {
        bgmImage.sprite = bgmDefaultSprite;
        if (AudioManager.instance.IsBGM_Muted())
        {
            bgmImage.sprite = bgmMutedSprite;
        }
    }
    private void ConditionSFX()
    {
        sfxImage.sprite = sfxDefaultSprite;
        if (AudioManager.instance.IsSFX_Muted())
        {
            sfxImage.sprite = sfxMutedSprite;
        }
    }
    public void MuteMusic()
    {

        AudioManager.instance.MusicMuteToggle();
        ConiditonBGM();


    }
    public void MuteSFX()
    {
        AudioManager.instance.MuteSoundToggle();
        ConditionSFX();

    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void Continue()
    {
        Time.timeScale = 1;
    }
}
