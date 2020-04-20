using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MuteButton : MonoBehaviour
    {
        [SerializeField]
        private Sprite onAudio;
        [SerializeField]
        private Sprite offAudio;
        [SerializeField]
        private Image image;

        public void OnClick()
        {
            ActionManager actionManager = ActionManager.Instance;
            if (actionManager.playerCurrentAction != null 
                && actionManager.playerCurrentAction.type == ActionType.MUTE
                && actionManager.playerCurrentAction.doIt)
            {
                actionManager.DoAction(new Action(true, ActionType.MUTE));
            }
            else
            {
                AudioListener.volume = AudioListener.volume > 0 ? 0 : 1;
                image.sprite = AudioListener.volume > 0 ? onAudio : offAudio;
            }
        }
    }
}