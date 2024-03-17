using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public interface I_ScratchVariableHolder_Texture2D
{
    public Texture2D GetAsTexture2D();
}
public interface I_ScratchVariableHolder_String
{
    public string GetStringValue();
}
public interface I_ScratchVariableHolder_Vector3
{
    public Vector3 GetVector3();
}

public abstract class AbstractScratchHolderMono_GenericSerializedValue<T> : AbstractScratchMono_VariableHolderAsString, I_ScratchVariableHolder_GenericSerializeValue<T>
{
    public override string GetVariableAsString() {

        ImplementGetCustomToStingValue(out bool hasCustom, out string valueAsText);
        if (hasCustom)
            return valueAsText;
        GetValue(out T value);
        return value.ToString();
    }
    public abstract void ImplementGetCustomToStingValue(out bool hasCustom, out string value);

    public abstract void GetValue(out T value);

    public abstract void SetValue(T value);
}
public abstract class ScratchHolderMono_GenericSerializedValue<T> : AbstractScratchHolderMono_GenericSerializedValue<T>
{
    [SerializeField]
    public T m_value;
    public override void GetValue(out T value)
    {
        value = m_value;
    }

 

    public override void SetValue(T value)
    {
        m_value = value;
    }
}
public interface I_ScratchVariableHolder_GenericSerializeValue<T>
{
    public void GetValue( out T value);
    public void SetValue(T value);
}

public abstract class AbstractScratchMono_Texture2DImageHolder : MonoBehaviour, I_ScratchVariableHolder_Texture2D
{
    public abstract void SetWithTexture2D(Texture2D texture);
    public abstract Texture2D GetAsTexture2D();
}
public abstract class AbstractScratchMono_LoadImageFrom : A_ScratchBlockableMono
{
    public override IEnumerator DoTheScratchableStuff()
    {
        yield return LoadIfRequired();
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        LoadIfRequiredWithoutCoroutine();
    }

    public abstract IEnumerator LoadIfRequired();
    public abstract void LoadIfRequiredWithoutCoroutine();
}
