using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerLoop : MonoBehaviour
{
    public static UIManagerLoop instance;

    [Header("Loop Holders")]
    public List<GameObject> loopHolderObjs;

    [Header("Loop Toggle")]
    public Toggle loopToggle;

    [Header("Arrow Direction Buttons")]
    public Button leftBtn;
    public Button rightBtn;
    public Button upBtn; 
    public Button downBtn;

    [Header("Move Direction Objects SEQUENCE")]
    /// <summary>
    /// Move Object Sequence is for single movement of pointer
    /// </summary>
    public GameObject leftArrowObj;
    public GameObject rightArrowObj;
    public GameObject upArrowObj;
    public GameObject downArrowObj;

    [Header("Move Direction Objects LOOP")]
    /// <summary>
    /// Move Object Loop is for loop movement of pointer
    /// </summary>
    public GameObject leftLoopArrowObj;
    public GameObject rightLoopArrowObj;
    public GameObject upLoopArrowObj;
    public GameObject downLoopArrowObj;

    [Header("UI Popups")]
    public GameObject levelCompletePopup;
    public GameObject levelFailPopup;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        //Adding on click listener for Arrow Direction buttons
        leftBtn.onClick.AddListener(() => OnClickOfBtn(Direction.Left));
        rightBtn.onClick.AddListener(() => OnClickOfBtn(Direction.Right));
        upBtn.onClick.AddListener(() => OnClickOfBtn(Direction.Up));
        downBtn.onClick.AddListener(() => OnClickOfBtn(Direction.Down));
    }

    //Spawning Move Direction Loop/Sequence object in Loop holders according to click of Arrow Direction button
    public void OnClickOfBtn(Direction dir)
    {
        for (int i = 0; i < loopHolderObjs.Count; i++)
        {
            //Checking loop holder contains Move direction object
            if (loopHolderObjs[i].transform.childCount == 0)
            {
                //Spawn Move Direction Loop object as loop toggle is on
                if (loopToggle.isOn)
                {
                    switch (dir)
                    {
                        case Direction.Left:
                            Instantiate(leftLoopArrowObj, loopHolderObjs[i].transform);
                            break;

                        case Direction.Right:
                            Instantiate(rightLoopArrowObj, loopHolderObjs[i].transform);
                            break;

                        case Direction.Up:
                            Instantiate(upLoopArrowObj, loopHolderObjs[i].transform);
                            break;

                        case Direction.Down:
                            Instantiate(downLoopArrowObj, loopHolderObjs[i].transform);
                            break;
                    }
                }
                //Spawn Move Direction Sequence object as loop toggle is off
                else
                {
                    switch (dir)
                    {
                        case Direction.Left:
                            Instantiate(leftArrowObj, loopHolderObjs[i].transform);
                            break;

                        case Direction.Right:
                            Instantiate(rightArrowObj, loopHolderObjs[i].transform);
                            break;

                        case Direction.Up:
                            Instantiate(upArrowObj, loopHolderObjs[i].transform);
                            break;

                        case Direction.Down:
                            Instantiate(downArrowObj, loopHolderObjs[i].transform);
                            break;
                    }
                }
                break;
            }
        }
    }

    //Remove Move Direction object from Loop holder
    public void OnClickOfClearBtn(GameObject removeObj)
    {
        loopHolderObjs.Remove(removeObj);
        Destroy(removeObj);
    }

    //Level Complete Popup
    public void ShowLevelCompletePopup()
    {
        levelCompletePopup.SetActive(true);
    }

    //Level Failed Popup
    public void ShowLevelFailPopup()
    {
        levelFailPopup.SetActive(true);
    }

    //Reload current scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Back to scene selection
    public void BackToSelectionScene()
    {
        SceneManager.LoadScene(0);
    }
}
