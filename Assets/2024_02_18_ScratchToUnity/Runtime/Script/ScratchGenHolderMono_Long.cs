public class ScratchGenHolderMono_Long : ScratchHolderMono_GenericSerializedValue<long>
{
    public override void ImplementGetCustomToStingValue(out bool hasCustom, out string value)
    {
        hasCustom = false;
        value = null;
    }
}
