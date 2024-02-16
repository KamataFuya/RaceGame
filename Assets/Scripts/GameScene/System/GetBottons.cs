using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using UnityEngine.UI;
using XInputDotNetPure;

//入力処理管理クラス
public class GetBottons : MonoBehaviour 
{
    // インスタンスへのアクセスを簡単にするためのシングルトンパターン
    public static GetBottons Instance { get; private set; }

    // コントローラーのプレイヤー番号
    public PlayerIndex playerIndex = PlayerIndex.One;

    // コントローラーの入力状態を保持する変数
    private GamePadState state;
    private GamePadState prevState;

    // 初期化
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 毎フレームの更新
    private void Update()
    {
        // コントローラーの状態を更新
        prevState = state;
        state = GamePad.GetState(playerIndex);
    }

    // ボタンが押されたかどうかをチェックするメソッド
    public bool GetButtonDown(Button button)
    {
        switch (button)
        {
            case Button.A:
                return state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released;
            case Button.B:
                return state.Buttons.B == ButtonState.Pressed && prevState.Buttons.B == ButtonState.Released;
            case Button.X:
                return state.Buttons.X == ButtonState.Pressed && prevState.Buttons.X == ButtonState.Released;
            case Button.Y:
                return state.Buttons.Y == ButtonState.Pressed && prevState.Buttons.Y == ButtonState.Released;
            case Button.UP:
                return state.DPad.Up == ButtonState.Pressed && prevState.DPad.Up == ButtonState.Released;
            case Button.DOWN:
                return state.DPad.Down == ButtonState.Pressed && prevState.DPad.Down == ButtonState.Released;
            case Button.RIGHT:
                return state.DPad.Right == ButtonState.Pressed && prevState.DPad.Right == ButtonState.Released;
            case Button.LEFT:
                return state.DPad.Left == ButtonState.Pressed && prevState.DPad.Left == ButtonState.Released;
            case Button.R:
                return state.Buttons.RightShoulder == ButtonState.Pressed && prevState.Buttons.RightShoulder == ButtonState.Released;
            case Button.L:
                return state.Buttons.LeftShoulder == ButtonState.Pressed && prevState.Buttons.LeftShoulder == ButtonState.Released;
            case Button.Start:
                return state.Buttons.Start == ButtonState.Pressed && prevState.Buttons.Start == ButtonState.Released;
            case Button.Back:
                return state.Buttons.Back == ButtonState.Pressed && prevState.Buttons.Back == ButtonState.Released;
            case Button.Guide:
                return state.Buttons.Guide == ButtonState.Pressed && prevState.Buttons.Guide == ButtonState.Released;
            // 他のボタンも同様に追加
            default:
                return false;
        }
    }

    // アナログスティックの値を取得するメソッド
    public Vector2 GetLeftStick()
    {
        return new Vector2(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
    }

    public Vector2 GetRightStick()
    {
        return new Vector2(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
    }

    // 左右のトリガーキーの値を取得するメソッド
    public float GetLeftTrigger()
    {
        return state.Triggers.Left;
    }

    public float GetRightTrigger()
    {
        return state.Triggers.Right;
    }

    // 左右のトリガーキーを押した瞬間を取得
    public bool GetLeftTriggerDown(float threshold)
    {
        return state.Triggers.Left >= threshold && prevState.Triggers.Left < threshold;
    }

    public bool GetRightTriggerDown(float threshold)
    {
        return state.Triggers.Right >= threshold && prevState.Triggers.Right < threshold;
    }

    // 左右のトリガーキーの離した瞬間を取得
    public bool GetLeftTriggerUp(float threshold)
    {
        return state.Triggers.Left < threshold && prevState.Triggers.Left >= threshold;
    }

    public bool GetRightTriggerUp(float threshold)
    {
        return state.Triggers.Right < threshold && prevState.Triggers.Right >= threshold;
    }

    /// <summary>
    /// コントローラーを指定した強さと長さで振動させる
    /// </summary>
    /// <param name="intensity">振動する強さ(0.0f~1.0f)</param>
    /// <param name="duration">振動させる秒数</param>
    /// <returns></returns>
    IEnumerator StartVibration(float intensity, float duration)
    {
        // コントローラーを指定した強さと長さで振動させる
        GamePad.SetVibration(playerIndex, intensity, intensity);

        // 振動の長さだけ待機する
        yield return new WaitForSeconds(duration);

        // 振動を停止する
        GamePad.SetVibration(playerIndex, 0, 0);
    }

    // ボタンの種類を定義する列挙型
    public enum Button
    {
        A,
        B,
        X,
        Y,
        UP,
        DOWN,
        RIGHT,
        LEFT,
        R,
        L,
        Start,
        Back,
        Guide, //XBOXボタン
    }

}
