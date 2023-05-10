using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputSystem : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<float> OnEnableHorizontalInput;
    [SerializeField]
    private UnityEvent OnJump;

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        OnEnableHorizontalInput.Invoke(horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump.Invoke();
        }
    }
}
