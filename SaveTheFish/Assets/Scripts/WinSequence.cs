using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinSequence : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nextRoundText;

    private SwitchProcessProfile _switchProcess;
    public SwitchProcessProfile switchProcess
    {
        get
        {
            if(_switchProcess == null)
            {
                _switchProcess = Camera.main.GetComponent<SwitchProcessProfile>();
            }
            return _switchProcess;
        }
    }
    public void Show(int nextRound)
    {
        gameObject.SetActive(true);
        switchProcess.SetProfile(1);
        nextRoundText.text = string.Format("NEXT ROUND : {0:00}", nextRound);
    }

    public void OnClick()
    {
        gameObject.SetActive(false);
        switchProcess.SetProfile(0);
        ActionManager.Instance.StartActionSequence();
    }
}
