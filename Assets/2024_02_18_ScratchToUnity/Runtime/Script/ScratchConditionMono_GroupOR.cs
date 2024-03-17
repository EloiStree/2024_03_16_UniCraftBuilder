
public class ScratchConditionMono_GroupOR: AbstractScratchMono_ConditionHolder
{

    public AbstractScratchMono_ConditionHolder[] m_conditionsZeroToLenght;

    public override bool IsConditionTrue()
    {
        foreach (var item in m_conditionsZeroToLenght)
        {
            if (item == null)
                return false;
            if (item.IsConditionTrue())
                return true;
        }
        return false;
    }
}