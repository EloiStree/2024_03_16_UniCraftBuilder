using System.Collections;
using UnityEngine;

public class ScratchControlMono_WaitUntil : A_ScratchBlockableMono
{
    public AbstractScratchMono_ConditionHolder m_ifCondition;
 
    public override IEnumerator DoTheScratchableStuff()
    {
        if (!Application.isPlaying)
            yield return null;
        while (m_ifCondition.IsConditionFalse())
           yield return new UnityEngine.WaitForEndOfFrame();
        yield return null;
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        NotifyTheActionWouldBeDangerousToExecuteOutsideOfPlayMode();
    }
}
