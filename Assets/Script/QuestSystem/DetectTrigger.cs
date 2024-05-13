using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectTrigger : MonoBehaviour
{
    public bool _canDetect = false;
    public void SetCanDetect(bool canDetect) => _canDetect = canDetect;
    [SerializeField] UnityEvent OnDetected;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Detected ");
        if (!_canDetect) return;
        OnDetected?.Invoke();
    }
}
