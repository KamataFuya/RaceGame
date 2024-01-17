using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PoseManager : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public Text[] menuTexts;
    private int selectedTextIndex = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1; // �Q�[����ʏ�̑��x�ōĐ�

        // �����I���e�L�X�g��ݒ�
        SelectText(selectedTextIndex);

        // �e�L�X�g���\���ɂ���
        HideTextList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        // ���L�[�Ńe�L�X�g�I��
        if (pauseMenuUI.activeSelf)
        {
            

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedTextIndex = (selectedTextIndex - 1 + menuTexts.Length) % menuTexts.Length;
                SelectText(selectedTextIndex);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedTextIndex = (selectedTextIndex + 1) % menuTexts.Length;
                SelectText(selectedTextIndex);
            }

            // �X�y�[�X�L�[�Ńe�L�X�g��I��
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteSelectedOption();
            }          
        }
    }

    public void TogglePauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        ShowTextList();
        Time.timeScale = pauseMenuUI.activeSelf ? 0 : 1;
        Cursor.lockState = pauseMenuUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = pauseMenuUI.activeSelf;

        // �|�[�Y���j���[���J���ꂽ�Ƃ��A�ŏ��̃e�L�X�g��I������
        if (pauseMenuUI.activeSelf)
        {
            selectedTextIndex = 0;
        }
    }

    private void SelectText(int index)
    {
        // �I�𒆂̃e�L�X�g�Ƀt�H�[�J�X�𓖂Ă�
        for (int i = 0; i < menuTexts.Length; i++)
        {
            menuTexts[i].color = (i == index) ? Color.cyan : Color.white;
        }
    }

    private void ExecuteSelectedOption()
    {
        // �I�����ꂽ�e�L�X�g�ɂ���ď����𕪊�
        switch (selectedTextIndex)
        {
            case 0:
                Resume();

                break;
            case 1:
                Restart();
                break;
            case 2:
                QuitToTitle();
                break;
        }
    }

    private void Resume()
    {
        TogglePauseMenu();
    }

    private void Restart()
    {
        // ���݂̃V�[�����ēǂݍ���
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    private void QuitToTitle()
    {
        // �^�C�g����ʂ̃V�[�����ɕύX
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        Time.timeScale = 1;
    }

    void HideTextList()
    {
        foreach (Text textElement in menuTexts)
        {
            textElement.gameObject.SetActive(false);
        }
    }

    void ShowTextList()
    {
        foreach (Text textElement in menuTexts)
        {
            textElement.gameObject.SetActive(!textElement.gameObject.activeSelf);
        }
    }

}
