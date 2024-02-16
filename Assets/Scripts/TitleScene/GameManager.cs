using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
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
        SceneManager.LoadScene("Turorial");
    }

}
