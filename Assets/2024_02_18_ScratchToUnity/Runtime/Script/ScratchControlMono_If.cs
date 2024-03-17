using System.Collections;

public class ScratchControlMono_If : A_ScratchBlockableMono
{

    public AbstractScratchMono_ConditionHolder m_ifCondition;
    public A_ScratchBlockableMono m_onConditionTrue;
    public bool m_lastValue;
    public override IEnumerator DoTheScratchableStuff()
    {
        bool isTrue = m_ifCondition.IsConditionTrue();
            m_lastValue = isTrue;
        if (isTrue)
            yield return m_onConditionTrue.DoTheScratchableStuff();
        yield return null;
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        bool isTrue = m_ifCondition.IsConditionTrue();
        m_lastValue = isTrue;
        if (isTrue)
           m_onConditionTrue.DoTheScratchableStuffWithoutCoroutine();
    }
}

