public class ScratchConditionMono_GroupAND : AbstractScratchMono_ConditionHolder
{

    public AbstractScratchMono_ConditionHolder [] m_conditionsZeroToLenght;

    public override bool IsConditionTrue()
    {
        bool isTrue = true;
        foreach (var item in m_conditionsZeroToLenght)
        {
            if (m_conditionsZeroToLenght == null)
                return false;
            isTrue = isTrue && item.IsConditionTrue();
            if (!isTrue)
                return false;

        }
        return true;
    }
}
