using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchMono_VariableHolder : AbstractScratchMono_VariableHolderAsString
{
    public string m_variable;

    public void SetValue(string value) { m_variable = value; }
    public void SetValue(int value) { m_variable = value.ToString(); }
    public void SetValue(float value) { m_variable = value.ToString(); }
    public void SetValue(double value) { m_variable = value.ToString(); }
    public void SetValue(bool value) { m_variable = value.ToString(); }
    public override string GetVariableAsString()
    {
        return m_variable;
    }
}


public abstract class AbstractScratchMono_VariableHolderAsString : MonoBehaviour {
    public abstract string GetVariableAsString();

    public float GetValueAsFloat(float defaulValueIfError = 0)
    {

        if (float.TryParse(GetVariableAsString(), out float result))
        {
            return result;
        }
        else
        {
            return defaulValueIfError;
        }
    }
    public float GetValueAsInt(int defaulValueIfError = 0)
    {

        if (int.TryParse(GetVariableAsString(), out int result))
        {
            return result;
        }
        else
        {
            return defaulValueIfError;
        }
    }
    public double GetValueAsDouble(double defaulValueIfError = 0)
    {

        if (double.TryParse(GetVariableAsString(), out double result))
        {
            return result;
        }
        else
        {
            return defaulValueIfError;
        }
    }
}