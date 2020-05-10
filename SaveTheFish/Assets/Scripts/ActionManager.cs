using Helper;
using System;
using UnityEngine;
using UI;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
#endif

public enum ActionState { INIT, SHOW_INSTRUCTIONS, IN_PROGRESS, PAUSE_ACTION, SEQUENCE_DONE, GAME_OVER }
public class ActionManager : SingletonSaved<ActionManager>
{
    [SerializeField]
    protected float actionTimer = 3f;
    [SerializeField]
    protected float decreaseTimer = 0.9f;
    [SerializeField]
    protected float minTimer = 0.8f;
    [SerializeField]
    public float holdMinTime { get; protected set; } = 0.3f;
    [SerializeField]
    private DisplaySequence displaySequence;
    [SerializeField]
    private WinSequence winSequence;
    [SerializeField]
    private float waitBeforeWinSequence = 1f;
    [SerializeField]
    private FailSequence failSequence;
    [SerializeField]
    GameObject fish;

    protected RandomActionList actions = new RandomActionList();
    public ActionState currentState { get; protected set; }
    public int actionCount { get; protected set; }
    public int playerCurrentActionCount { get; protected set; }
    public UserAction lastAction
    {
        get
        {
            return actions.Count > actionCount - 1 && actionCount >= 1 ? actions[actionCount - 1] : null;
        }
    }
    public UserAction playerCurrentAction
    {
        get
        {
            return actions.Count > playerCurrentActionCount && playerCurrentActionCount >= 0 ? actions[playerCurrentActionCount] : null;
        }
    }

    public float initialTime { get; private set; } = 0;
    public float remainingTime { get; private set; } = 0;

    public float remainingRatio
    {
        get
        {
            return remainingTime / initialTime;
        }
    }

    public void StartGame()
    {
        Reset();
        StartActionSequence();
    }

    public void Update()
    {
        if(currentState == ActionState.IN_PROGRESS)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Mathf.Max(remainingTime, 0);
            if(remainingTime == 0)
            {
                if (playerCurrentAction.doIt)
                    StartCoroutine(GameOver(null));
                else
                    ActionSuccess();
            }
        }
    }

    public void StartActionSequence()
    {
        if (currentState == ActionState.INIT || currentState == ActionState.SEQUENCE_DONE)
        {
            ResetTimer();
            actionCount++;
            playerCurrentActionCount = 0;
            ShowIntructions();
        }
    }

    private void ShowIntructions()
    {
        currentState = ActionState.SHOW_INSTRUCTIONS;
        Utils.ClearLogs();
        Debug.Log("Start");
        Debug.Log(lastAction);
        displaySequence.Show(lastAction, actions.GetRange(0, actionCount - 1));
    }

    public void OnInstructionsHidden()
    {
        currentState = ActionState.IN_PROGRESS;
        playerCurrentActionCount = 0;
    }

    private float CalculateTimerDuration()
    {
        return (actionTimer - minTimer) * Mathf.Pow(decreaseTimer, actionCount) + minTimer;
    }

    public void StartPauseTimer()
    {
        if(currentState == ActionState.IN_PROGRESS)
            currentState = ActionState.PAUSE_ACTION;
    }

    public void StopPauseTimer()
    {
        if(currentState == ActionState.PAUSE_ACTION)
            currentState = ActionState.IN_PROGRESS;
    }

    public void DoAction(UserAction action)
    {
        if(currentState == ActionState.IN_PROGRESS)
        {
            Debug.Log(action);
            if(playerCurrentAction.IsValid(action))
            {
                ActionSuccess();
            }
            else
            {
                StartCoroutine(GameOver(action));
            }
        }
    }

    private void ActionSuccess()
    {
        Debug.Log("Well done");
        playerCurrentActionCount++;
        if (playerCurrentActionCount >= actionCount)
        {
            currentState = ActionState.SEQUENCE_DONE;
            StartCoroutine(ShowWinSequence());
        }
        else
        {
            ResetTimer();
        }
        if (fish != null)
            fish.BroadcastMessage("ActionSuccess", null, SendMessageOptions.DontRequireReceiver);
    }

    private IEnumerator ShowWinSequence()
    {
        yield return new WaitForSecondsRealtime(waitBeforeWinSequence);
        winSequence.Show(actionCount + 1);
    }

    private IEnumerator GameOver(UserAction userAction)
    {
        Debug.Log("Game Over");
        if (fish != null)
            fish.BroadcastMessage("ActionFail", null, SendMessageOptions.DontRequireReceiver);
        currentState = ActionState.GAME_OVER;
        yield return new WaitForSecondsRealtime(waitBeforeWinSequence);
        failSequence.Show(userAction,playerCurrentAction);
    }

    public bool IsGameOver()
    {
        return currentState == ActionState.GAME_OVER;
    }

    public void Restart()
    {
        Reset();
        StartActionSequence();
    }

    private void Reset()
    {
        actionCount = 0;
        playerCurrentActionCount = 0;
        currentState = ActionState.INIT;
        actions.Init();
    }

    private void ResetTimer()
    {
        remainingTime = CalculateTimerDuration();
        initialTime = remainingTime;
    }
}
