using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelect : MonoBehaviour
{
    public Text[] modeTexts; // モードのテキスト
    private int selectedMode = 0; // 選択中のモード

    private void Update()
    {
        // 矢印キーの上下で選択を変更
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeSelection(1);
        }

        // スペースキーで選択したモードに遷移
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectMode();
        }
    }

    // 選択肢の変更
    private void ChangeSelection(int direction)
    {
        selectedMode = (selectedMode + direction + modeTexts.Length) % modeTexts.Length;
        UpdateHighlight();
    }

    // 選択したモードに遷移
    private void SelectMode()
    {
        string sceneName = ""; // 各モードに対応するシーン名

        // 選択したモードに応じてシーン名を設定
        switch (selectedMode)
        {
            case 0:
                sceneName = "StageSelect";
                break;
            case 1:
                sceneName = "StageSelect";
                break;
                // 他のモードがあればここに追加
        }

        // シーンへ遷移
        SceneManager.LoadScene(sceneName);
    }

    // ハイライトの更新
    private void UpdateHighlight()
    {
        // 全てのモードのテキストを通常の色に戻す
        foreach (var text in modeTexts)
        {
            text.color = Color.black;
        }

        // 選択中のモードのテキストをハイライト
        modeTexts[selectedMode].color = Color.yellow;
    }
}
