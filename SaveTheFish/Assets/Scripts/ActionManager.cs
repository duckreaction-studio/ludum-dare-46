using Helper;
using System;
using UnityEngine;
using UI;

#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
#endif

public enum ActionState { INIT, SHOW_INSTRUCTIONS, IN_PROGRESS, PAUSE_ACTION, DONE, GAME_OVER }
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

    protected ActionSequence actions = new ActionSequence();
    protected ActionState currentState;
    protected int actionCount;
    protected Action currentAction;

    public float remainingTime { get; private set; } = 0;

    public void Start()
    {
        Init();
        Restart();
    }

    private void Init()
    {
        displaySequence.hidden += OnInstructionsHidden;
    }

    public void Update()
    {
        if(currentState == ActionState.INIT || currentState == ActionState.DONE)
        {
            currentAction = actions[actionCount];
            remainingTime = CalculateTimerDuration();
            actionCount++;
            ShowIntructions();
        }
        else if(currentState == ActionState.IN_PROGRESS)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Mathf.Max(remainingTime, 0);
            if(remainingTime == 0)
            {
                if (currentAction.doIt)
                    GameOver();
                else
                    currentState = ActionState.DONE;
            }
        }
    }

    private void ShowIntructions()
    {
        currentState = ActionState.SHOW_INSTRUCTIONS;
        Utils.ClearLogs();
        Debug.Log("Start");
        Debug.Log(currentAction);
        displaySequence.Show(currentAction, actions.GetRange(0, actionCount - 1));
    }

    private void OnInstructionsHidden(object sender, EventArgs e)
    {
        currentState = ActionState.IN_PROGRESS;
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        currentState = ActionState.GAME_OVER;
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
        currentState = ActionState.IN_PROGRESS;
    }

    public void DoAction(Action action)
    {
        if(currentState == ActionState.IN_PROGRESS)
        {
            if(currentAction.IsValid(action))
            {
                Debug.Log("Well done");
                currentState = ActionState.DONE;
            }
            else
            {
                GameOver();
            }
        }
    }

    public bool IsGameOver()
    {
        return currentState == ActionState.GAME_OVER;
    }

    public void Restart()
    {
        actionCount = 0;
        currentState = ActionState.INIT;
        actions.Init();
    }

}
