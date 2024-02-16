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
        //if (NumberOfLaps <= raceSystem.lapCount) //�S�[��������
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
        if (other.gameObject.tag == "Player")//player�ɏՓ˂��ꂽ��
        {
            if (!raceSystem.startFlag)
            {
                raceSystem.StartJudge();
                
            }
            if (raceSystem.checkCount >= 4 && raceSystem.startFlag) //�S�Ẵ`�F�b�N�|�C���g��ʉ߂��Ă���A���X�^�[�g���Ă�����
            {
                raceSystem.LapCount(); //���񐔉��Z
            }

            if (NumberOfLaps <= raceSystem.lapCount)//���񐔂ɒB������ԂŃS�[�����C���ɐG�ꂽ��
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
