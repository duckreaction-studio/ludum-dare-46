using System.Collections.Generic;
using UnityEngine;

public class ActionSequence : List<Action>
{
    public static readonly int MAX_ACTIONS = 100;
    public void Init()
    {
        Clear();
        FillListWithRandomActions();
        Shuffle();
    }

    private void FillListWithRandomActions()
    {
        bool doIt = Random.Range(0f, 1f) > 0.1f;
        ActionType type;
        string target = null;
        for (int i = 0; i < MAX_ACTIONS; i++)
        {
            if (i < 60)
            {
                type = Random.Range(0f, 1f) > 0.5f ? ActionType.HOLD : ActionType.CLICK;
                target = RandomFishTarget();
            }
            else if (i < 90)
            {
                type = ActionType.PRESS_KEY;
                target = ((char)Random.Range(65, 91)).ToString();
            }
            else
            {
                type = Random.Range(0f, 1f) > 0.5f ? ActionType.MUTE : ActionType.QUIT;
                target = null;
            }
            Add(new Action(doIt, type, target));
        }
    }

    private void Shuffle()
    {
        for(int i = 0; i < 1000; i++)
        {
            int a = Random.Range(0, MAX_ACTIONS);
            int b = Random.Range(0, MAX_ACTIONS);
            var tmp = this[a];
            this[a] = this[b];
            this[b] = tmp;
        }
    }

    /*    private Action CreateRandomAction()
        {
            ActionType type = (ActionType)UnityEngine.Random.Range(0, 5);
            string target = "";
            if (type == ActionType.PRESS_KEY)
            {
                char ascii = (char)Random.Range(65, 91);
                target = ascii.ToString();
            }
            else if (type == ActionType.CLICK)
            {
                target = RandomFishTarget();
            }
            bool doIt = UnityEngine.Random.Range(0f, 1f) > 0.5f;
            return new Action(doIt, type, target);
        }*/

    public string RandomFishTarget()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
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
