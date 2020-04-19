using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayActionCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text uiText;

    void Update()
    {
        uiText.text = "ACTIONS COUNT " + ActionManager.Instance.playerCurrentActionCount
            + "/" + ActionManager.Instance.actionCount;
    }
}
