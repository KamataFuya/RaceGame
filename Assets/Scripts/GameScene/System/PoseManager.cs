using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PoseManager : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public Text[] menuTexts;
    private int selectedTextIndex = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1; // ゲームを通常の速度で再生

        // 初期選択テキストを設定
        SelectText(selectedTextIndex);

        // テキストを非表示にする
        HideTextList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        // 矢印キーでテキスト選択
        if (pauseMenuUI.activeSelf)
        {
            

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedTextIndex = (selectedTextIndex - 1 + menuTexts.Length) % menuTexts.Length;
                SelectText(selectedTextIndex);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedTextIndex = (selectedTextIndex + 1) % menuTexts.Length;
                SelectText(selectedTextIndex);
            }

            // スペースキーでテキストを選択
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteSelectedOption();
            }          
        }
    }

    public void TogglePauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        ShowTextList();
        Time.timeScale = pauseMenuUI.activeSelf ? 0 : 1;
        Cursor.lockState = pauseMenuUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = pauseMenuUI.activeSelf;

        // ポーズメニューが開かれたとき、最初のテキストを選択する
        if (pauseMenuUI.activeSelf)
        {
            selectedTextIndex = 0;
        }
    }

    private void SelectText(int index)
    {
        // 選択中のテキストにフォーカスを当てる
        for (int i = 0; i < menuTexts.Length; i++)
        {
            menuTexts[i].color = (i == index) ? Color.cyan : Color.white;
        }
    }

    private void ExecuteSelectedOption()
    {
        // 選択されたテキストによって処理を分岐
        switch (selectedTextIndex)
        {
            case 0:
                Resume();

                break;
            case 1:
                Restart();
                break;
            case 2:
                QuitToTitle();
                break;
        }
    }

    private void Resume()
    {
        TogglePauseMenu();
    }

    private void Restart()
    {
        // 現在のシーンを再読み込み
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    private void QuitToTitle()
    {
        // タイトル画面のシーン名に変更
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        Time.timeScale = 1;
    }

    void HideTextList()
    {
        foreach (Text textElement in menuTexts)
        {
            textElement.gameObject.SetActive(false);
        }
    }

    void ShowTextList()
    {
        foreach (Text textElement in menuTexts)
        {
            textElement.gameObject.SetActive(!textElement.gameObject.activeSelf);
        }
    }

}
