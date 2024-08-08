using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public UnityEvent<GameObject> response;

    //Register GameEvent Listeners
    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    //Unregister GameEvent Listeners
    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    //Invoke Unity Event
    public void OnEventRaised(GameObject gameObject)
    {
        response.Invoke(gameObject);
    }
}
