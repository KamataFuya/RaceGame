using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public RaceSystem raceSystem;
    public int NumberOfLaps;

    private int transitionCount;

    private void Update()
    {
        //if (NumberOfLaps <= raceSystem.lapCount) //ゴールしたら
        //{
        //    transitionCount++;
        //}
        //if(transitionCount >= 30)
        //{
        //    ChangeScene(string nextScene);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//playerに衝突されたら
        {
            if (!raceSystem.startFlag)
            {
                raceSystem.StartJudge();
                
            }
            if (raceSystem.checkCount >= 4 && raceSystem.startFlag) //全てのチェックポイントを通過している、かつスタートしていたら
            {
                raceSystem.LapCount(); //周回数加算
            }

            if (NumberOfLaps <= raceSystem.lapCount)//周回数に達した状態でゴールラインに触れたら
            {
                raceSystem.Finish();
            }
        }
    }

    public void ChangeScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }
}
