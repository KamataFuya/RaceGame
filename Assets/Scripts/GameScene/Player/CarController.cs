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

    //�m�F�p�e�L�X�g
    public Text textComponent;
    [SerializeField] GameObject ReverseRunning;

    //�������Ă���`�F�b�N�|�C���g
    CheckPoint nowCheckpoint;
    //�ʉ߂����`�F�b�N�|�C���g��
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
        float motor = maxMotorTorque * Input.GetAxis("Vertical"); //�㉺���L�[
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal"); //���E���L�[

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

        ////���l�v�Z
        //var v = nowCheckpoint.nextCheckPoint.transform.position - transform.position;
        //v.y = 0;
        //v = v.normalized * Time.deltaTime * Speed;

        //var p1 = p0 + v;

        //transform.position = p1;
        //transform.rotation = UnityEngine.Quaternion.LookRotation(v);

        ////�t���`�F�b�N
        //if (nowCheckpoint.CheckReverseRunning(p0, p1))
        //{
        //    //�t��
        //    ReverseRunning.SetActive(true);
        //}
        //else
        //{
        //    //�t�����ĂȂ�
        //    ReverseRunning.SetActive(false);
        //}

        //���]�����畜�A������


        //���l�`�F�b�N�p
        textComponent.text = "transform.x : " + transform.position.x;
    }

    //public WheelCollider[] wheelColliders;
    //public float MaxSpeed; //�ō��������߂�ϐ�
    //public float MaxBackSpeed;
    //public float AccelerationPower; //�����͂����߂�ϐ�
    //public float SwingPower; //����͂����߂�ϐ�
    //public float steerAngle;
    //public float Speed; //�O�i���鑬��
    //public float BackSpeed; //��ނ��鑬��
    //public new Rigidbody rigidbody;
    ////public RectTransform info;

    ////�^�C�}�[
    //private int OverturnTimer; //���]���C������܂ł̃^�C�}�[

    ////�t���O
    //private bool backFlag;

    ////�M�A
    //private int gearNum;

    ////���͕ێ��p
    //private UnityEngine.Vector3 inputDirection; //�O�i�A�u���[�L
    //private UnityEngine.Vector3 steeringDirection; //���E
    //private UnityEngine.Vector3 backDirection; //���
    //private UnityEngine.Vector3 cameraDirection; //�J�����؂�ւ�
    //private UnityEngine.Vector3 optionDirection; //���j���[��ʌĂяo��
    //private UnityEngine.Vector3 gearDirection; //�M�A�`�F���W

    //private UnityEngine.Vector3 moveValue;

    //InputAction steeringKey, moveKey, cameraKey, backKey, optionKey, gearKey;

    ////�������Ă���`�F�b�N�|�C���g
    //CheckPoint nowCheckpoint;
    ////�ʉ߂����`�F�b�N�|�C���g��
    //int _checkCount;
    //public int checkCount => _checkCount;

    ////public GetBottons input;
    ////private GameInputs inputs;

    ////�m�F�p�e�L�X�g
    //public Text textComponent;
    //[SerializeField] GameObject ReverseRunning;

    //private void Awake()
    //{
    //    //rigidbody = GetComponent<Rigidbody>();
    //    //// GameInputs�̃C���X�^���X����
    //    //inputs = new @GameInputs();
    //    //// ���ꂼ���InputAction�̖���������U��
    //    //inputs.Player.AccelorBrake.started += OnMove;
    //    //inputs.Player.AccelorBrake.performed += OnMove; //�A�N�Z���ƃu���[�L
    //    //inputs.Player.AccelorBrake.canceled += OnMove;
    //    //// GameInputs�̗L����
    //    //inputs.Enable();

    //    //�ϐ�������
    //    Speed = 0.0f;
    //    backFlag = false;
    //    nowCheckpoint = CheckPoint.StartPoint;
    //    transform.position = nowCheckpoint.transform.position;

    //    //���͊֌W
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
    //    //���g�ŃC���X�^���X������Action�N���X��IDisposable���������Ă���̂ŕK��Dispose����
    //    //inputs?.Dispose();
    //}

    //private void Update()
    //{
    //    //�L�[���͂̎擾
    //    var inputSteerAxis = steeringKey.ReadValue<UnityEngine.Vector2>();
    //    var inputMoveAxis = moveKey.ReadValue<UnityEngine.Vector2>();
    //    var inputCameraAxis = cameraKey.ReadValue<UnityEngine.Vector2>();
    //    var inputBackAxis = backKey.ReadValue<UnityEngine.Vector2>();
    //    var inputOptionAxis = optionKey.ReadValue<UnityEngine.Vector2>();
    //    var inputGearAxis = gearKey.ReadValue<UnityEngine.Vector2>();
    //    //�L�[���͕ێ�
    //    inputDirection.y = inputMoveAxis.x; //�A�N�Z��
    //    inputDirection.x = inputMoveAxis.y; //�u���[�L
    //    steeringDirection.y = inputSteerAxis.x; //�E
    //    steeringDirection.x = inputSteerAxis.y; //��
    //    backDirection.x = inputBackAxis.y; //���
    //    gearDirection.y = inputGearAxis.y;
    //    //������Ɠ��͂���Ă��邩�m�F
    //    Debug.Log("inputDirection.y : " + inputDirection.y.ToString());
    //    Debug.Log("inputDirection.x : " + inputDirection.x.ToString());
    //    Debug.Log("steeringDirection.y : " + steeringDirection.y.ToString());
    //    Debug.Log("steeringDirection.x : " + steeringDirection.x.ToString());
    //    Debug.Log("backDirection.x : " + backDirection.x.ToString());
    //    Debug.Log("backFlag : " + backFlag.ToString());

    //    //input.InputSteer();

    //    //���@����

    //    var p0 = transform.position;

    //    //�A�N�Z��
    //    if (inputDirection.x > 0)
    //    {
    //        if (Speed < 120) //120�ŉ������Ȃ��Ȃ�
    //        {
    //            //���x = �A�N�Z���J�x �~ �����x �� 150 (��150�͒����p�Ɋ����Ă邾��)
    //            Speed += inputDirection.x * AccelerationPower / 150;
    //        }
    //    }
    //    //��������(�A�N�Z���𓥂�ł��Ȃ���)
    //    if (Speed >= 0)
    //    {
    //        Speed -= AccelerationPower * Time.deltaTime / 10;
    //    }
    //    else
    //    {
    //        Speed += AccelerationPower * Time.deltaTime / 10;
    //    }
    //    //��ޏ���
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
    //    //���x��+�ɂȂ�����t���O��false��
    //    else
    //    {
    //        backFlag = false;
    //    }



    //    //�u���[�L �� �u���[�L�������Ă���ƁA���x��0�̎��ɔ��ʂ̑��x���������o�O���c���Ă���B
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
    //    //���x��0�������̂�h��
    //    if (Speed < 0 && !backFlag)
    //    {
    //        Speed = 0;
    //    }

    //    //���x�̔��f
    //    rigidbody.velocity = transform.forward * Speed;
    //    //���񏈗�
    //    steerAngle = steeringDirection.y;
    //    //���x��0�łȂ���ΐ���
    //    if (Speed != 0)
    //    {
    //        transform.Rotate(UnityEngine.Vector3.up, SwingPower * steerAngle * Time.deltaTime);
    //    }

    //    //�M�A�`�F���W
    //    if (gearDirection.y > 0) //�V�t�g�A�b�v
    //    {
    //        if (gearNum < 6) //�M�A�͍ő�6���܂�
    //        {
    //            gearNum++;
    //        }
    //    }
    //    else if (gearDirection.y < 0) //�V�t�g�_�E��
    //    {
    //        if (gearNum > 0) //�M�A�͍Œ�1���܂�
    //        {
    //            gearNum--;
    //        }
    //    }

    //    ////�ړ������̗͂�������
    //    //rigidbody.AddForce(
    //    //    new UnityEngine.Vector3(
    //    //        0,
    //    //        0,
    //    //        moveValue.x
    //    //        )
    //    //    );

    //    //���l�v�Z
    //    var v = nowCheckpoint.nextCheckPoint.transform.position - transform.position;
    //    v.y = 0;
    //    v = v.normalized * Time.deltaTime * Speed;

    //    var p1 = p0 + v;

    //    transform.position = p1;
    //    transform.rotation = UnityEngine.Quaternion.LookRotation(v);

    //    //�t���`�F�b�N
    //    if (nowCheckpoint.CheckReverseRunning(p0, p1))
    //    {
    //        //�t��
    //        ReverseRunning.SetActive(true);
    //    }
    //    else
    //    {
    //        //�t�����ĂȂ�
    //        ReverseRunning.SetActive(false);
    //    }

    //    textComponent.text = "steeringDirection.y : " + steeringDirection.y;


    //}

    //private void OnMove(InputAction.CallbackContext context)
    //{
    //    // Move�A�N�V�����̓��͎擾
    //    moveValue = context.ReadValue<UnityEngine.Vector3>();
    //}


    //���� UP / DOWN -> y, RIGHT / LEFT -> x

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    //�L�[���͂̎擾
    //    //var inputMoveAxis = move.ReadValue<Vector2>();
    //    var inputSteerAxis = steering.ReadValue<float>();
    //    //�L�[���͕ێ�
    //    //inputDirection.z = inputMoveAxis.x;
    //    //inputDirection.x = inputMoveAxis.y;

    //    //�ړ����x

    //    //Debug.Log("inputDirection.z : " + inputDirection.z);

    //    //�Q�[���p�b�h�̏����擾
    //    var gamepad = Gamepad.current;
    //    //if (gamepad == null)
    //    //{
    //    //    return; //�ڑ�����Ă��Ȃ���΃��^�[��
    //    //}

    //    if(inputDirection != Vector3.zero)
    //    {
    //        Debug.Log("Accel!!!!");
    //    }


    //    //�����̌v�Z
    //    if (/*gamepad.rightTrigger.isPressed || */Input.GetKey(KeyCode.W)) //�A�N�Z��
    //    {
    //        //����
    //        Speed += AccelerationPower * Time.deltaTime * gamepad.rightTrigger.ReadValue();
    //        if (Speed > MaxSpeed)
    //        {
    //            Speed = MaxSpeed;
    //        }
    //    }
    //    //�A�N�Z���𗣂��ƌ�������
    //    else
    //    {
    //        if (!gamepad.yButton.isPressed) //�o�b�N�����Ă���Ƃ������O
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
    //    if (gamepad.leftTrigger.isPressed || Input.GetKey(KeyCode.DownArrow)) //�u���[�L
    //    {
    //        //����
    //        Speed -= AccelerationPower * Time.deltaTime / 2;

    //        //���x��0�����ɂȂ�Ȃ�
    //        if (Speed < 0)
    //        {
    //            Speed = 0;
    //        }
    //    }


    //    if (Speed <= 0)
    //    {
    //        if (gamepad.yButton.isPressed) //�o�b�N
    //        {

    //            if (Speed > -MaxBackSpeed)
    //            {
    //                //���ɉ�����
    //                Speed -= AccelerationPower * Time.deltaTime / 2; //�ō����x�ȏ�ɂ͂Ȃ�Ȃ�
    //            }
    //        }

    //    }

    //    //���x�̔��f
    //    rigidbody.velocity = transform.forward * Speed;        

    //    //����p�x�̌v�Z
    //    if (Speed != 0) //���x��0�̎��͐��񂵂Ȃ�
    //    {
    //        if (Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal") > 0)
    //        {
    //            steerAngle = Input.GetAxis("Horizontal");
    //            transform.Rotate(Vector3.up, SwingPower * steerAngle * Time.deltaTime);
    //        }
    //    }

    //    //�T�C�h�u���[�L
    //    //if (gamepad.bButton.isPressed)
    //    //{

    //    //}

    //    //�V�t�g�`�F���W
    //    if(gamepad.xButton.isPressed) //�V�t�g�_�E��
    //    {

    //    }
    //    if (gamepad.aButton.isPressed) //�V�t�g�A�b�v
    //    {

    //    }

    //    //���]�C��
    //    if(transform.localEulerAngles.x >= 90)
    //    {
    //        if(OverturnTimer <= 25) {
    //            OverturnTimer++;
    //        }
    //        else
    //        {
    //            OverturnTimer = 0;
    //            transform.Rotate(new Vector3(0,0,0), SwingPower * steerAngle * Time.deltaTime); //���]���畜�A
    //        }

    //    }

    //    //�^�C��

    //}

}
