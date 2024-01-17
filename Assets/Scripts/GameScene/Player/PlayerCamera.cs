using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour
{
    //ゲームオブジェクト取得
    public GameObject Chase;
    public GameObject Onboard;
    public GameObject Fullscreen;
    public GameObject ChaseB;
    public GameObject OnboardB;
    public GameObject FullscreenB;
    //public GameObject Player;

    //変数
    private int cameraNum; //0:チェイス,1:オンボード,2:フルスクリーン
    private bool BackViewFlag; //false:正面,true:バックビューON

    private UnityEngine.Vector3 chasePos;
    private UnityEngine.Vector3 onboardPos;
    private UnityEngine.Vector3 fullscreenPos;
    private UnityEngine.Vector3 bChasePos;
    private UnityEngine.Vector3 bOnboardPos;
    private UnityEngine.Vector3 bFullscreenPos;
    private UnityEngine.Quaternion rotation;
    private UnityEngine.Quaternion bRotation; //後方視点の時の向き(180°後ろを向く)

    private UnityEngine.Vector3 cameraDirection; //キー入力保持用

    public Text textComponent; //確認用

    void Start()
    {
        //カメラの初期化
        Chase.gameObject.SetActive(true);
        Onboard.gameObject.SetActive(false);
        Fullscreen.gameObject.SetActive(false);
        cameraNum = 0;
        //向きの設定
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
        //チェイス視点
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
        //オンボード視点
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
        //フルスクリーン
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

    ////正面カメラ
    //public Camera Chase = default;
    //public Camera OnBoard = default;
    //public Camera FullScreen = default;
    ////後方カメラ
    //public Camera ChaseB = default;
    //public Camera OnBoardB = default;
    //public Camera FullScreenB = default;
    ////確認用
    //public Text cameraText;

    //private int CamCount; //カメラに番号を振り分け、管理する

    //// Start is called before the first frame update
    //void Start()
    //{
    //    // 初期化処理
    //    //初期視点を設定
    //    Chase = Camera.main;
    //    //チェイス視点をメインとし、有効化
    //    ActiveChaseCamera();
    //    CamCount = 0;
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    //ゲームパッドの状態取得
    //    var gamepad = Gamepad.current;
    //    //視点切り替え
    //    ChangePerspective(gamepad.leftShoulder.isPressed, gamepad.rightShoulder.isPressed);

    //}

    ////関数 : チェイス視点を有効化
    //public void ActiveChaseCamera()
    //{
    //    Chase.gameObject.SetActive(true);
    //    OnBoard.gameObject.SetActive(false);
    //    FullScreen.gameObject.SetActive(false);
    //    ChaseB.gameObject.SetActive(false);
    //    OnBoardB.gameObject.SetActive(false);
    //    FullScreenB.gameObject.SetActive(false);
    //}

    ////関数 : オンボード視点を有効化
    //public void ActiveOnBoardCamera()
    //{
    //    Chase.gameObject.SetActive(false);
    //    OnBoard.gameObject.SetActive(true);
    //    FullScreen.gameObject.SetActive(false);
    //    ChaseB.gameObject.SetActive(false);
    //    OnBoardB.gameObject.SetActive(false);
    //    FullScreenB.gameObject.SetActive(false);
    //}

    ////関数 : フルスクリーン視点を有効化
    //public void ActiveFullScreenCamera()
    //{
    //    Chase.gameObject.SetActive(false);
    //    OnBoard.gameObject.SetActive(false);
    //    FullScreen.gameObject.SetActive(true);
    //    ChaseB.gameObject.SetActive(false);
    //    OnBoardB.gameObject.SetActive(false);
    //    FullScreenB.gameObject.SetActive(false);
    //}

    ////関数 : チェイス(後方)視点を有効化
    //public void ActiveChaseBCamera()
    //{
    //    Chase.gameObject.SetActive(false);
    //    OnBoard.gameObject.SetActive(false);
    //    FullScreen.gameObject.SetActive(false);
    //    ChaseB.gameObject.SetActive(true);
    //    OnBoardB.gameObject.SetActive(false);
    //    FullScreenB.gameObject.SetActive(false);
    //}

    ////関数 : オンボード(後方)視点を有効化
    //public void ActiveOnBoardBCamera()
    //{
    //    Chase.gameObject.SetActive(false);
    //    OnBoard.gameObject.SetActive(false);
    //    FullScreen.gameObject.SetActive(false);
    //    ChaseB.gameObject.SetActive(false);
    //    OnBoardB.gameObject.SetActive(true);
    //    FullScreenB.gameObject.SetActive(false);
    //}

    ////関数 : フルスクリーン(後方)視点を有効化
    //public void ActiveFullScreenBCamera()
    //{
    //    Chase.gameObject.SetActive(false);
    //    OnBoard.gameObject.SetActive(false);
    //    FullScreen.gameObject.SetActive(false);
    //    ChaseB.gameObject.SetActive(false);
    //    OnBoardB.gameObject.SetActive(false);
    //    FullScreenB.gameObject.SetActive(true);
    //}

    ////視点切り替え
    //public void ChangePerspective(bool leftShoulder, bool rightShoulder)
    //{
    //    //バックビュー
    //    if (leftShoulder/* || Input.GetKey(KeyCode.Space)*/)
    //    {
    //        //視点番号の切り替え 2:FS,1:OB,0:C,-1:CB,-2:OBC,-3:FSB
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

    //    //正面の各視点の切り替え
    //    if (rightShoulder)
    //    {
    //        if(CamCount == 0)
    //        {
    //            CamCount = 1; //チェイスの次はオンボード
    //        }
    //        if(CamCount == 1)
    //        {
    //            CamCount = 2; //オンボードの次はフルスクリーン
    //        }
    //        if(CamCount == 2)
    //        {
    //            CamCount = 0; //フルスクリーンの次はチェイス
    //        }
    //    }

    //    //カメラのアクティブ設定
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

    //    //番号確認
    //    Debug.Log(CamCount);
    //    cameraText.text = "CameraNum : " + CamCount.ToString();
    //}
}
