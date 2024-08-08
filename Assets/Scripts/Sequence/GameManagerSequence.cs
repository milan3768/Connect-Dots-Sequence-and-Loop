using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManagerSequence : MonoBehaviour
{
    [Header("Pointer and Line Object")]
    [SerializeField] GameObject pointer;
    [SerializeField] LineRenderer pointerLine;

    [Header("Patterns")]
    [SerializeField] List<Direction> drawnPattern = new List<Direction>();
    /// <summary>
    /// Set directions of Result pattern in the inspector
    /// </summary>
    [SerializeField] List<Direction> resultPattern;

    int lineCount = 0;

    //This will start drawing pattern on click of GO button
    public void DrawPattern()
    {
        //Set first pointer position of line
        lineCount++;
        pointerLine.positionCount = lineCount;
        pointerLine.SetPosition(lineCount - 1, new Vector3(pointer.transform.position.x, pointer.transform.position.y, 0f));

        StartCoroutine(CheckForSequencePoint());
    }

    //Check for next point to move with direction
    IEnumerator CheckForSequencePoint()
    {
        Vector3 desiredPos = Vector3.zero;

        foreach (GameObject go in UIManagerSequence.instance.sequenceObj)
        {
            //Checking for Move direction object in sequence holders
            if (go.transform.childCount != 0)
            {
                //Checking for direction
                switch (go.GetComponentInChildren<MoveDirectionSequence>().direction)
                {
                    case Direction.Left:
                        //Left move position
                        desiredPos = new Vector3(pointer.transform.position.x - 1f, pointer.transform.position.y, 0f);
                        MovePointerAndDrawPattern(desiredPos, Direction.Left);
                        break;

                    case Direction.Right:
                        //Right move position
                        desiredPos = new Vector3(pointer.transform.position.x + 1f, pointer.transform.position.y, 0f);
                        MovePointerAndDrawPattern(desiredPos, Direction.Right);
                        break;

                    case Direction.Up:
                        //Up move position
                        desiredPos = new Vector3(pointer.transform.position.x, pointer.transform.position.y + 1f, 0f);
                        MovePointerAndDrawPattern(desiredPos, Direction.Up);
                        break;

                    case Direction.Down:
                        //Down move position
                        desiredPos = new Vector3(pointer.transform.position.x, pointer.transform.position.y - 1f, 0f);
                        MovePointerAndDrawPattern(desiredPos, Direction.Down);
                        break;
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        yield return new WaitForSeconds(0.5f);
        CheckForResult();
    }

    //Move pointer object to next position and draw line
    void MovePointerAndDrawPattern(Vector3 movePos, Direction dir)
    {
        //Checking position is inside the point grid
        if ((movePos.x >= 0f && movePos.x <= 3f) && (movePos.y >= 0f && movePos.y <= 3f))
        {
            //Moving pointer
            pointer.transform.DOMove(movePos, 0.5f);

            //Drawing line
            lineCount++;
            pointerLine.positionCount = lineCount;
            pointerLine.SetPosition(lineCount - 1, movePos);

            //Add current direction to drawnPattern list
            drawnPattern.Add(dir);
        }
        else
        {
            Debug.Log("Point is not on Grid!");
        }
    }

    //Checking for results
    void CheckForResult()
    {
        //Checking resultPattern and drawnPattern counts
        if (resultPattern.Count == drawnPattern.Count)
        {
            for (int i = 0; i < resultPattern.Count; i++)
            {
                //Level failed becuase resultPattern is not same as drawnPattern
                if (resultPattern[i] != drawnPattern[i])
                {
                    UIManagerSequence.instance.ShowLevelFailPopup();
                    break;
                }
            }

            //Level completed becuase resultPattern is same as drawnPattern
            UIManagerSequence.instance.ShowLevelCompletePopup();
        }
        else
        {
            //Level failed becuase resultPattern and drawnPattern counts are not same
            UIManagerSequence.instance.ShowLevelFailPopup();
        }
    }
}

//Move Direction Enum
public enum Direction
{
    Left,
    Right,
    Up,
    Down
}
