using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Playerevent : MonoBehaviour
{
    public static UnityAction<Transform> onPlayerSpawned;

    public static UnityAction onPlayerDespawned;
}
