﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public void OnClick()
    {
        ActionManager.Instance.StartGame();
        gameObject.SetActive(false);
    }
}
