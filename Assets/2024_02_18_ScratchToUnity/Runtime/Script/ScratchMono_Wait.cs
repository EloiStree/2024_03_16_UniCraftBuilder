using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScratchMono_Wait : A_ScratchBlockableMono
{
    public float m_waitInSeconds;

    public override IEnumerator DoTheScratchableStuff()
    {
        if (Application.isPlaying)
        {
            if (m_waitInSeconds <= 0)
                yield return null;
            yield return new WaitForSeconds(m_waitInSeconds);
        }
        else {
            m_startWaiting = DateTime.Now;
            yield return (DateTime.Now-m_startWaiting).TotalSeconds<m_waitInSeconds;
        }
    }

    private DateTime m_startWaiting;

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
    }
}


