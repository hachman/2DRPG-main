using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneModifier : MonoBehaviour
{
    
    public void LoadScene(string sceneName) => ChangeScene.instance?.LoadScene(sceneName);
    
}
