using Helper;
using System;
using UnityEngine;
using UI;

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
    GameObject fish;

    protected ActionSequence actions = new ActionSequence();
    protected ActionState currentState;
    protected int actionCount;
    protected int playerCurrentActionCount;
    public Action lastAction
    {
        get
        {
            return actions.Count > actionCount - 1 && actionCount > 1 ? actions[actionCount - 1] : null;
        }
    }
    public Action playerCurrentAction
    {
        get
        {
            return actions.Count > playerCurrentActionCount && playerCurrentActionCount >= 0 ? actions[playerCurrentActionCount] : null;
        }
    }

    public float remainingTime { get; private set; } = 0;

    public void StartGame()
    {
        Init();
        Reset();
        StartActionSequence();
    }

    private void Init()
    {
        displaySequence.hidden += OnInstructionsHidden;
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
                    GameOver();
                else
                    ActionSuccess();
            }
        }
    }

    private void StartActionSequence()
    {
        if (currentState == ActionState.INIT || currentState == ActionState.SEQUENCE_DONE)
        {
            remainingTime = CalculateTimerDuration();
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

    private void OnInstructionsHidden(object sender, EventArgs e)
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

    public void DoAction(Action action)
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
                GameOver();
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
            StartActionSequence();
        }
        else
        {
            remainingTime = CalculateTimerDuration();
        }
        if (fish != null)
            fish.BroadcastMessage("ActionSuccess", null, SendMessageOptions.DontRequireReceiver);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        currentState = ActionState.GAME_OVER;
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
}
