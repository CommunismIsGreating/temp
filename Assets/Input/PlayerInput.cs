using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player inputs;
    /// <summary>
    /// ��ű���
    /// </summary>
    public bool Jump => inputs.GamePlay.Jump.WasPressedThisFrame();//��ʾ�Ƿ�����һ֡����jump�󶨰���
    public bool stopJump => inputs.GamePlay.Jump.WasReleasedThisFrame();//��ʾ�Ƿ�����һ֡�ɿ�jump�󶨰���


    public bool onQ => inputs.GamePlay.Q_exchangeSelf.WasPressedThisFrame();//��ʾ�Ƿ�����һ֡����Q�󶨰���
    public bool deQ => inputs.GamePlay.Q_exchangeSelf.WasReleasedThisFrame();
    public bool onE => inputs.GamePlay.E_exchange.WasPressedThisFrame();//��ʾ�Ƿ�����һ֡����E�󶨰���
    public bool deE => inputs.GamePlay.E_exchange.WasReleasedThisFrame();
    public bool onR => inputs.GamePlay.R_Throw.WasPressedThisFrame();//��ʾ�Ƿ�����һ֡����R�󶨰���
    public bool deR => inputs.GamePlay.R_Throw.WasReleasedThisFrame();

    public bool Move => axes.x != 0f;
    public Vector2 axes => inputs.GamePlay.Move.ReadValue<Vector2>();//��ȡ�������

    private void Awake()
    {
        inputs = new Player();
    }
    public void EnableGamePlay()
    {
        inputs.GamePlay.Enable();
        Cursor.lockState = CursorLockMode.None;
    }
    public void DisableGamePlay()
    {
        inputs.GamePlay.Disable();
    }
}
