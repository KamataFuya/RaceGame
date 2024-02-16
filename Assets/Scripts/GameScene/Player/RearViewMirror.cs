using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RearViewMirror : MonoBehaviour
{
    public Camera rearCamera; // ����̃J����
    public RawImage mirrorImage; // �~���[UI��RawImage

    private RenderTexture renderTexture; // �J�����f�����e�N�X�`���Ƃ��Ċi�[����ϐ�

    void Start()
    {
        // �J�����f�����e�N�X�`���Ƃ��Ċi�[����RenderTexture���쐬
        renderTexture = new RenderTexture(Screen.width / 2, Screen.height / 2, 0);
        rearCamera.targetTexture = renderTexture;

        // �~���[UI��RawImage�Ƀe�N�X�`����ݒ�
        mirrorImage.texture = renderTexture;
    }

    void Update()
    {
        // �~���[UI�̕\�����X�V
        mirrorImage.enabled = IsInRearViewMode();
    }

    // ������m�F���郂�[�h���ǂ����𔻒肷�郁�\�b�h
    bool IsInRearViewMode()
    {
        // ��Ƃ��āA�v���[���[��������m�F���邽�߂̃L�[�������Ă��邩�ǂ����Ŕ��肷��
        return true; // ���̕����͕K�v�ɉ����ĕύX���Ă�������
    }
}
