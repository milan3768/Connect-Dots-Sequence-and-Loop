using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManagerLoop : MonoBehaviour
{
    [Header("Pointer and Line Object")]
    public GameObject pointer;
    public LineRenderer pointerLine;

    [Header("Patterns")]
    [SerializeField] List<Direction> drawnPattern = new List<Direction>();
    /// <summary>
    /// Set directions of Result pattern in the inspector
    /// </summary>
    public List<Direction> resultPattern;

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
        MoveDirectionSequence moveDirectionSequence = null;
        MoveDirectionLoop moveDirectionLoop = null;

        foreach (GameObject go in UIManagerLoop.instance.loopHolderObjs)
        {
            //Checking for Move direction object in sequence holders
            if (go.transform.childCount != 0)
            {
                //Move Direction Loop object
                if (go.GetComponentInChildren<MoveDirectionLoop>())
                {
                    moveDirectionLoop = go.GetComponentInChildren<MoveDirectionLoop>();
                    //Checking for direction
                    switch (moveDirectionLoop.direction)
                    {
                        case Direction.Left:
                            //Left move position with loop
                            for (int i = 0; i < moveDirectionLoop.count; i++)
                            {
                                desiredPos = new Vector3(pointer.transform.position.x - 1f, pointer.transform.position.y, 0f);
                                StartCoroutine(MovePointerAndDrawLine(desiredPos, Direction.Left));
                                yield return new WaitForSeconds(0.5f);
                            }
                            break;

                        case Direction.Right:
                            //Right move position with loop
                            for (int i = 0; i < moveDirectionLoop.count; i++)
                            {
                                desiredPos = new Vector3(pointer.transform.position.x + 1f, pointer.transform.position.y, 0f);
                                StartCoroutine(MovePointerAndDrawLine(desiredPos, Direction.Right));
                                yield return new WaitForSeconds(0.5f);
                            }
                            break;

                        case Direction.Up:
                            //Up move position with loop
                            for (int i = 0; i < moveDirectionLoop.count; i++)
                            {
                                desiredPos = new Vector3(pointer.transform.position.x, pointer.transform.position.y + 1f, 0f);
                                StartCoroutine(MovePointerAndDrawLine(desiredPos, Direction.Up));
                                yield return new WaitForSeconds(0.5f);
                            }
                            break;

                        case Direction.Down:
                            //Down move position with loop
                            for (int i = 0; i < moveDirectionLoop.count; i++)
                            {
                                desiredPos = new Vector3(pointer.transform.position.x, pointer.transform.position.y - 1f, 0f);
                                StartCoroutine(MovePointerAndDrawLine(desiredPos, Direction.Down));
                                yield return new WaitForSeconds(0.5f);
                            }
                            break;
                    }
                }
                //Move Direction Sequence object
                else
                {
                    moveDirectionSequence = go.GetComponentInChildren<MoveDirectionSequence>();
                    //Checking for direction
                    switch (moveDirectionSequence.direction)
                    {
                        case Direction.Left:
                            //Left move position
                            desiredPos = new Vector3(pointer.transform.position.x - 1f, pointer.transform.position.y, 0f);
                            StartCoroutine(MovePointerAndDrawLine(desiredPos, Direction.Left));
                            break;

                        case Direction.Right:
                            //RIght move position
                            desiredPos = new Vector3(pointer.transform.position.x + 1f, pointer.transform.position.y, 0f);
                            StartCoroutine(MovePointerAndDrawLine(desiredPos, Direction.Right));
                            break;

                        case Direction.Up:
                            //Up move position
                            desiredPos = new Vector3(pointer.transform.position.x, pointer.transform.position.y + 1f, 0f);
                            StartCoroutine(MovePointerAndDrawLine(desiredPos, Direction.Up));
                            break;

                        case Direction.Down:
                            //Down move position
                            desiredPos = new Vector3(pointer.transform.position.x, pointer.transform.position.y - 1f, 0f);
                            StartCoroutine(MovePointerAndDrawLine(desiredPos, Direction.Down));
                            break;
                    }

                    yield return new WaitForSeconds(0.5f);
                } 
            }
        }

        yield return new WaitForSeconds(0.5f);
        CheckForResult();
    }

    //Move pointer object to next position and draw line
    IEnumerator MovePointerAndDrawLine(Vector3 movePos, Direction dir)
    {
        //Checking position is inside the point grid
        if ((movePos.x >= 0f && movePos.x <= 3f) && (movePos.y >= 0f && movePos.y <= 3f))
        {
            //Moving pointer
            pointer.transform.DOMove(movePos, 0.5f);
            yield return new WaitForSeconds(0.15f);

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
                    UIManagerLoop.instance.ShowLevelFailPopup();
                    break;
                }
            }

            //Level completed becuase resultPattern is same as drawnPattern
            UIManagerLoop.instance.ShowLevelCompletePopup();
        }
        else
        {
            //Level failed becuase resultPattern and drawnPattern counts are not same
            UIManagerLoop.instance.ShowLevelFailPopup();
        }
    }
}
