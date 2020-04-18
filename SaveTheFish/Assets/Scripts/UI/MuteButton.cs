using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MuteButton : MonoBehaviour
    {
        public void OnClick()
        {
            ActionManager.Instance.DoAction(new Action(true,ActionType.MUTE));
        }
    }
}