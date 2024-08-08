using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerSequence : MonoBehaviour
{
    public static UIManagerSequence instance;

    [Header("Sequence Holders")]
    public List<GameObject> sequenceObj;

    [Header("Arrow Direction Buttons")]
    public Button leftBtn;
    public Button rightBtn;
    public Button upBtn;
    public Button downBtn;

    [Header("Move Direction Objects")]
    public GameObject leftArrowObj;
    public GameObject rightArrowObj;
    public GameObject upArrowObj; 
    public GameObject downArrowObj;

    [Header("UI Popups")]
    public GameObject levelCompletePopup;
    public GameObject levelFailPopup;

    private void Awake()
    {
        if (instance == null)
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

    //Spawning Move Direction object in Sequence holders according to click of Arrow Direction button
    public void OnClickOfBtn(Direction dir)
    {
        for (int i = 0; i < sequenceObj.Count; i++)
        {
            //Checking Seuquence holder contains Move direction object
            if (sequenceObj[i].transform.childCount == 0)
            {
                switch (dir)
                {
                    case Direction.Left:
                        Instantiate(leftArrowObj, sequenceObj[i].transform);
                        break;

                    case Direction.Right:
                        Instantiate(rightArrowObj, sequenceObj[i].transform);
                        break;

                    case Direction.Up:
                        Instantiate(upArrowObj, sequenceObj[i].transform);
                        break;

                    case Direction.Down:
                        Instantiate(downArrowObj, sequenceObj[i].transform);
                        break;

                }
                break;
            }
        }
    }

    //Remove Move Direction object from Sequence holder
    public void OnClickOfClearBtn(GameObject removeObj)
    {
        sequenceObj.Remove(removeObj);
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
