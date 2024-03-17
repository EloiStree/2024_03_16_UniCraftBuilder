using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractScratchMono_ConditionHolder : MonoBehaviour
{
    public abstract bool IsConditionTrue();
    public bool IsConditionFalse() { return !IsConditionTrue(); }
}



public class ScratchConditionMono_XOR: AbstractScratchMono_ConditionHolder
{

    public AbstractScratchMono_ConditionHolder m_leftCondition;
    public AbstractScratchMono_ConditionHolder m_rightCondition;

    public override bool IsConditionTrue()
    {
        return m_leftCondition.IsConditionTrue() ^ m_rightCondition.IsConditionTrue();
    }
}