using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScratchMono_PostItHolder : AbstractScratchMono_VariableHolderAsString
{
    [TextArea(3,10)]
    public string m_postItText;
    public string m_postItUniqueId;

    public void SetValue(string value) { m_postItText = value; }
    public void SetValue(int value) { m_postItText = value.ToString(); }
    public void SetValue(float value) { m_postItText = value.ToString(); }
    public void SetValue(double value) { m_postItText = value.ToString(); }
    public void SetValue(bool value) { m_postItText = value.ToString(); }
    public override string GetVariableAsString()
    {
        return m_postItText;
    }


    public UnityEvent<string> m_onTextChanged;
    public void OnValidate()
    {
        if(m_onTextChanged!=null)
        m_onTextChanged.Invoke(m_postItText);
    }
    public void Reset()
    {
        RefreshGuidID();
    }

    [ContextMenu("Refresh ID")]
    private void RefreshGuidID()
    {
        m_postItUniqueId = Guid.NewGuid().ToString();
    }


}
