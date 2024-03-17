public class ScratchConditionMono_ValueInRange : AbstractScratchMono_ConditionHolder
{
    public AbstractScratchMono_VariableHolderAsString m_value;
    public double m_minValue =0.5f;
    public double m_maxValue =1f;
    public bool m_useEqual=true;
    public override bool IsConditionTrue()
    {
        double v = m_value.GetValueAsDouble();
        if (m_useEqual)
            return v >= m_minValue && v <= m_maxValue;
        else 
            return v > m_minValue && v < m_maxValue;

    }
}