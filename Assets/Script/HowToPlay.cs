using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] GameObject HTPPanel;
    [SerializeField] Image HTPImage;
    [SerializeField] GameObject FocusPanel;
    [SerializeField] Image ArrowImage;
    [SerializeField] TextMeshProUGUI HTPTMPro;
    [Space]
    [Header("How To Play Images")]
    [SerializeField] Sprite[] HTPImages;
    [Space]
    [SerializeField] Button NextButton;
    
    int HTPCount = 0;

    string[] HTPText = new string[]
    {
        "To move your character, use the joystick on your lower left corner.",
        "And to interact, use the interact button on your lower right corner.",
        "You may also interact NPC from a distance when the exclamation \" ! \" point shows above their head.",
        "To answer the given problems, select your answers by clicking them"
    };

    
    private void Start()
    {
        if (HTPImage.sprite != null)
        {
            Debug.Log("Initial");
            HTPImage.sprite = HTPImages[0];
            HTPTMPro.SetText(HTPText[0]);

            ArrowImage.transform.position = new Vector3(875f, 543f, 0);
            ArrowImage.transform.rotation = Quaternion.Euler(0, 0, -90f);

            HTPCount++;
        }
    }

    public void NextInstruction()
    {
        Debug.Log("Before Codes: " +  HTPCount);
        
        if(HTPCount == 1)
        {
            ArrowImage.transform.position = new Vector3(1431, 470, 0);
            ArrowImage.transform.rotation = Quaternion.Euler(0, 0, 90f);
        }
        else if (HTPCount == 2)
        {
            FocusPanel.SetActive(false);

            ArrowImage.gameObject.SetActive(false);
        }
        if (NextButton.GetComponentInChildren<TextMeshProUGUI>().text == "GOT IT!")
        {
            HTPPanel.SetActive(false);
            HTPCount = 0;
            NextButton.GetComponentInChildren<TextMeshProUGUI>().SetText("NEXT>");

            ArrowImage.transform.position = new Vector3(875f, 543f, 0);
            ArrowImage.transform.rotation = Quaternion.Euler(0, 0, -90f);
        }
        if (HTPCount == HTPImages.Length - 1)
        {
            NextButton.GetComponentInChildren<TextMeshProUGUI>().SetText("GOT IT!");
            
        }
        
        HTPImage.sprite = HTPImages[HTPCount];
        HTPTMPro.SetText(HTPText[HTPCount]);
        HTPCount++;
        Debug.Log("After Codes: " + HTPCount);
    }
}
