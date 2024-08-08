using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour
{
    //Load Sequence Game Scene
    public void LoadSequenceScene()
    {
        SceneManager.LoadScene("SequenceScene");
    }

    //Load Looping Game Scene
    public void LoadLoopingScene()
    {
        SceneManager.LoadScene("LoopScene");
    }
}
