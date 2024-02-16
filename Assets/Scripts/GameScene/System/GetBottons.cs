using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using UnityEngine.UI;
using XInputDotNetPure;

//���͏����Ǘ��N���X
public class GetBottons : MonoBehaviour 
{
    // �C���X�^���X�ւ̃A�N�Z�X���ȒP�ɂ��邽�߂̃V���O���g���p�^�[��
    public static GetBottons Instance { get; private set; }

    // �R���g���[���[�̃v���C���[�ԍ�
    public PlayerIndex playerIndex = PlayerIndex.One;

    // �R���g���[���[�̓��͏�Ԃ�ێ�����ϐ�
    private GamePadState state;
    private GamePadState prevState;

    // ������
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���t���[���̍X�V
    private void Update()
    {
        // �R���g���[���[�̏�Ԃ��X�V
        prevState = state;
        state = GamePad.GetState(playerIndex);
    }

    // �{�^���������ꂽ���ǂ������`�F�b�N���郁�\�b�h
    public bool GetButtonDown(Button button)
    {
        switch (button)
        {
            case Button.A:
                return state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released;
            case Button.B:
                return state.Buttons.B == ButtonState.Pressed && prevState.Buttons.B == ButtonState.Released;
            case Button.X:
                return state.Buttons.X == ButtonState.Pressed && prevState.Buttons.X == ButtonState.Released;
            case Button.Y:
                return state.Buttons.Y == ButtonState.Pressed && prevState.Buttons.Y == ButtonState.Released;
            case Button.UP:
                return state.DPad.Up == ButtonState.Pressed && prevState.DPad.Up == ButtonState.Released;
            case Button.DOWN:
                return state.DPad.Down == ButtonState.Pressed && prevState.DPad.Down == ButtonState.Released;
            case Button.RIGHT:
                return state.DPad.Right == ButtonState.Pressed && prevState.DPad.Right == ButtonState.Released;
            case Button.LEFT:
                return state.DPad.Left == ButtonState.Pressed && prevState.DPad.Left == ButtonState.Released;
            case Button.R:
                return state.Buttons.RightShoulder == ButtonState.Pressed && prevState.Buttons.RightShoulder == ButtonState.Released;
            case Button.L:
                return state.Buttons.LeftShoulder == ButtonState.Pressed && prevState.Buttons.LeftShoulder == ButtonState.Released;
            case Button.Start:
                return state.Buttons.Start == ButtonState.Pressed && prevState.Buttons.Start == ButtonState.Released;
            case Button.Back:
                return state.Buttons.Back == ButtonState.Pressed && prevState.Buttons.Back == ButtonState.Released;
            case Button.Guide:
                return state.Buttons.Guide == ButtonState.Pressed && prevState.Buttons.Guide == ButtonState.Released;
            // ���̃{�^�������l�ɒǉ�
            default:
                return false;
        }
    }

    // �A�i���O�X�e�B�b�N�̒l���擾���郁�\�b�h
    public Vector2 GetLeftStick()
    {
        return new Vector2(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
    }

    public Vector2 GetRightStick()
    {
        return new Vector2(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
    }

    // ���E�̃g���K�[�L�[�̒l���擾���郁�\�b�h
    public float GetLeftTrigger()
    {
        return state.Triggers.Left;
    }

    public float GetRightTrigger()
    {
        return state.Triggers.Right;
    }

    // ���E�̃g���K�[�L�[���������u�Ԃ��擾
    public bool GetLeftTriggerDown(float threshold)
    {
        return state.Triggers.Left >= threshold && prevState.Triggers.Left < threshold;
    }

    public bool GetRightTriggerDown(float threshold)
    {
        return state.Triggers.Right >= threshold && prevState.Triggers.Right < threshold;
    }

    // ���E�̃g���K�[�L�[�̗������u�Ԃ��擾
    public bool GetLeftTriggerUp(float threshold)
    {
        return state.Triggers.Left < threshold && prevState.Triggers.Left >= threshold;
    }

    public bool GetRightTriggerUp(float threshold)
    {
        return state.Triggers.Right < threshold && prevState.Triggers.Right >= threshold;
    }

    /// <summary>
    /// �R���g���[���[���w�肵�������ƒ����ŐU��������
    /// </summary>
    /// <param name="intensity">�U�����鋭��(0.0f~1.0f)</param>
    /// <param name="duration">�U��������b��</param>
    /// <returns></returns>
    IEnumerator StartVibration(float intensity, float duration)
    {
        // �R���g���[���[���w�肵�������ƒ����ŐU��������
        GamePad.SetVibration(playerIndex, intensity, intensity);

        // �U���̒��������ҋ@����
        yield return new WaitForSeconds(duration);

        // �U�����~����
        GamePad.SetVibration(playerIndex, 0, 0);
    }

    // �{�^���̎�ނ��`����񋓌^
    public enum Button
    {
        A,
        B,
        X,
        Y,
        UP,
        DOWN,
        RIGHT,
        LEFT,
        R,
        L,
        Start,
        Back,
        Guide, //XBOX�{�^��
    }

}
