﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    private Vector2 originalPos;
    protected override void Start()
    {
        base.Start();
        originalPos = background.anchoredPosition;
        // background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        // background.gameObject.SetActive(false);
        background.anchoredPosition = ScreenPointToAnchoredPosition(originalPos);
        base.OnPointerUp(eventData);
    }
}