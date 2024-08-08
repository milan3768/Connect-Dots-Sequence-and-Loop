using TMPro;
using UnityEngine;

public class MoveDirectionLoop : MonoBehaviour
{
    [Header("Move Direction")]
    public Direction direction;

    [Header("Loop Count")]
    public int count = 2;
    [SerializeField] TextMeshProUGUI countTxt;

    [Space]
    public GameEvent removeMoveObjEvent;

    //Raise remove move object GameEvent
    public void RemoveMoveObject(GameObject obj)
    {
        removeMoveObjEvent.Raise(obj);
    }

    //Increase loop count
    public void IncreaseMoveCount()
    {
        count++;
        countTxt.text = count.ToString();
    }
}
