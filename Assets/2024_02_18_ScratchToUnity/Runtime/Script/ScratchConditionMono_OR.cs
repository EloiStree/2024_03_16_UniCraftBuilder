public class ScratchConditionMono_OR: AbstractScratchMono_ConditionHolder
{

    public AbstractScratchMono_ConditionHolder m_leftCondition;
    public AbstractScratchMono_ConditionHolder m_rightCondition;

    public override bool IsConditionTrue()
    {
        return m_leftCondition.IsConditionTrue() || m_rightCondition.IsConditionTrue();
    }
}