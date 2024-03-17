using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Scratch3DNextCastMono_RaycastLine : Scratch3DMono_AbstractSearchNextCastBlocks
{
    public LayerMask m_allowMask = ~1;
    public Transform m_anchorForward;
    public float m_distance=1;

    public bool m_useDebugLogDraw;


    public float m_debugTimeDraw=0.5f;
    public List<Scratch3DRootTagMono> m_toIgnore;

    private void Reset()
    {
        m_anchorForward = this.transform;
        Scratch3DMonoStaticUtility.SearchForScratch3DMonoRoot(this.gameObject, out bool _, out var root);
        if (root != null) {
            m_toIgnore.Add(root);
        }
    }

    public override void DebugDraw(float secondToDraw, Color dominantColor)
    {
        Debug.DrawLine(m_anchorForward.position, m_anchorForward.position + m_anchorForward.forward * m_distance, dominantColor, m_debugTimeDraw);
    }

    public override void GetNextBlocks(out IEnumerable<Scratch3DRootTagMono> blockRoots)
    {

        blockRoots = null;

        RaycastHit[] hits = Physics.RaycastAll(m_anchorForward.position, m_anchorForward.forward, m_distance, m_allowMask);
        if (m_useDebugLogDraw)
            Debug.DrawLine(m_anchorForward.position, m_anchorForward.position + m_anchorForward.forward * m_distance, hits.Length > 0 ? Color.green : Color.red, m_debugTimeDraw);
        if (m_useDebugLogDraw) { 
            foreach (var item in hits)
            {
                Debug.DrawLine(item.point, item.point+ Vector3.up*0.02f, Color.yellow, m_debugTimeDraw);
            }
        }


        Scratch3DMonoStaticUtility.SearchForWithOrder(
            m_anchorForward.position,
            out bool found,
            out IEnumerable <Scratch3DMono_CastableColliderEntry> castable,
            hits);
        if (m_useDebugLogDraw  && found)
            Debug.DrawLine(m_anchorForward.position+Vector3.up*.01f, m_anchorForward.position + Vector3.up * .01f + m_anchorForward.forward * m_distance, hits.Length > 0 ? Color.cyan : Color.red, m_debugTimeDraw);

        if (!found) {
            castable = null;
            return;
        }

        List<Scratch3DRootTagMono> rootlist = new List<Scratch3DRootTagMono>();
        foreach (var cast in castable)
        {
            Scratch3DMonoStaticUtility.SearchForScratch3DMonoRoot(cast.gameObject, out bool foundroot, out Scratch3DRootTagMono root);
            if (foundroot && !m_toIgnore.Contains(root))
                rootlist.Add(root);
        }
        blockRoots = rootlist;
    }
}

public class Scratch3DMonoStaticUtility {

    public static void SearchForScratch3DMonoExecutor(GameObject target, out bool found , out A_ScratchBlockableMono executable)
    {
        found = false;
        executable = null;
        SearchForScratch3DMonoRoot(target, out bool foundLocal, out Scratch3DRootTagMono root);
        if (foundLocal && root != null)
        {
            SearchForScratch3DMonoExecutor(root, out found, out Scratch3DMono_AbstractBlocksExecutionManager manager);
            executable = manager;
        }
    }

    public static void SearchForScratch3DMonoRoot(GameObject from, out bool found, out Scratch3DRootTagMono root)
    {
        root = null;
        found = false;
        List<Scratch3DRootTagMono> list = new List<Scratch3DRootTagMono>();
        list.AddRange(from.GetComponents<Scratch3DRootTagMono>());
        list.AddRange(from.GetComponentsInParent<Scratch3DRootTagMono>());
        if (list.Count == 0) { 
            return;
        }
        if (list.Count == 1) {
            found = true; root = list[0]; 
            return; 
        }
        list = list.Distinct().ToList();
        if (list.Count > 1)
        {
            Debug.LogWarning("You should never have several Root on a block", from);
            found = true; root = list[0]; 
            return;
        }
        else if (list.Count == 1)
        {
            found = true; root = list[0]; 
            return;
        }

    }


    public static void SearchForScratch3DMonoExecutor(Scratch3DRootTagMono root, out bool found, out Scratch3DMono_AbstractBlocksExecutionManager executor)
    {
        executor = null;
        found = false;
        List<Scratch3DMono_AbstractBlocksExecutionManager> list = new List<Scratch3DMono_AbstractBlocksExecutionManager>();
        list.AddRange(root.GetComponents<Scratch3DMono_AbstractBlocksExecutionManager>());
        list.AddRange(root.GetComponentsInChildren<Scratch3DMono_AbstractBlocksExecutionManager>());
        if (list.Count == 0)
        {
            return;
        }
        if (list.Count == 1)
        {
            found = true; executor = list[0];
            return;
        }
        list = list.Distinct().ToList();
        if (list.Count > 1)
        {
            Debug.LogWarning("You should never have several Executor manager on a block", root);
            found = true; executor = list[0];
            return;
        }
        else if (list.Count == 1)
        {
            found = true; executor = list[0];
            return;
        }
    }

    public static void SearchForWithNoOrder(out bool found, out IEnumerable<Scratch3DMono_CastableColliderEntry> castable, params RaycastHit[] hits)
    {
        List<Scratch3DMono_CastableColliderEntry> result = new List<Scratch3DMono_CastableColliderEntry>();

        foreach (var item in hits)
        {
            result.Add(item.collider.GetComponent<Scratch3DMono_CastableColliderEntry>());
        }
        castable = result.Distinct().Where(k => k != null);
        found = castable.Count() > 0;
    }
    public static void SearchForWithOrder(Vector3 startPoint, out bool found, out IEnumerable<Scratch3DMono_CastableColliderEntry> castable, params RaycastHit[] hits)
    {
        List<Scratch3DMono_CastableColliderEntry> result = new List<Scratch3DMono_CastableColliderEntry>();

        
        foreach (var item in hits.OrderBy(k => Vector3.Distance(startPoint, k.point)))
        {
            result.Add(item.collider.GetComponent<Scratch3DMono_CastableColliderEntry>());
        }
        castable = result.Distinct().Where(k => k != null);
        found = castable.Count() > 0;
    }
}




public class Scratch3DMono_3DHolderExitTag : MonoBehaviour
{ }


//public class Scratch3DMono_3DStackHolderWithDebug : A_ScratchBlockableMono
//{
//    public A_ScratchBlockableMono m_startBlockableOnAction;
//    public UnityEvent m_onBlockLogicStartRunning;
//    public UnityEvent m_onBlockLogicEndRunning;

//    public bool m_notifyNextUpdateStartRunning;
//    public bool m_notifyNextUpdateStopRunning;

//    private void Update()
//    {
//        if (m_notifyNextUpdateStartRunning)
//        {
//            m_notifyNextUpdateStartRunning = false;
//            m_onBlockLogicStartRunning.Invoke();
//        }
//        if (m_notifyNextUpdateStopRunning)
//        {
//            m_notifyNextUpdateStopRunning = false;
//            m_onBlockLogicEndRunning.Invoke();
//        }
//    }

//    public override IEnumerator DoTheScratchableStuff()
//    {
//        m_notifyNextUpdateStartRunning = true;
//        yield return m_startBlockableOnAction.DoTheScratchableStuff();
//        m_notifyNextUpdateStopRunning = false;

//    }
//}

//public class Scratch3DNextCastMono_CastableBlockStartEnd : A_ScratchBlockableMono
//{
//    public A_ScratchBlockableMono m_stackStart;
//    public Scratch3DMono_AbstractNextCastBlocks m_

//    public override IEnumerator DoTheScratchableStuff()
//    {
//        throw new System.NotImplementedException();
//    }
//}
