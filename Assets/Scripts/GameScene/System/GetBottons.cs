using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//入力処理管理クラス
public class GetBottons : MonoBehaviour 
{
    ////変数
    ////入力保持用
    //private UnityEngine.Vector3 inputDirection; //前進、ブレーキ
    //public UnityEngine.Vector3 steeringDirection; //左右
    //private UnityEngine.Vector3 backDirection; //後退
    //private UnityEngine.Vector3 cameraDirection; //カメラ切り替え
    //private UnityEngine.Vector3 optionDirection; //メニュー画面呼び出し
    //private UnityEngine.Vector3 gearDirection; //ギアチェンジ

    //InputAction steeringKey, moveKey, cameraKey, backKey, optionKey, gearKey;

    //void Start()
    //{
    //    //入力関係
    //    var playerInput = GetComponent<PlayerInput>();
    //    steeringKey = playerInput.actions["Steering"];
    //    moveKey = playerInput.actions["AccelorBrake"];
    //    cameraKey = playerInput.actions["Camera"];
    //    backKey = playerInput.actions["Back"];
    //    optionKey = playerInput.actions["Option"];
    //    gearKey = playerInput.actions["Gear"];
    //}
    //private void Update()
    //{
    //    InputSteer();
    //}

    ////ステアリング --> ここで値をいじって他のクラスでここの変数を取得すればよいのでは...?
    //public void InputSteer()
    //{
    //    //入力情報取得、保持
    //    var inputSteerAxis = steeringKey.ReadValue<UnityEngine.Vector2>();
    //    steeringDirection.y = inputSteerAxis.x; //右
    //    steeringDirection.x = inputSteerAxis.y; //左       
    //}

    ////
}
