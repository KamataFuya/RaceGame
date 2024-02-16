using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RearViewMirror : MonoBehaviour
{
    public Camera rearCamera; // 後方のカメラ
    public RawImage mirrorImage; // ミラーUIのRawImage

    private RenderTexture renderTexture; // カメラ映像をテクスチャとして格納する変数

    void Start()
    {
        // カメラ映像をテクスチャとして格納するRenderTextureを作成
        renderTexture = new RenderTexture(Screen.width / 2, Screen.height / 2, 0);
        rearCamera.targetTexture = renderTexture;

        // ミラーUIのRawImageにテクスチャを設定
        mirrorImage.texture = renderTexture;
    }

    void Update()
    {
        // ミラーUIの表示を更新
        mirrorImage.enabled = IsInRearViewMode();
    }

    // 後方を確認するモードかどうかを判定するメソッド
    bool IsInRearViewMode()
    {
        // 例として、プレーヤーが後方を確認するためのキーを押しているかどうかで判定する
        return true; // この部分は必要に応じて変更してください
    }
}
