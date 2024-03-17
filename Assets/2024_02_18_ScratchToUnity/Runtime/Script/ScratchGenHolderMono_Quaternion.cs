using UnityEngine;

public class ScratchGenHolderMono_Quaternion : ScratchHolderMono_GenericSerializedValue<Quaternion>
{
    public override void ImplementGetCustomToStingValue(out bool hasCustom, out string value)
    {
        hasCustom = false;
        value = null;
    }
}
