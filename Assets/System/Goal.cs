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
        //if (NumberOfLaps <= raceSystem.lapCount) //ƒS[ƒ‹‚µ‚½‚ç
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
        if (other.gameObject.tag == "Player")//player‚ÉÕ“Ë‚³‚ê‚½‚ç
        {
            if (!raceSystem.startFlag)
            {
                raceSystem.StartJudge();
            }

            if (raceSystem.checkCount >= 4 && raceSystem.startFlag)
            {
                raceSystem.LapCount();
            }

            if (NumberOfLaps <= raceSystem.lapCount)//Žü‰ñ”‚É’B‚µ‚½ó‘Ô‚ÅƒS[ƒ‹ƒ‰ƒCƒ“‚ÉG‚ê‚½‚ç
            {
                raceSystem.Finish();
            }
            raceSystem.ObtainLapTime();
        }
    }

    public void ChangeScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }
}
