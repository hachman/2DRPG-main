using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour, Interactable
{
    public void Interact()
    {
        Debug.Log("You Need to finish all the quest before passing to me!");
    }
}
