using UnityEngine;





[System.Serializable]
public class ScratchInputGroup_Vector3 
{
    public Vector3 m_manualVector3;
    public AbstractScratchMono_VariableHolderAsString m_givenX;
    public AbstractScratchMono_VariableHolderAsString m_givenY;
    public AbstractScratchMono_VariableHolderAsString m_givenZ;

    public Vector3 GetVector3()
    {
        Vector3 value = m_manualVector3;
        if (m_givenX)
            value.x = m_givenX.GetValueAsFloat();
        if (m_givenY)
            value.y = m_givenY.GetValueAsFloat();
        if (m_givenZ)
            value.z = m_givenZ.GetValueAsFloat();
        return value;
    }
}
