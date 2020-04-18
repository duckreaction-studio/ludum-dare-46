using Helper;
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
#endif

public enum ActionState { INIT, IN_PROGRESS, DONE, GAME_OVER }
public class ActionManager : SingletonSaved<ActionManager>
{
    [SerializeField]
    protected float actionTimer = 3f;
    [SerializeField]
    protected float decreaseTimer = 0.9f;
    [SerializeField]
    protected float minTimer = 0.8f;

    protected ActionState currentState;
    protected int actionCount;
    protected float currentTimerDuration;
    protected float startActionTime;

    protected Action currentAction;

    public float remaingTime
    {
        get
        {
            return Mathf.Max(startActionTime + currentTimerDuration - Time.realtimeSinceStartup, 0);
        }
    }

    public void Update()
    {
        if(currentState == ActionState.INIT || currentState == ActionState.DONE)
        {
            currentState = ActionState.IN_PROGRESS;
            currentAction = CreateRandomAction();
            startActionTime = Time.realtimeSinceStartup;
            CalculateTimerDuration();
            Utils.ClearLogs();
            Debug.Log("Start");
            Debug.Log(currentAction);
        }
        else if(currentState == ActionState.IN_PROGRESS)
        {
            if(Time.realtimeSinceStartup > startActionTime + currentTimerDuration)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        currentState = ActionState.GAME_OVER;
    }

    private void CalculateTimerDuration()
    {
        currentTimerDuration = (actionTimer - minTimer) * Mathf.Pow(decreaseTimer, actionCount) + minTimer;
    }

    private Action CreateRandomAction()
    {
        ActionType type = ActionType.CLICK; //(ActionType)UnityEngine.Random.Range(0, 3);
        string target = "";
        if(type == ActionType.PRESS_KEY)
        {
            char ascii = (char)UnityEngine.Random.Range(65, 91);
            target = ascii.ToString();
        }else if(type == ActionType.CLICK)
        {
            target = RandomFishTarget();
        }
        return new Action(type, target);
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
    }

    public string RandomFishTarget()
    {
        int rand = UnityEngine.Random.Range(0, 4);
        switch(rand)
        {
            case 1:
                return "head";
            case 2:
                return "body";
            case 3:
                return "tail";
            default:
                return null;
        }
    }

}
