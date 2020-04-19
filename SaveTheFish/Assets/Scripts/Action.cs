
using System;

public enum ActionType { CLICK, HOLD, PRESS_KEY, MUTE, QUIT }
public class Action
{
    public bool doIt { get; private set; }
    public ActionType type { get; private set; }
    public string target { get; private set; }

    public Action()
    {

    }

    public Action(bool doIt, ActionType type, string target = null)
    {
        this.doIt = doIt;
        this.type = type;
        this.target = target;
    }

    public bool IsValid(Action action)
    {
        if (doIt != action.doIt)
            return false;
        if (type != action.type)
            return false;
        if (!string.IsNullOrEmpty(target) && target != action.target)
            return false;
        return true;
    }

    public override string ToString()
    {
        return (doIt ? "Fish says ": "") + TypeToString(type) + 
            ( string.IsNullOrEmpty(target) ? "" : " " + target);
    }

    public string ShortLabel()
    {
        return TypeToString(type) +
            (string.IsNullOrEmpty(target) ? "" : " " + target);
    }

    private string TypeToString(ActionType type)
    {
        return type.ToString().Replace("_", " ");
    }

    public static Action PressKey(string key)
    {
        return new Action(true, ActionType.PRESS_KEY, key);
    }

    public static Action Click(string target)
    {
        return new Action(true, ActionType.CLICK, target);
    }

    public static Action Hold(string target)
    {
        return new Action(true, ActionType.HOLD, target);
    }
}
