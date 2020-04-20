using System.Collections.Generic;
using UnityEngine;

public class RandomActionList : List<Action>
{
    public static readonly int MAX_ACTIONS = 40;
    public void Init()
    {
        Clear();
        FillListWithRandomActions();
        Shuffle();
        this[0] = new Action(true, ActionType.CLICK); //first action always click on the fish
        RandomNotDoItAction();
    }

    private void FillListWithRandomActions()
    {
        ActionType type;
        string target = null;
        for (int i = 0; i < MAX_ACTIONS; i++)
        {
            if (i < MAX_ACTIONS * 0.6)
            {
                type = Random.Range(0f, 1f) > 0.5f ? ActionType.HOLD : ActionType.CLICK;
                target = RandomFishTarget();
            }
            else if (i < MAX_ACTIONS * 0.9)
            {
                type = ActionType.PRESS_KEY;
                target = ((char)Random.Range(65, 91)).ToString();
            }
            else
            {
                type = Random.Range(0f, 1f) > 0.5f ? ActionType.MUTE : ActionType.QUIT;
                target = null;
            }

            Add(new Action(true, type, target));
        }
    }

    private void RandomNotDoItAction()
    {
        int doItIndex = Random.Range(5, 11);
        do
        {
            this[doItIndex].doIt = false;
            doItIndex += Random.Range(3, 8);
        } while (doItIndex < this.Count);
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

    public string RandomFishTarget()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 1:
                return "head";
            case 2:
                return "body";
          /*  case 3:
                return "tail";*/
            default:
                return null;
        }
    }
}
