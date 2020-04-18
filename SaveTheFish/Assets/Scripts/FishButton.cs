using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishButton : MonoBehaviour
{
    [SerializeField]
    private string target;

    private void OnMouseDown()
    {
        ActionManager.Instance.DoAction(Action.Click(target));
    }
}
