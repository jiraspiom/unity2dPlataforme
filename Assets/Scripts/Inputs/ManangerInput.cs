using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManangerInput : MonoBehaviour
{
    private float inputX;

    public event Action OnButtonEvent;
    private bool isDown;

    public bool IsDown()
    {
        return isDown;
    }

    /// <summary>
    /// metodo usado na configuracao do input do player
    /// </summary>
    /// <param name="value"></param>
    public void SetInputMove(InputAction.CallbackContext value)
    {
        inputX = value.ReadValue<Vector2>().x;
    }
    public float GetInputX()
    {
        return inputX;
    }

    public void OnButtonDown()
    {
        OnButtonEvent?.Invoke();
    }

    /// <summary>
    /// metodo usado na configuracao do input do player
    /// </summary>
    /// <param name="value"></param>
    public void GetButtonDown(InputAction.CallbackContext value)
    {
        // essa propriedade verifica se o botao foi presionado
        if (value.performed)
        {
            OnButtonDown();
        }
        isDown = value.performed;
    }
}
