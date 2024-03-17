using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScratchHelperMono_LoopIntervalAction : MonoBehaviour
{

    public float m_timeBetweenAction = 6;

    public UnityEvent m_onActoinCall;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(m_timeBetweenAction);
            m_onActoinCall.Invoke();
        }
    }

}
