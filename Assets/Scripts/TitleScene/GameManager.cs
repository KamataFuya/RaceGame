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

    // ���̃V�[���ǂݍ���
    public void LoadNextScene()
    {
        // ���̃V�[���֑J��
        SceneManager.LoadScene("Turorial");
    }

}
