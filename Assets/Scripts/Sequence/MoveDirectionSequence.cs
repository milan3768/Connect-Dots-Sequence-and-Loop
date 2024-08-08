using UnityEngine;

public class MoveDirectionSequence : MonoBehaviour
{
    [Header("Move Direction")]
    public Direction direction;

    [Space]
    public GameEvent removeMoveObjEvent;

    //Raise remove move object GameEvent
    public void RemoveMoveObject(GameObject obj)
    {
        removeMoveObjEvent.Raise(obj);
    }
}
