using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
  public List<Dialog> questDialog;
  public Dialog defaultDialog;
  public void Interact()
  {
    if (QuestSystemManager.instance.activeQuest != null)
    {
      DialogManager.Instance.ShowDialog(defaultDialog, () => { }); return; // if theres an active quest, do not run search
    }
    foreach (var item in questDialog)
    {
      if (item.quesToGive.IsRequirentsDone() && item.quesToGive.questState == Quest_State.Inactive) // if activequest is null, then find the requirements done and inactive
      {
        DialogManager.Instance.ShowDialog(item, () =>
      {
        QuestSystemManager.instance.ActivateQuest(item.quesToGive);
      });

        return; // exevute the earliest quest so return as soon as given
      }
    }

    // if there's no quests to give but no active quest
    DialogManager.Instance.ShowDialog(defaultDialog, () => { });

  }
}
