using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class QuitButton : MonoBehaviour
    {
        public void OnClick()
        {
            ActionManager.Instance.DoAction(new Action(ActionType.QUIT));
        }
    }
}