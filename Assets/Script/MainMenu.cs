/*using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] TextMeshProUGUI TextPanel;
    int count = 0;
    float timer = 0f;
    bool isCounting = false;
    private void Start()
    {
        AudioManager.instance.PlayBackgroundMusic("menu");
    }
    public void PlayGame()
    {
        AudioManager.instance.PlaySoundEffect("click");
        SceneManager.LoadSceneAsync(1);
        QuestSystemManager.instance.ResetQuests();
    }
    public void OptionMenu()
    {
        
    }
    public void LoadGame()
    {
        AudioManager.instance.PlaySoundEffect("click");
        if (string.IsNullOrEmpty(QuestSystemManager.instance.questDatabaseJSON.ActiveQuestName)) return;
        QuestSystemManager.instance.RepositionPlayer = true;
        SceneManager.LoadScene(QuestSystemManager.instance.questDatabaseJSON.savedScene);
    }
    public void QuitGame()
    {
        AudioManager.instance.PlaySoundEffect("click");
        Application.Quit();
    }
    public void OnNewGameClicked()
    {
        AudioManager.instance.PlaySoundEffect("click");
        DataPersistenceManager.instance.NewGame();
    }
    public void OnLoadGameClicked()
    {
        AudioManager.instance.PlaySoundEffect("click");
        DataPersistenceManager.instance.LoadGame();
    }
    public void OnSaveGameClicked()
    {
        AudioManager.instance.PlaySoundEffect("click");
        DataPersistenceManager.instance.SaveGame();
    }
    private void Update()
    {
        if (isCounting)
        {
            timer += Time.deltaTime;
            if(timer > 2f) 
            {
                count = 0;
                timer = 0f;
                isCounting = false;
            }
        }
    }
    public void CreditsDisplay()
    {
        if(!isCounting)
        {
            if (Panel.gameObject.active)
            {
                Panel.SetActive(false);
            }
            isCounting=true;
            timer =0f;
        }
        count++;
        if (count >= 5 && timer <= 2f)
        {
            Panel.SetActive(true);
            int i = Random.Range(0, 6);
            string[] text = new string[]
            {
                "\"Ro hxd jfwc xc qrwqf unwr j bwn, qraab kdam unwr j bwn\" - J.Y.S. Jkmdu Tjuvj\n\n\n .........",
                "\"Bxn vyxyn ynj bxd nvwbnnb, nwnq nxmj qrwqfa\" - Jwnxwhvd\n\n\n .........",
                "\"Hxd ljw'cnlygclb rbx jcq yjwnqrwoxjc yjwjwn qrwox\" - Dwtxvfw\n\n\n .........",
                "\"Ro hxd'anj yjwn oxq hxd qrnaywj qrnaywj qrnay\" - Numar Nmxrx Ljufn\n\n\n .........",
                "\"Mnaj qrnayxq nmqrb qrnay mnr xqjq\" - Dwtxvfw\n\n\n .........",
                "\"Cqn krppnbc kdamwn rb j ujih lxvymrjw\" - Dwtxvfw\n\n\n ........."
            };
            TextPanel.SetText(text[i]);
            count = 0;
            timer = 0f;
            isCounting = false;
        }
    }
}*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI textPanel;

    private int count = 0;
    private float timer = 0f;
    private bool isCounting = false;

    private void Start()
    {
        AudioManager.instance.PlayBackgroundMusic("menu");
    }

    public void PlayGame()
    {
        AudioManager.instance.PlaySoundEffect("click");
        QuestSystemManager.instance.ResetQuests();
        SceneManager.LoadSceneAsync(1);
    }

    public void OptionMenu()
    {
        // Option menu logic here
    }

    public void LoadGame()
    {
        AudioManager.instance.PlaySoundEffect("click");

        if (string.IsNullOrEmpty(QuestSystemManager.instance.questDatabaseJSON.ActiveQuestName)) return;

        QuestSystemManager.instance.RepositionPlayer = true;
        SceneManager.LoadScene(QuestSystemManager.instance.questDatabaseJSON.savedScene);
    }

    public void QuitGame()
    {
        AudioManager.instance.PlaySoundEffect("click");
        Application.Quit();
    }

    public void OnNewGameClicked()
    {
        AudioManager.instance.PlaySoundEffect("click");
        
        DataPersistenceManager.instance.NewGame();
        
        QuestSystemManager.instance.ResetQuests();
    }

    public void OnLoadGameClicked()
    {
        AudioManager.instance.PlaySoundEffect("click");
        DataPersistenceManager.instance.LoadGame();
    }

    public void OnSaveGameClicked()
    {
        AudioManager.instance.PlaySoundEffect("click");
        DataPersistenceManager.instance.SaveGame();
    }

    private void Update()
    {
        if (isCounting)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                count = 0;
                timer = 0f;
                isCounting = false;
            }
        }
    }

    public void CreditsDisplay()
    {
        if (!isCounting)
        {
            if (panel.activeSelf)
            {
                panel.SetActive(false);
            }
            isCounting = true;
            timer = 0f;
        }

        count++;

        if (count >= 5 && timer <= 2f)
        {
            panel.SetActive(true);
            int i = Random.Range(0, 6);
            string[] text = new string[]
            {
                "\"Ro hxd jfwc xc qrwqf unwr j bwn, qraab kdam unwr j bwn\" - J.Y.S. Jkmdu Tjuvj\n\n\n .........",
                "\"Bxn vyxyn ynj bxd nvwbnnb, nwnq nxmj qrwqfa\" - Jwnxwhvd\n\n\n .........",
                "\"Hxd ljw'cnlygclb rbx jcq yjwnqrwoxjc yjwjwn qrwox\" - Dwtxvfw\n\n\n .........",
                "\"Ro hxd'anj yjwn oxq hxd qrnaywj qrnaywj qrnay\" - Numar Nmxrx Ljufn\n\n\n .........",
                "\"Mnaj qrnayxq nmqrb qrnay mnr xqjq\" - Dwtxvfw\n\n\n .........",
                "\"Cqn krppnbc kdamwn rb j ujih lxvymrjw\" - Dwtxvfw\n\n\n ........."
            };
            textPanel.SetText(text[i]);
            count = 0;
            timer = 0f;
            isCounting = false;
        }
    }
}

