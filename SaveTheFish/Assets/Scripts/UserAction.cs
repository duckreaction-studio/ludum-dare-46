
using System;

public enum ActionType { CLICK, HOLD, PRESS_KEY, MUTE, QUIT }
public class UserAction
{
    public bool doIt { get; set; }
    public ActionType type { get; private set; }
    public string target { get; private set; }

    public UserAction()
    {

    }

    public UserAction(bool doIt, ActionType type, string target = null)
    {
        this.doIt = doIt;
        this.type = type;
        this.target = target;
    }

    public bool IsValid(UserAction action)
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

    public static UserAction PressKey(string key)
    {
        return new UserAction(true, ActionType.PRESS_KEY, key);
    }

    public static UserAction Click(string target)
    {
        return new UserAction(true, ActionType.CLICK, target);
    }

    public static UserAction Hold(string target)
    {
        return new UserAction(true, ActionType.HOLD, target);
    }
}
