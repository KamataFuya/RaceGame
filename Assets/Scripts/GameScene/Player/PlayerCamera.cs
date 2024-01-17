using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour
{
    //�Q�[���I�u�W�F�N�g�擾
    public GameObject Chase;
    public GameObject Onboard;
    public GameObject Fullscreen;
    public GameObject ChaseB;
    public GameObject OnboardB;
    public GameObject FullscreenB;
    //public GameObject Player;

    //�ϐ�
    private int cameraNum; //0:�`�F�C�X,1:�I���{�[�h,2:�t���X�N���[��
    private bool BackViewFlag; //false:����,true:�o�b�N�r���[ON

    private UnityEngine.Vector3 chasePos;
    private UnityEngine.Vector3 onboardPos;
    private UnityEngine.Vector3 fullscreenPos;
    private UnityEngine.Vector3 bChasePos;
    private UnityEngine.Vector3 bOnboardPos;
    private UnityEngine.Vector3 bFullscreenPos;
    private UnityEngine.Quaternion rotation;
    private UnityEngine.Quaternion bRotation; //������_�̎��̌���(180����������)

    private UnityEngine.Vector3 cameraDirection; //�L�[���͕ێ��p

    public Text textComponent; //�m�F�p

    void Start()
    {
        //�J�����̏�����
        Chase.gameObject.SetActive(true);
        Onboard.gameObject.SetActive(false);
        Fullscreen.gameObject.SetActive(false);
        cameraNum = 0;
        //�����̐ݒ�
        rotation = Quaternion.Euler(0, 0, 0);
        bRotation = Quaternion.Euler(0, 180, 0);
    }

    private void Update()
    {
        ChangeCamera();
        SetCamera();
    }

    private void ChangeCamera()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            if (cameraNum < 2)
            {
                cameraNum++;
            }
            else
            {
                cameraNum = 0;
            }
        }
        if (Input.GetKey(KeyCode.B) || Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            BackViewFlag = true;
        }
        else
        {
            BackViewFlag = false;
        }
        textComponent.text = "cameraNum : " + cameraNum;
        //textComponent.text = "BackViewFlag : " + BackViewFlag;
    }

    private void SetCamera()
    {
        //�`�F�C�X���_
        if (cameraNum == 0)
        {
            Fullscreen.gameObject.SetActive(false);
            if (BackViewFlag)
            {
                ChaseB.gameObject.SetActive(true);
                Chase.gameObject.SetActive(false);
            }
            else
            {
                ChaseB.gameObject.SetActive(false);
                Chase.gameObject.SetActive(true);
            }
        }
        //�I���{�[�h���_
        if (cameraNum == 1)
        {
            Chase.gameObject.SetActive(false);
            if (BackViewFlag)
            {
                OnboardB.gameObject.SetActive(true);
                Onboard.gameObject.SetActive(false);
            }
            else
            {
                OnboardB.gameObject.SetActive(false);
                Onboard.gameObject.SetActive(true);
            }
        }
        //�t���X�N���[��
        if (cameraNum == 2)
        {
            Onboard.gameObject.SetActive(false);
            if (BackViewFlag)
            {
                FullscreenB.gameObject.SetActive(true);
                Fullscreen.gameObject.SetActive(false);
            }
            else
            {
                FullscreenB.gameObject.SetActive(false);
                Fullscreen.gameObject.SetActive(true);
            }
        }
    }

    ////���ʃJ����
    //public Camera Chase = default;
    //public Camera OnBoard = default;
    //public Camera FullScreen = default;
    ////����J����
    //public Camera ChaseB = default;
    //public Camera OnBoardB = default;
    //public Camera FullScreenB = default;
    ////�m�F�p
    //public Text cameraText;

    //private int CamCount; //�J�����ɔԍ���U�蕪���A�Ǘ�����

    //// Start is called before the first frame update
    //void Start()
    //{
    //    // ����������
    //    //�������_��ݒ�
    //    Chase = Camera.main;
    //    //�`�F�C�X���_�����C���Ƃ��A�L����
    //    ActiveChaseCamera();
    //    CamCount = 0;
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    //�Q�[���p�b�h�̏�Ԏ擾
    //    var gamepad = Gamepad.current;
    //    //���_�؂�ւ�
    //    ChangePerspective(gamepad.leftShoulder.isPressed, gamepad.rightShoulder.isPressed);

    //}

    ////�֐� : �`�F�C�X���_��L����
    //public void ActiveChaseCamera()
    //{
    //    Chase.gameObject.SetActive(true);
    //    OnBoard.gameObject.SetActive(false);
    //    FullScreen.gameObject.SetActive(false);
    //    ChaseB.gameObject.SetActive(false);
    //    OnBoardB.gameObject.SetActive(false);
    //    FullScreenB.gameObject.SetActive(false);
    //}

    ////�֐� : �I���{�[�h���_��L����
    //public void ActiveOnBoardCamera()
    //{
    //    Chase.gameObject.SetActive(false);
    //    OnBoard.gameObject.SetActive(true);
    //    FullScreen.gameObject.SetActive(false);
    //    ChaseB.gameObject.SetActive(false);
    //    OnBoardB.gameObject.SetActive(false);
    //    FullScreenB.gameObject.SetActive(false);
    //}

    ////�֐� : �t���X�N���[�����_��L����
    //public void ActiveFullScreenCamera()
    //{
    //    Chase.gameObject.SetActive(false);
    //    OnBoard.gameObject.SetActive(false);
    //    FullScreen.gameObject.SetActive(true);
    //    ChaseB.gameObject.SetActive(false);
    //    OnBoardB.gameObject.SetActive(false);
    //    FullScreenB.gameObject.SetActive(false);
    //}

    ////�֐� : �`�F�C�X(���)���_��L����
    //public void ActiveChaseBCamera()
    //{
    //    Chase.gameObject.SetActive(false);
    //    OnBoard.gameObject.SetActive(false);
    //    FullScreen.gameObject.SetActive(false);
    //    ChaseB.gameObject.SetActive(true);
    //    OnBoardB.gameObject.SetActive(false);
    //    FullScreenB.gameObject.SetActive(false);
    //}

    ////�֐� : �I���{�[�h(���)���_��L����
    //public void ActiveOnBoardBCamera()
    //{
    //    Chase.gameObject.SetActive(false);
    //    OnBoard.gameObject.SetActive(false);
    //    FullScreen.gameObject.SetActive(false);
    //    ChaseB.gameObject.SetActive(false);
    //    OnBoardB.gameObject.SetActive(true);
    //    FullScreenB.gameObject.SetActive(false);
    //}

    ////�֐� : �t���X�N���[��(���)���_��L����
    //public void ActiveFullScreenBCamera()
    //{
    //    Chase.gameObject.SetActive(false);
    //    OnBoard.gameObject.SetActive(false);
    //    FullScreen.gameObject.SetActive(false);
    //    ChaseB.gameObject.SetActive(false);
    //    OnBoardB.gameObject.SetActive(false);
    //    FullScreenB.gameObject.SetActive(true);
    //}

    ////���_�؂�ւ�
    //public void ChangePerspective(bool leftShoulder, bool rightShoulder)
    //{
    //    //�o�b�N�r���[
    //    if (leftShoulder/* || Input.GetKey(KeyCode.Space)*/)
    //    {
    //        //���_�ԍ��̐؂�ւ� 2:FS,1:OB,0:C,-1:CB,-2:OBC,-3:FSB
    //        if (CamCount == 0)
    //        {
    //            CamCount = -1;
    //        }
    //        if(CamCount == 1)
    //        {
    //            CamCount = -2;
    //        }
    //        if(CamCount == 2)
    //        {
    //            CamCount = -3;
    //        }
    //    }
    //    else
    //    {
    //        if (CamCount == -1)
    //        {
    //            CamCount = 0;
    //        }
    //        if (CamCount == -2)
    //        {
    //            CamCount = 1;
    //        }
    //        if (CamCount == -3)
    //        {
    //            CamCount = 2;
    //        }
    //    }

    //    //���ʂ̊e���_�̐؂�ւ�
    //    if (rightShoulder)
    //    {
    //        if(CamCount == 0)
    //        {
    //            CamCount = 1; //�`�F�C�X�̎��̓I���{�[�h
    //        }
    //        if(CamCount == 1)
    //        {
    //            CamCount = 2; //�I���{�[�h�̎��̓t���X�N���[��
    //        }
    //        if(CamCount == 2)
    //        {
    //            CamCount = 0; //�t���X�N���[���̎��̓`�F�C�X
    //        }
    //    }

    //    //�J�����̃A�N�e�B�u�ݒ�
    //    if(CamCount == -3)
    //    {
    //        ActiveFullScreenBCamera();
    //    }
    //    if(CamCount == -2)
    //    {
    //        ActiveOnBoardBCamera();
    //    }
    //    if(CamCount == -1)
    //    {
    //        ActiveChaseBCamera();
    //    }
    //    if(CamCount == 0)
    //    {
    //        ActiveChaseCamera();
    //    }
    //    if(CamCount == 1)
    //    {
    //        ActiveOnBoardCamera();
    //    }
    //    if(CamCount == 2)
    //    {
    //        ActiveFullScreenCamera();
    //    }

    //    //�ԍ��m�F
    //    Debug.Log(CamCount);
    //    cameraText.text = "CameraNum : " + CamCount.ToString();
    //}
}
