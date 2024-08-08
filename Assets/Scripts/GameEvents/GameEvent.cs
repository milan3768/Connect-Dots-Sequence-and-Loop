using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "InGameEvents", menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    //Add GameEvent Listener
    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    //Remove GameEvent Listener
    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }

    //Raise Listerner with GameObject
    public void Raise(GameObject gameObject)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(gameObject);
        }
    }
}
