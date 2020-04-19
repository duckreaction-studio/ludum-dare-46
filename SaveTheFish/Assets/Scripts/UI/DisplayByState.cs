using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayByState : MonoBehaviour
{
    [SerializeField]
    private ActionState state;

    [SerializeField]
    private GameObject[] targets;

    void Update()
    {
        foreach(var go in targets)
        {
            go.SetActive(ActionManager.Instance.currentState == state);
        }
    }
}
