using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;
using UnityEngine.UI;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;

public class RaceSystem : MonoBehaviour
{
    //変数
    public int checkCount;//チェックポイント通過数
    public int lapCount;//ラップ数
    private float times, seconds, minutes;//タイム
    public bool startFlag; //スタートしたか
    private bool goalFlag; //ゴールしたかどうか
    private float lapTsec01, lapTminutes01;
    private float[] lapTimeSeconds; //配列でタイムを管理
    private float[] lapTimeMinutes;
    private float fastestTime = 999.999f; //ファステストラップタイム保存用
    List<float> lapTimes = new List<float>(); //ラップタイム保存用リスト
    List<Text> texts = new List<Text>(); //ラップタイム表示用テキスト
    private bool poseFlag; // ポーズ画面が開いているかいないかの判別フラグ

    List<GameObject> cp = new List<GameObject>();

    //テキスト
    public Text textComponent;
    public Text laps;
    public Text start;
    public Text finish;
    public Text time;
    public Text rank;
    public Text reverse;
    public Text lapTime01;
    public Text lapTimesText;
    public Text fastestLapText;

    //オブジェクト
    public GameObject CheckPoint01;
    public GameObject CheckPoint02;
    public GameObject CheckPoint03;
    public GameObject CheckPoint04;

    //コースの属性(ダートなどの判定用)
    public enum attribute
    {
        None,

        Road, //道
        Dart, //ダート

        Max
    };

    public Texture2D attributeTexture = null;

    //他のスクリプト
    public CheckPoint checkPoint;
    public CarController carController;

   
    // Start is called before the first frame update
    void Start()
    {
        startFlag = false;
        goalFlag = false;
        poseFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFlag && !goalFlag)
        {
            RaceTimer();
        }
        //RaceTimer();
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
        //ラップタイムをリストに追加
        lapTimes.Add(times);
        //ラップタイム表示
        Laptimes();
        //最速のラップタイムを更新
        if(times < fastestTime)
        {
            fastestTime = times;
            FastestLap();
        }
        //ラップタイマーリセット
        times = 0;
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
        //finish.text = "Finish!!!".ToString();
        //goalFlag = true;
    }

    //レースタイマー
    public void RaceTimer()
    {
        //時間の計測
        times += Time.deltaTime;

        //ラップタイムが何分何秒なのかコンマ001秒まで計算(99分59.999秒でカンストするようにする)
        minutes = (int)(times / 60); //分
        seconds = (int)(times % 60); //秒
        float millisecond = times - UnityEngine.Mathf.FloorToInt(times); //ミリ秒

        //1週したらリセット -> timesをlapが加算されるタイミングで0に戻す。保存もそのタイミングでする。ベストラップも


        //時間を表示
        time.text = minutes.ToString("00") + "'" + ((int)seconds).ToString("00") + millisecond.ToString(".000");

        
    }

    //ラップタイム表示用
    public void Laptimes()
    {
        //ラップタイムの一覧を表示
        string lapTimesString = "Lap Times \n";
        for (int i = 0; i < lapTimes.Count; i++)
        {
            minutes = (int)(lapTimes[i] / 60); //分
            seconds = (int)(lapTimes[i] % 60); //秒
            float millisecond = lapTimes[i] - UnityEngine.Mathf.FloorToInt(lapTimes[i]); //ミリ秒
            lapTimesString = lapTimesString += "Lap " + (i + 1) + " : " + minutes.ToString("00") + "'" + ((int)seconds).ToString("00") + millisecond.ToString(".000") + "\n";
        }
        lapTimesText.text = lapTimesString;

        //一定の周回数を超えると古いタイムから表示をやめる(ただし、ファステストの記録のみは保持)

    }

    //ファステストラップ表示用
    public void FastestLap()
    {
        fastestLapText.text = "Fastest Lap : " + fastestTime.ToString("F2");
        minutes = (int)(fastestTime / 60); //分
        seconds = (int)(fastestTime % 60); //秒
        float millisecond = fastestTime - UnityEngine.Mathf.FloorToInt(fastestTime); //ミリ秒
        fastestLapText.text = "Fastest Lap : " + minutes.ToString("00") + "'" + ((int)seconds).ToString("00") + millisecond.ToString(".000");
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
        ////判定
        //if (checkPoint.CheckReverseRunning(p0, p1))
        //{
        //    //逆走している


        //    //逆走していることをプレイヤーに伝える
        //    reverse.text = "逆走";
        //}
        //else
        //{
        //    //逆走していない

        //}



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

    //コース属性の取得
    //public attribute GetAttribute()
    //{

    //}

}
