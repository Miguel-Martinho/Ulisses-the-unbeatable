using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;
    private ButtonInfo buttonInfo;

    private Scene nextScene;

    private void Awake()
    {
        buttonInfo = GetComponent<ButtonInfo>();

        buttonInfo.OnSubmit += Submit;
    }

    private void Submit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}