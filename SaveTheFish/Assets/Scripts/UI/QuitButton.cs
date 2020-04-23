using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class QuitButton : MonoBehaviour
    {

        public void OnClick()
        {
            ActionManager actionManager = ActionManager.Instance;
            if (actionManager.playerCurrentAction != null 
                && actionManager.playerCurrentAction.type == ActionType.QUIT
                && actionManager.playerCurrentAction.doIt)
            {
                ActionManager.Instance.DoAction(new Action(true, ActionType.QUIT));
            }
            else 
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
                Application.OpenURL("https://ldjam.com/events/ludum-dare/46/save-the-fish");
#else
                Application.Quit();
#endif
            }
        }

    }
}