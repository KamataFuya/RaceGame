using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    public Image[] stageImages; // �X�e�[�W�̉摜
    private int selectedStage = 0; // �I�𒆂̃X�e�[�W

    private void Update()
    {
        // ���E���L�[�őI����ύX
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSelection(1);
        }

        // �X�y�[�X�L�[�őI�������X�e�[�W�ɑJ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StageSelect();
        }
    }

    // �I�����̕ύX
    private void ChangeSelection(int direction)
    {
        selectedStage = (selectedStage + direction + stageImages.Length) % stageImages.Length;
        UpdateHighlight();
    }

    // �I�������X�e�[�W�ɑJ��
    private void StageSelect()
    {
        string sceneName = ""; // �e�X�e�[�W�ɑΉ�����V�[����

        // �I�������X�e�[�W�ɉ����ăV�[������ݒ�
        switch (selectedStage)
        {
            case 0:
                sceneName = "Cource01";
                break;
            case 1:
                sceneName = "Cource01";
                break;
                // ���̃X�e�[�W������΂����ɒǉ�
        }

        // �V�[���֑J��
        SceneManager.LoadScene(sceneName);
    }

    // �n�C���C�g�̍X�V
    private void UpdateHighlight()
    {
        // �S�ẴX�e�[�W�̉摜��ʏ�̐F�ɖ߂�
        foreach (var image in stageImages)
        {
            image.color = Color.white;
        }

        // �I�𒆂̃X�e�[�W�̉摜���n�C���C�g
        stageImages[selectedStage].color = Color.yellow;
    }
}
