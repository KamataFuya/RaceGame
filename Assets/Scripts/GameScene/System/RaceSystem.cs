using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;
using UnityEngine.UI;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;

public class RaceSystem : MonoBehaviour
{
    //�ϐ�
    public int checkCount;//�`�F�b�N�|�C���g�ʉߐ�
    public int lapCount;//���b�v��
    private float times, seconds, minutes;//�^�C��
    public bool startFlag; //�X�^�[�g������
    private bool goalFlag; //�S�[���������ǂ���
    private float lapTsec01, lapTminutes01;
    private float[] lapTimeSeconds; //�z��Ń^�C�����Ǘ�
    private float[] lapTimeMinutes;
    private float fastestTime = 999.999f; //�t�@�X�e�X�g���b�v�^�C���ۑ��p
    List<float> lapTimes = new List<float>(); //���b�v�^�C���ۑ��p���X�g
    List<Text> texts = new List<Text>(); //���b�v�^�C���\���p�e�L�X�g
    private bool poseFlag; // �|�[�Y��ʂ��J���Ă��邩���Ȃ����̔��ʃt���O

    List<GameObject> cp = new List<GameObject>();

    //�e�L�X�g
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

    //�I�u�W�F�N�g
    public GameObject CheckPoint01;
    public GameObject CheckPoint02;
    public GameObject CheckPoint03;
    public GameObject CheckPoint04;

    //�R�[�X�̑���(�_�[�g�Ȃǂ̔���p)
    public enum attribute
    {
        None,

        Road, //��
        Dart, //�_�[�g

        Max
    };

    public Texture2D attributeTexture = null;

    //���̃X�N���v�g
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
        //���b�v�^�C�������X�g�ɒǉ�
        lapTimes.Add(times);
        //���b�v�^�C���\��
        Laptimes();
        //�ő��̃��b�v�^�C�����X�V
        if(times < fastestTime)
        {
            fastestTime = times;
            FastestLap();
        }
        //���b�v�^�C�}�[���Z�b�g
        times = 0;
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
        //finish.text = "Finish!!!".ToString();
        //goalFlag = true;
    }

    //���[�X�^�C�}�[
    public void RaceTimer()
    {
        //���Ԃ̌v��
        times += Time.deltaTime;

        //���b�v�^�C�����������b�Ȃ̂��R���}001�b�܂Ōv�Z(99��59.999�b�ŃJ���X�g����悤�ɂ���)
        minutes = (int)(times / 60); //��
        seconds = (int)(times % 60); //�b
        float millisecond = times - UnityEngine.Mathf.FloorToInt(times); //�~���b

        //1�T�����烊�Z�b�g -> times��lap�����Z�����^�C�~���O��0�ɖ߂��B�ۑ������̃^�C�~���O�ł���B�x�X�g���b�v��


        //���Ԃ�\��
        time.text = minutes.ToString("00") + "'" + ((int)seconds).ToString("00") + millisecond.ToString(".000");

        
    }

    //���b�v�^�C���\���p
    public void Laptimes()
    {
        //���b�v�^�C���̈ꗗ��\��
        string lapTimesString = "Lap Times \n";
        for (int i = 0; i < lapTimes.Count; i++)
        {
            minutes = (int)(lapTimes[i] / 60); //��
            seconds = (int)(lapTimes[i] % 60); //�b
            float millisecond = lapTimes[i] - UnityEngine.Mathf.FloorToInt(lapTimes[i]); //�~���b
            lapTimesString = lapTimesString += "Lap " + (i + 1) + " : " + minutes.ToString("00") + "'" + ((int)seconds).ToString("00") + millisecond.ToString(".000") + "\n";
        }
        lapTimesText.text = lapTimesString;

        //���̎��񐔂𒴂���ƌÂ��^�C������\������߂�(�������A�t�@�X�e�X�g�̋L�^�݂͕̂ێ�)

    }

    //�t�@�X�e�X�g���b�v�\���p
    public void FastestLap()
    {
        fastestLapText.text = "Fastest Lap : " + fastestTime.ToString("F2");
        minutes = (int)(fastestTime / 60); //��
        seconds = (int)(fastestTime % 60); //�b
        float millisecond = fastestTime - UnityEngine.Mathf.FloorToInt(fastestTime); //�~���b
        fastestLapText.text = "Fastest Lap : " + minutes.ToString("00") + "'" + ((int)seconds).ToString("00") + millisecond.ToString(".000");
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
        ////����
        //if (checkPoint.CheckReverseRunning(p0, p1))
        //{
        //    //�t�����Ă���


        //    //�t�����Ă��邱�Ƃ��v���C���[�ɓ`����
        //    reverse.text = "�t��";
        //}
        //else
        //{
        //    //�t�����Ă��Ȃ�

        //}



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

    //�R�[�X�����̎擾
    //public attribute GetAttribute()
    //{

    //}

}
