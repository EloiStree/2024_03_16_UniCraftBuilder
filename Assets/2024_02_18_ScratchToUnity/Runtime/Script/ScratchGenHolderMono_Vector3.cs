using UnityEngine;

public class ScratchGenHolderMono_Vector3 : ScratchHolderMono_GenericSerializedValue<Vector3>
{
    public override void ImplementGetCustomToStingValue(out bool hasCustom, out string value)
    {
        hasCustom = false;
        value = null;
    }
}
