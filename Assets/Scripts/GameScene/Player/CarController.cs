using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    //確認用テキスト
    public Text textComponent;
    [SerializeField] GameObject ReverseRunning;

    //今走っているチェックポイント
    CheckPoint nowCheckpoint;
    //通過したチェックポイント数
    int _checkCount;
    public int checkCount => _checkCount;

    private void Awake()
    {
        //nowCheckpoint = CheckPoint.StartPoint;
        //transform.position = nowCheckpoint.transform.position;
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        UnityEngine.Vector3 position;
        UnityEngine.Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical"); //上下矢印キー
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal"); //左右矢印キー

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }

        ////数値計算
        //var v = nowCheckpoint.nextCheckPoint.transform.position - transform.position;
        //v.y = 0;
        //v = v.normalized * Time.deltaTime * Speed;

        //var p1 = p0 + v;

        //transform.position = p1;
        //transform.rotation = UnityEngine.Quaternion.LookRotation(v);

        ////逆走チェック
        //if (nowCheckpoint.CheckReverseRunning(p0, p1))
        //{
        //    //逆走
        //    ReverseRunning.SetActive(true);
        //}
        //else
        //{
        //    //逆走してない
        //    ReverseRunning.SetActive(false);
        //}

        //横転したら復帰させる


        //数値チェック用
        textComponent.text = "transform.x : " + transform.position.x;
    }

    //public WheelCollider[] wheelColliders;
    //public float MaxSpeed; //最高速を決める変数
    //public float MaxBackSpeed;
    //public float AccelerationPower; //加速力を決める変数
    //public float SwingPower; //旋回力を決める変数
    //public float steerAngle;
    //public float Speed; //前進する速さ
    //public float BackSpeed; //後退する速さ
    //public new Rigidbody rigidbody;
    ////public RectTransform info;

    ////タイマー
    //private int OverturnTimer; //横転を修正するまでのタイマー

    ////フラグ
    //private bool backFlag;

    ////ギア
    //private int gearNum;

    ////入力保持用
    //private UnityEngine.Vector3 inputDirection; //前進、ブレーキ
    //private UnityEngine.Vector3 steeringDirection; //左右
    //private UnityEngine.Vector3 backDirection; //後退
    //private UnityEngine.Vector3 cameraDirection; //カメラ切り替え
    //private UnityEngine.Vector3 optionDirection; //メニュー画面呼び出し
    //private UnityEngine.Vector3 gearDirection; //ギアチェンジ

    //private UnityEngine.Vector3 moveValue;

    //InputAction steeringKey, moveKey, cameraKey, backKey, optionKey, gearKey;

    ////今走っているチェックポイント
    //CheckPoint nowCheckpoint;
    ////通過したチェックポイント数
    //int _checkCount;
    //public int checkCount => _checkCount;

    ////public GetBottons input;
    ////private GameInputs inputs;

    ////確認用テキスト
    //public Text textComponent;
    //[SerializeField] GameObject ReverseRunning;

    //private void Awake()
    //{
    //    //rigidbody = GetComponent<Rigidbody>();
    //    //// GameInputsのインスタンス生成
    //    //inputs = new @GameInputs();
    //    //// それぞれのInputActionの役割を割り振る
    //    //inputs.Player.AccelorBrake.started += OnMove;
    //    //inputs.Player.AccelorBrake.performed += OnMove; //アクセルとブレーキ
    //    //inputs.Player.AccelorBrake.canceled += OnMove;
    //    //// GameInputsの有効化
    //    //inputs.Enable();

    //    //変数初期化
    //    Speed = 0.0f;
    //    backFlag = false;
    //    nowCheckpoint = CheckPoint.StartPoint;
    //    transform.position = nowCheckpoint.transform.position;

    //    //入力関係
    //    var playerInput = GetComponent<PlayerInput>();
    //    steeringKey = playerInput.actions["Steering"];
    //    moveKey = playerInput.actions["AccelorBrake"];
    //    cameraKey = playerInput.actions["Camera"];
    //    backKey = playerInput.actions["Back"];
    //    optionKey = playerInput.actions["Option"];
    //    gearKey = playerInput.actions["Gear"];


    //}

    //private void OnDestroy()
    //{
    //    //自身でインスタンス化したActionクラスはIDisposableを実装しているので必ずDisposeする
    //    //inputs?.Dispose();
    //}

    //private void Update()
    //{
    //    //キー入力の取得
    //    var inputSteerAxis = steeringKey.ReadValue<UnityEngine.Vector2>();
    //    var inputMoveAxis = moveKey.ReadValue<UnityEngine.Vector2>();
    //    var inputCameraAxis = cameraKey.ReadValue<UnityEngine.Vector2>();
    //    var inputBackAxis = backKey.ReadValue<UnityEngine.Vector2>();
    //    var inputOptionAxis = optionKey.ReadValue<UnityEngine.Vector2>();
    //    var inputGearAxis = gearKey.ReadValue<UnityEngine.Vector2>();
    //    //キー入力保持
    //    inputDirection.y = inputMoveAxis.x; //アクセル
    //    inputDirection.x = inputMoveAxis.y; //ブレーキ
    //    steeringDirection.y = inputSteerAxis.x; //右
    //    steeringDirection.x = inputSteerAxis.y; //左
    //    backDirection.x = inputBackAxis.y; //後退
    //    gearDirection.y = inputGearAxis.y;
    //    //きちんと入力されているか確認
    //    Debug.Log("inputDirection.y : " + inputDirection.y.ToString());
    //    Debug.Log("inputDirection.x : " + inputDirection.x.ToString());
    //    Debug.Log("steeringDirection.y : " + steeringDirection.y.ToString());
    //    Debug.Log("steeringDirection.x : " + steeringDirection.x.ToString());
    //    Debug.Log("backDirection.x : " + backDirection.x.ToString());
    //    Debug.Log("backFlag : " + backFlag.ToString());

    //    //input.InputSteer();

    //    //自機動作

    //    var p0 = transform.position;

    //    //アクセル
    //    if (inputDirection.x > 0)
    //    {
    //        if (Speed < 120) //120で加速しなくなる
    //        {
    //            //速度 = アクセル開度 × 加速度 ÷ 150 (÷150は調整用に割ってるだけ)
    //            Speed += inputDirection.x * AccelerationPower / 150;
    //        }
    //    }
    //    //減速処理(アクセルを踏んでいない時)
    //    if (Speed >= 0)
    //    {
    //        Speed -= AccelerationPower * Time.deltaTime / 10;
    //    }
    //    else
    //    {
    //        Speed += AccelerationPower * Time.deltaTime / 10;
    //    }
    //    //後退処理
    //    if (Speed <= 0)
    //    {
    //        if (backDirection.x > 0)
    //        {
    //            backFlag = true;
    //            if (Speed > -60)
    //            {
    //                Speed -= AccelerationPower * Time.deltaTime / 2;
    //            }
    //        }
    //    }
    //    //速度が+になったらフラグをfalseに
    //    else
    //    {
    //        backFlag = false;
    //    }



    //    //ブレーキ → ブレーキをかけていると、速度が0の時に微量の速度が足されるバグが残っている。
    //    if (inputDirection.x < 0)
    //    {
    //        if (Speed >= 0)
    //        {
    //            Speed -= AccelerationPower * Time.deltaTime;
    //        }
    //        else
    //        {
    //            Speed += AccelerationPower * Time.deltaTime;
    //        }
    //    }
    //    //速度が0を下回るのを防ぐ
    //    if (Speed < 0 && !backFlag)
    //    {
    //        Speed = 0;
    //    }

    //    //速度の反映
    //    rigidbody.velocity = transform.forward * Speed;
    //    //旋回処理
    //    steerAngle = steeringDirection.y;
    //    //速度が0でなければ旋回
    //    if (Speed != 0)
    //    {
    //        transform.Rotate(UnityEngine.Vector3.up, SwingPower * steerAngle * Time.deltaTime);
    //    }

    //    //ギアチェンジ
    //    if (gearDirection.y > 0) //シフトアップ
    //    {
    //        if (gearNum < 6) //ギアは最大6速まで
    //        {
    //            gearNum++;
    //        }
    //    }
    //    else if (gearDirection.y < 0) //シフトダウン
    //    {
    //        if (gearNum > 0) //ギアは最低1速まで
    //        {
    //            gearNum--;
    //        }
    //    }

    //    ////移動方向の力を加える
    //    //rigidbody.AddForce(
    //    //    new UnityEngine.Vector3(
    //    //        0,
    //    //        0,
    //    //        moveValue.x
    //    //        )
    //    //    );

    //    //数値計算
    //    var v = nowCheckpoint.nextCheckPoint.transform.position - transform.position;
    //    v.y = 0;
    //    v = v.normalized * Time.deltaTime * Speed;

    //    var p1 = p0 + v;

    //    transform.position = p1;
    //    transform.rotation = UnityEngine.Quaternion.LookRotation(v);

    //    //逆走チェック
    //    if (nowCheckpoint.CheckReverseRunning(p0, p1))
    //    {
    //        //逆走
    //        ReverseRunning.SetActive(true);
    //    }
    //    else
    //    {
    //        //逆走してない
    //        ReverseRunning.SetActive(false);
    //    }

    //    textComponent.text = "steeringDirection.y : " + steeringDirection.y;


    //}

    //private void OnMove(InputAction.CallbackContext context)
    //{
    //    // Moveアクションの入力取得
    //    moveValue = context.ReadValue<UnityEngine.Vector3>();
    //}


    //メモ UP / DOWN -> y, RIGHT / LEFT -> x

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    //キー入力の取得
    //    //var inputMoveAxis = move.ReadValue<Vector2>();
    //    var inputSteerAxis = steering.ReadValue<float>();
    //    //キー入力保持
    //    //inputDirection.z = inputMoveAxis.x;
    //    //inputDirection.x = inputMoveAxis.y;

    //    //移動速度

    //    //Debug.Log("inputDirection.z : " + inputDirection.z);

    //    //ゲームパッドの情報を取得
    //    var gamepad = Gamepad.current;
    //    //if (gamepad == null)
    //    //{
    //    //    return; //接続されていなければリターン
    //    //}

    //    if(inputDirection != Vector3.zero)
    //    {
    //        Debug.Log("Accel!!!!");
    //    }


    //    //速さの計算
    //    if (/*gamepad.rightTrigger.isPressed || */Input.GetKey(KeyCode.W)) //アクセル
    //    {
    //        //加速
    //        Speed += AccelerationPower * Time.deltaTime * gamepad.rightTrigger.ReadValue();
    //        if (Speed > MaxSpeed)
    //        {
    //            Speed = MaxSpeed;
    //        }
    //    }
    //    //アクセルを離すと減速する
    //    else
    //    {
    //        if (!gamepad.yButton.isPressed) //バックをしているときも除外
    //        {
    //            if (Speed > 0)
    //            {
    //                Speed -= AccelerationPower * Time.deltaTime / 2;
    //            }
    //            else
    //            {
    //                Speed = 0.0f;
    //            }
    //        }
    //    }
    //    if (gamepad.leftTrigger.isPressed || Input.GetKey(KeyCode.DownArrow)) //ブレーキ
    //    {
    //        //減速
    //        Speed -= AccelerationPower * Time.deltaTime / 2;

    //        //速度が0未満にならない
    //        if (Speed < 0)
    //        {
    //            Speed = 0;
    //        }
    //    }


    //    if (Speed <= 0)
    //    {
    //        if (gamepad.yButton.isPressed) //バック
    //        {

    //            if (Speed > -MaxBackSpeed)
    //            {
    //                //後ろに下がる
    //                Speed -= AccelerationPower * Time.deltaTime / 2; //最高速度以上にはならない
    //            }
    //        }

    //    }

    //    //速度の反映
    //    rigidbody.velocity = transform.forward * Speed;        

    //    //旋回角度の計算
    //    if (Speed != 0) //速度が0の時は旋回しない
    //    {
    //        if (Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal") > 0)
    //        {
    //            steerAngle = Input.GetAxis("Horizontal");
    //            transform.Rotate(Vector3.up, SwingPower * steerAngle * Time.deltaTime);
    //        }
    //    }

    //    //サイドブレーキ
    //    //if (gamepad.bButton.isPressed)
    //    //{

    //    //}

    //    //シフトチェンジ
    //    if(gamepad.xButton.isPressed) //シフトダウン
    //    {

    //    }
    //    if (gamepad.aButton.isPressed) //シフトアップ
    //    {

    //    }

    //    //横転修正
    //    if(transform.localEulerAngles.x >= 90)
    //    {
    //        if(OverturnTimer <= 25) {
    //            OverturnTimer++;
    //        }
    //        else
    //        {
    //            OverturnTimer = 0;
    //            transform.Rotate(new Vector3(0,0,0), SwingPower * steerAngle * Time.deltaTime); //横転から復帰
    //        }

    //    }

    //    //タイヤ

    //}

}
