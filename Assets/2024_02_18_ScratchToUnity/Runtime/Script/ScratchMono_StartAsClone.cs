using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScratchMono_StartAsClone : MonoBehaviour
{

    public UnityEvent m_onNotifiedAsClone;
    public bool m_isObjectClone;

    private void Awake()
    {
        m_isObjectClone = false;
    }
    public void NotifyObjectIsClone() {
        if (m_isObjectClone == false) { 
            m_onNotifiedAsClone.Invoke();
            m_isObjectClone = true;
        }
    }
}
