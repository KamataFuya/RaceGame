using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;
using UnityEngine.UI;
using Unity.VisualScripting;

public class RaceSystem : MonoBehaviour
{
    //�ϐ�
    public int checkCount;//�`�F�b�N�|�C���g�ʉߐ�
    public int lapCount;//���b�v��
    private float seconds, minutes;//�^�C��
    public bool startFlag; //�X�^�[�g������
    private bool goalFlag; //�S�[���������ǂ���
    private float lapTsec01, lapTminutes01;
    private float[] lapTimeSeconds; //�z��Ń^�C�����Ǘ�
    private float[] lapTimeMinutes;
    private float fastestTime; //�t�@�X�e�X�g���b�v�^�C���ۑ��p

    //�e�L�X�g
    public Text textComponent;
    public Text laps;
    public Text start;
    public Text finish;
    public Text time;
    public Text rank;
    public Text reverse;
    public Text lapTime01;

    //�I�u�W�F�N�g
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

    //�`�F�b�N�|�C���g��ʉ߂����Ƃ��̏���
    public void PassTheCheckpoint()
    {
        if (!goalFlag)
        {
            checkCount++;
            Debug.Log("CheckCount : " + checkCount);
            laps.text = "CheckCount : " + checkCount.ToString();
        }
    }

    //���b�v�J�E���g
    public void LapCount()
    {
        //���񐔉��Z
        lapCount++;
        //�`�F�b�N�|�C���g����
        ReturnCheckPoint();
        //�`�F�b�N�J�E���g���Z�b�g
        checkCount = 0;
        //�m�F�p
        Debug.Log("lapCount : " + lapCount);
        textComponent.text = "lapCount : " + lapCount.ToString();
    }

    //�`�F�b�N�|�C���g����
    public void ReturnCheckPoint()
    {
        CheckPoint01.gameObject.SetActive(true);
        CheckPoint02.gameObject.SetActive(true);
        CheckPoint03.gameObject.SetActive(true);
        CheckPoint04.gameObject.SetActive(true);
    }

    //�X�^�[�g����
    public void StartJudge()
    {
        start.text = "Start!!!".ToString();
        startFlag = true;
    }

    //�S�[������
    public void Finish()
    {
        finish.text = "Finish!!!".ToString();
        goalFlag = true;
    }

    //���[�X�^�C�}�[
    public void RaceTimer()
    {
        //���Ԃ̌v��
        if (!goalFlag)
        {
            seconds += Time.deltaTime; //�b�P�ʂ̌v��
        }
        if (seconds >= 60)
        {
            minutes++; //���P�ʂ̌v��
            seconds -= 60;
        }

        //���Ԃ�\��
        time.text = minutes.ToString("00") + " : " + ((int)seconds).ToString("00");
    }

    //���ʎ擾
    public void ObtainRank()
    {
        //���ʎ擾

        //���ʕ\��

    }

    //�t������
    public void ReverseRunningJudge()
    {
        //�t�����Ă��邱�Ƃ��v���C���[�ɓ`����

    }

    //���b�v�^�C���擾
    public void ObtainLapTime()
    {
        //�擾
        if(lapCount == 1)
        {
            lapTminutes01 = minutes;
            lapTsec01 = seconds;
        }

        //�\��
        lapTime01.text = lapTminutes01.ToString("00") + " : " + ((int)lapTsec01).ToString("00");

        //�z��ŊǗ�������
        //for(int i = 0; i <= lapCount; i++)
        //{
        //    if(lapCount > i)
        //    {
        //        lapTimeSeconds[i] = seconds;
        //        lapTimeMinutes[i] = minutes;
        //    }
        //}
    }

    //���b�v�^�C���̋L�^
    private void RecordTime()
    {
        //�L�^���鐔��ݒ�(���񂵂��������L�^����)
        int saveNum = GetComponent<Goal>().NumberOfLaps;
        //while���g���č~���Ńf�[�^���㏑�����Ă���
        while (saveNum >= 1)
        {
            int loadNum = saveNum - 1;

        }
    }

    //���b�v�^�C���ۑ�
    private void SaveRecord()
    {

    }

    //�t�@�X�e�X�g���b�v����A�\��
    private void UpdateFastestTime()
    {
        //�����̃t�@�X�e�X�g���b�v�����������b�v�^�C�����o����
        //if (/*lapTime01 < fastestTime*/)
        //{
        //    //fastestTime = //laptime;
        //}
    }
}
