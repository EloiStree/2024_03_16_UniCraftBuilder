using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchScriptInputMono_RandomFloat : AbstractScratchMono_VariableHolderAsString
{

    public float m_minimValue=0;
    public float m_maximuValue=100;

    public override string GetVariableAsString()
    {
        return ""+UnityEngine.Random.Range(-m_minimValue, m_maximuValue);
    }


}
