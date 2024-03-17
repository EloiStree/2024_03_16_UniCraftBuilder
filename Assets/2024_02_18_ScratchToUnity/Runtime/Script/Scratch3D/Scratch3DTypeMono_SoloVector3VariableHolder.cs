public class Scratch3DTypeMono_SoloVector3VariableHolder : Scratch3DTypeMono_SoloVariableHolder<AbstractScratchMono_VariableHolderVector3>
{
    public AbstractScratchMono_VariableHolderVector3 m_variableHolder;
    public override AbstractScratchMono_VariableHolderVector3 GetVariable() 
    { return m_variableHolder; }
}
