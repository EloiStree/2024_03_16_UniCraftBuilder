using System.Collections;

public class ScratchControlMono_RepeatUntil : A_ScratchBlockableMono
{

    public AbstractScratchMono_ConditionHolder m_ifCondition;
    public A_ScratchBlockableMono m_whatToRepeat;
    public bool m_useFrameWaitSecurity = true;

    public override IEnumerator DoTheScratchableStuff()
    {
        while (m_ifCondition.IsConditionTrue()) { 
            yield return m_whatToRepeat.DoTheScratchableStuff();
            if (m_useFrameWaitSecurity)
                yield return new UnityEngine.WaitForEndOfFrame();
        }
        yield return null;
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        NotifyTheActionWouldBeDangerousToExecuteOutsideOfPlayMode();
    }
}
