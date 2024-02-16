using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    public Image[] stageImages; // ステージの画像
    private int selectedStage = 0; // 選択中のステージ

    private void Update()
    {
        // 左右矢印キーで選択を変更
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSelection(1);
        }

        // スペースキーで選択したステージに遷移
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StageSelect();
        }
    }

    // 選択肢の変更
    private void ChangeSelection(int direction)
    {
        selectedStage = (selectedStage + direction + stageImages.Length) % stageImages.Length;
        UpdateHighlight();
    }

    // 選択したステージに遷移
    private void StageSelect()
    {
        string sceneName = ""; // 各ステージに対応するシーン名

        // 選択したステージに応じてシーン名を設定
        switch (selectedStage)
        {
            case 0:
                sceneName = "Cource01";
                break;
            case 1:
                sceneName = "Cource01";
                break;
                // 他のステージがあればここに追加
        }

        // シーンへ遷移
        SceneManager.LoadScene(sceneName);
    }

    // ハイライトの更新
    private void UpdateHighlight()
    {
        // 全てのステージの画像を通常の色に戻す
        foreach (var image in stageImages)
        {
            image.color = Color.white;
        }

        // 選択中のステージの画像をハイライト
        stageImages[selectedStage].color = Color.yellow;
    }
}
