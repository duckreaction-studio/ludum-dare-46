
using System;

public enum ActionType { CLICK, HOLD, PRESS_KEY, MUTE, QUIT }
public class Action
{
    protected ActionType type;
    protected string target;

    public Action()
    {

    }

    public Action(ActionType type, string target)
    {
        this.type = type;
        this.target = target;
    }

    public bool IsValid(Action action)
    {
        if (type != action.type)
            return false;
        if (!string.IsNullOrEmpty(target) && target != action.target)
            return false;
        return true;
    }

    public override string ToString()
    {
        return type.ToString() + " " + target;
    }

    public static Action PressKey(string key)
    {
        return new Action(ActionType.PRESS_KEY, key);
    }

    public static Action Click(string target)
    {
        return new Action(ActionType.CLICK, target);
    }

    public static Action Hold(string target)
    {
        return new Action(ActionType.HOLD, target);
    }
}
