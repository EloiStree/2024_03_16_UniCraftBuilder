
/// <summary>
/// A variable is store in the current 3D object(need root Can only be one type per root)
/// </summary>
public class Scratch3DTypeMono_SoloStringVariableHolder : Scratch3DTypeMono_SoloVariableHolder<AbstractScratchMono_VariableHolderAsString>
{
    public AbstractScratchMono_VariableHolderAsString m_variableHolder;
    public override AbstractScratchMono_VariableHolderAsString GetVariable()
    {return m_variableHolder;}
}
