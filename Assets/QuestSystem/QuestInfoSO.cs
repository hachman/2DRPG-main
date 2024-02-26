using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSo", menuName ="ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSo : ScriptableObject
{
    [field: SerializeField] public string id {  get; private set; }

    [Header("General")]

    public string displayName;

    [Header("Requirements")]
    
    public int levelRequirment;

    public QuestInfoSo[] questPrerequisites;

    [Header("Steps")]

    public GameObject[] questStepPrefabs;

    [Header("Rewards")]

    public int KnowledgeReward;

    public int experienceReward;
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
