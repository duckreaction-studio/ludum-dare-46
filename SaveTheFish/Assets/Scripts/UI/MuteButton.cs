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

        private AudioListener _audioListener;
        public AudioListener audioListener
        {
            get
            {
                if(_audioListener == null && Camera.main != null)
                {
                    _audioListener = Camera.main.GetComponent<AudioListener>();
                }
                return _audioListener;
            }
        }
        public void OnClick()
        {
            ActionManager actionManager = ActionManager.Instance;
            if (actionManager.playerCurrentAction != null 
                && actionManager.playerCurrentAction.type == ActionType.MUTE
                && actionManager.playerCurrentAction.doIt)
            {
                actionManager.DoAction(new Action(true, ActionType.MUTE));
            }
            else if(audioListener != null)
            {
                audioListener.enabled = !audioListener.enabled;
                image.sprite = audioListener.enabled ? onAudio : offAudio;
            }
        }
    }
}