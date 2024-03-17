public class ScratchConditionMono_TRUEORFALSE : AbstractScratchMono_ConditionHolder
{
    public bool m_isTrue;

    public override bool IsConditionTrue()
    {
        return m_isTrue;
    }
}
