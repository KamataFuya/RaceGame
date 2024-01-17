using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelect : MonoBehaviour
{
    public Text[] modeTexts; // ���[�h�̃e�L�X�g
    private int selectedMode = 0; // �I�𒆂̃��[�h

    private void Update()
    {
        // ���L�[�̏㉺�őI����ύX
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeSelection(1);
        }

        // �X�y�[�X�L�[�őI���������[�h�ɑJ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectMode();
        }
    }

    // �I�����̕ύX
    private void ChangeSelection(int direction)
    {
        selectedMode = (selectedMode + direction + modeTexts.Length) % modeTexts.Length;
        UpdateHighlight();
    }

    // �I���������[�h�ɑJ��
    private void SelectMode()
    {
        string sceneName = ""; // �e���[�h�ɑΉ�����V�[����

        // �I���������[�h�ɉ����ăV�[������ݒ�
        switch (selectedMode)
        {
            case 0:
                sceneName = "StageSelect";
                break;
            case 1:
                sceneName = "StageSelect";
                break;
                // ���̃��[�h������΂����ɒǉ�
        }

        // �V�[���֑J��
        SceneManager.LoadScene(sceneName);
    }

    // �n�C���C�g�̍X�V
    private void UpdateHighlight()
    {
        // �S�Ẵ��[�h�̃e�L�X�g��ʏ�̐F�ɖ߂�
        foreach (var text in modeTexts)
        {
            text.color = Color.black;
        }

        // �I�𒆂̃��[�h�̃e�L�X�g���n�C���C�g
        modeTexts[selectedMode].color = Color.yellow;
    }
}
