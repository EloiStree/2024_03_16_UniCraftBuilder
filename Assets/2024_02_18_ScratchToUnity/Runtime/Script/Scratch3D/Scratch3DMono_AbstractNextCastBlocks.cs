using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Know how to fetch the next coming blocks to continuing the stream
/// </summary>
public abstract class Scratch3DMono_AbstractSearchNextCastBlocks : MonoBehaviour
{
    public abstract void GetNextBlocks(out IEnumerable< Scratch3DRootTagMono> blockRoots);
    public abstract void DebugDraw(float secondToDraw, Color dominantColor);
    [ContextMenu("Quick Debug Draw 1 Seconds")]
    public void QuickDebugDraw_1Seconds() => DebugDraw(1, Color.magenta);
    [ContextMenu("Quick Debug Draw 10 Seconds")]
    public void QuickDebugDraw_10Seconds() => DebugDraw(10, Color.magenta);
}


public class ScratchDebugDrawUtility {

    public static void DrawWorldCross(Vector3 point, Color color, float duration, float height = .01f)
    {
        Debug.DrawLine(point - Vector3.up * height, point + Vector3.up * height, color, duration);
        Debug.DrawLine(point - Vector3.right * height, point + Vector3.right * height, color, duration);
        Debug.DrawLine(point - Vector3.forward * height, point + Vector3.forward * height, color, duration);
    }
    public static void DrawWorldUpLine(Vector3 point, Color color, float duration, float height= .01f)
    {
        Debug.DrawLine(point, point + Vector3.up * height, color, duration);
    }
}

/// <summary>
/// How to manage the next block ? One, Multiple, Coditional , Broadcast ,Order ?
/// </summary>
public abstract class Scratch3DMono_AbstracthSearchNextCastBlocksManager: Scratch3DMono_AbstractSearchNextCastBlocks { }


/// <summary>
/// Know how to contine the propagation of the stream.
/// </summary>

public abstract class Scratch3DMono_AbstractBlocksExecutionManager : A_ScratchBlockableMono
{

}


/// <summary>
/// Define what is in the 3D object (can only be on per object and need root)
/// </summary>
public class Scratch3DTypeMono : MonoBehaviour {

    public void GetRoot(out Scratch3DRootTagMono rootTag) {
        Scratch3DMonoStaticUtility.SearchForScratch3DMonoRoot(this.gameObject, out bool _, out rootTag);
    }
}

/// <summary>
/// A variable is store in the current 3D object(need root Can only be one type per root)
/// </summary>
public abstract class Scratch3DTypeMono_SoloVariableHolder<T> : Scratch3DTypeMono
{
    public abstract T GetVariable();
}

public class Scratch3DTypeMono_ListVariableHolder : Scratch3DTypeMono { }
