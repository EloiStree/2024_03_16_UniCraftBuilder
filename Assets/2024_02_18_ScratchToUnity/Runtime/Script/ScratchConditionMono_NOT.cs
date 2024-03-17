public class ScratchConditionMono_NOT: AbstractScratchMono_ConditionHolder
{

    public AbstractScratchMono_ConditionHolder m_condition;

    public override bool IsConditionTrue()
    {
        return !m_condition.IsConditionTrue();
    }
}
