using Helper;
using UnityEngine;

public class InputManager : SingletonSaved<InputManager>
{
    public void OnGUI()
    {
        if(Event.current != null && Event.current.type == EventType.KeyUp)
        {
            string key = Event.current.keyCode.ToString();
            Debug.Log("Press " + key);
            ActionManager.Instance.DoAction(Action.PressKey(key));
        }
    }
}
