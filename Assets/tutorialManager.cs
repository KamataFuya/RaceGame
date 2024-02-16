using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextScene();
        }
    }

    // 次のシーン読み込み
    public void LoadNextScene()
    {
        // 次のシーンへ遷移
        SceneManager.LoadScene("ModeSelect");
    }
}
