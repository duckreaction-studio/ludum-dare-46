using Helper;
using UnityEngine;

public enum ActionState { UNKNOWN, IN_PROGRESS, DONE, GAME_OVER }
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

    public float remaingTime
    {
        get
        {
            return Mathf.Max(startActionTime + currentTimerDuration - Time.realtimeSinceStartup, 0);
        }
    }

    public void Update()
    {
        if(currentState == ActionState.UNKNOWN || currentState == ActionState.DONE)
        {
            currentState = ActionState.IN_PROGRESS;
            startActionTime = Time.realtimeSinceStartup;
            CalculateTimerDuration();
            Debug.Log("Start");
        }
        else if(currentState == ActionState.IN_PROGRESS)
        {
            if(Time.realtimeSinceStartup > startActionTime + currentTimerDuration)
            {
                Debug.Log("Game Over");
                currentState = ActionState.GAME_OVER;
            }
        }
    }

    private void CalculateTimerDuration()
    {
        currentTimerDuration = (actionTimer - minTimer) * Mathf.Pow(decreaseTimer, actionCount) + minTimer;
    }
}
