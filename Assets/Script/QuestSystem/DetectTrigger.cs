using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DetectTrigger : MonoBehaviour
{
    public LastKnownPosition lkp;

    public SAVESYSTEM ss;

    public bool _canDetect = false;
    public void SetCanDetect(bool canDetect) => _canDetect = canDetect;
    [SerializeField] UnityEvent OnDetected;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPrefs.SetString("previousScene", value: SceneManager.GetActiveScene().name);
        Debug.Log("Detected ");
       
        
        
        if (!_canDetect) return;
        OnDetected?.Invoke();
    }
}
