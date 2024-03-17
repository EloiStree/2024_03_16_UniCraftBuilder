using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchConditionMono_LessOrMore : AbstractScratchMono_ConditionHolder
{
    public ScratchMono_VariableHolder m_leftPart;
    public ScratchMono_VariableHolder m_rightPart;
    public OperationType m_operationType = OperationType.AreEquals;
    public enum OperationType { AreEquals, LeftLessThatRight, LeftIsMorethatRight, LeftLessThatRightOrEquals, LeftIsMorethatRightOrEquals }
    public override bool IsConditionTrue()
    {
        if (m_operationType == OperationType.AreEquals)
        {
            return m_leftPart.GetVariableAsString() == m_rightPart.GetVariableAsString();
        }
        else if (m_operationType == OperationType.LeftLessThatRight)
        {
            return m_leftPart.GetValueAsDouble() < m_rightPart.GetValueAsDouble();
        }
        else if (m_operationType == OperationType.LeftIsMorethatRight)
        {
            return m_leftPart.GetValueAsDouble() > m_rightPart.GetValueAsDouble();
        }
        else if (m_operationType == OperationType.LeftLessThatRightOrEquals)
        {
            return m_leftPart.GetValueAsDouble() <= m_rightPart.GetValueAsDouble();
        }
        else if (m_operationType == OperationType.LeftIsMorethatRightOrEquals)
        {
            return m_leftPart.GetValueAsDouble() >= m_rightPart.GetValueAsDouble();
        }
        return false;
    }

    
}
