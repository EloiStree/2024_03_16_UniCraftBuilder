using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScratchMono_WhenPressKeyboard : MonoBehaviour
{

    public KeyCode m_keyObserved;
    public UnityEvent m_onKeyPressed;

    public UnityEvent m_onKeyReleased;

    void Update()
    {

        if (Input.GetKeyDown(m_keyObserved))
            m_onKeyPressed.Invoke();
        if (Input.GetKeyUp(m_keyObserved))
            m_onKeyReleased.Invoke();
    }
}
