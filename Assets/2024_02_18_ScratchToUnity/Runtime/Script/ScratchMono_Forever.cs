using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchMono_Forever : A_ScratchBlockableMono
{
    public bool m_useEndOfFrameSecurity = true;
    public A_ScratchBlockableMono m_blockableToRunForever;

    public Coroutine m_running;

    [ContextMenu("Launch Forever loop")]
    public void LaunchForeverCoroutine() {
        if (m_running != null)
            StopCoroutine(m_running);
        m_running= StartCoroutine(DoTheScratchableStuff());
        
    }       

    public override IEnumerator DoTheScratchableStuff()
    {
        if (!Application.isPlaying)
            yield return null;
        while (true) {
            if (m_blockableToRunForever == this) {
                Debug.LogError("Never put in forever himself");
                throw new System.Exception("Never put in forever himself");
                
            }
            if(m_blockableToRunForever)
                yield return m_blockableToRunForever.DoTheScratchableStuff();
            
            if (m_useEndOfFrameSecurity)
                yield return new WaitForEndOfFrame();
        }
    }

    private void Reset()
    {
        foreach (var item in GetComponents<A_ScratchBlockableMono>())
        {
            if (item != this)
            {
                m_blockableToRunForever = item;
                return;
            }

        }
        foreach (var item in GetComponentsInChildren<A_ScratchBlockableMono>())
        {
            if (item != this)
            {
                m_blockableToRunForever = item;
                return;
            }

        }
     
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        NotifyTheActionWouldBeDangerousToExecuteOutsideOfPlayMode();
    }
}
