using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;
using UnityEngine.UI;
using Unity.VisualScripting;

public class RaceSystem : MonoBehaviour
{
    //変数
    public int checkCount;//チェックポイント通過数
    public int lapCount;//ラップ数
    private float seconds, minutes;//タイム
    public bool startFlag; //スタートしたか
    private bool goalFlag; //ゴールしたかどうか
    private float lapTsec01, lapTminutes01;
    private float[] lapTimeSeconds; //配列でタイムを管理
    private float[] lapTimeMinutes;
    private float fastestTime; //ファステストラップタイム保存用

    //テキスト
    public Text textComponent;
    public Text laps;
    public Text start;
    public Text finish;
    public Text time;
    public Text rank;
    public Text reverse;
    public Text lapTime01;

    //オブジェクト
    public GameObject CheckPoint01;
    public GameObject CheckPoint02;
    public GameObject CheckPoint03;
    public GameObject CheckPoint04;

    // Start is called before the first frame update
    void Start()
    {
        startFlag = false;
        goalFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFlag && !goalFlag)
        {
            RaceTimer();
        }
    }

    //チェックポイントを通過したときの処理
    public void PassTheCheckpoint()
    {
        if (!goalFlag)
        {
            checkCount++;
            Debug.Log("CheckCount : " + checkCount);
            laps.text = "CheckCount : " + checkCount.ToString();
        }
    }

    //ラップカウント
    public void LapCount()
    {
        //周回数加算
        lapCount++;
        //チェックポイント復活
        ReturnCheckPoint();
        //チェックカウントリセット
        checkCount = 0;
        //確認用
        Debug.Log("lapCount : " + lapCount);
        textComponent.text = "lapCount : " + lapCount.ToString();
    }

    //チェックポイント復活
    public void ReturnCheckPoint()
    {
        CheckPoint01.gameObject.SetActive(true);
        CheckPoint02.gameObject.SetActive(true);
        CheckPoint03.gameObject.SetActive(true);
        CheckPoint04.gameObject.SetActive(true);
    }

    //スタート処理
    public void StartJudge()
    {
        start.text = "Start!!!".ToString();
        startFlag = true;
    }

    //ゴール処理
    public void Finish()
    {
        finish.text = "Finish!!!".ToString();
        goalFlag = true;
    }

    //レースタイマー
    public void RaceTimer()
    {
        //時間の計測
        if (!goalFlag)
        {
            seconds += Time.deltaTime; //秒単位の計測
        }
        if (seconds >= 60)
        {
            minutes++; //分単位の計測
            seconds -= 60;
        }

        //時間を表示
        time.text = minutes.ToString("00") + " : " + ((int)seconds).ToString("00");
    }

    //順位取得
    public void ObtainRank()
    {
        //順位取得

        //順位表示

    }

    //逆走判定
    public void ReverseRunningJudge()
    {
        //逆走していることをプレイヤーに伝える

    }

    //ラップタイム取得
    public void ObtainLapTime()
    {
        //取得
        if(lapCount == 1)
        {
            lapTminutes01 = minutes;
            lapTsec01 = seconds;
        }

        //表示
        lapTime01.text = lapTminutes01.ToString("00") + " : " + ((int)lapTsec01).ToString("00");

        //配列で管理したい
        //for(int i = 0; i <= lapCount; i++)
        //{
        //    if(lapCount > i)
        //    {
        //        lapTimeSeconds[i] = seconds;
        //        lapTimeMinutes[i] = minutes;
        //    }
        //}
    }

    //ラップタイムの記録
    private void RecordTime()
    {
        //記録する数を設定(周回した分だけ記録する)
        int saveNum = GetComponent<Goal>().NumberOfLaps;
        //whileを使って降順でデータを上書きしていく
        while (saveNum >= 1)
        {
            int loadNum = saveNum - 1;

        }
    }

    //ラップタイム保存
    private void SaveRecord()
    {

    }

    //ファステストラップ判定、表示
    private void UpdateFastestTime()
    {
        //既存のファステストラップよりも早いラップタイムが出たら
        //if (/*lapTime01 < fastestTime*/)
        //{
        //    //fastestTime = //laptime;
        //}
    }
}
