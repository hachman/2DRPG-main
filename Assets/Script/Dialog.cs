using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Dialogue", menuName = "New Dialogue")]
public class Dialog : ScriptableObject
{
    public Quest_Data quesToGive;
    public List<string> Lines;


}
