using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchMono_CreateCloneOf : A_ScratchBlockableMono
{

    public GameObject m_objectToClone;
    public List<GameObject> m_created = new List<GameObject>();
    public Transform m_cloneParent;
    public override IEnumerator DoTheScratchableStuff()
    {
        CreateClone();
        yield return null;
    }

    [ContextMenu("Create  clone")]
    public  void CreateClone()
    {
        GameObject created = GameObject.Instantiate(m_objectToClone);
        m_created.Add(created);
        created.transform.parent = m_cloneParent;
        List<ScratchMono_StartAsClone> list = new List<ScratchMono_StartAsClone>();
        created.GetComponentsInChildren<ScratchMono_StartAsClone>(true, list);
        created.GetComponents<ScratchMono_StartAsClone>( list);
        foreach (var item in list)
        {
            if (item != null)
                item.NotifyObjectIsClone();
        }
    }
    [ContextMenu("Kill all clone")]
    public  void KillAllCreatedClone()
    {
        foreach (var toKill in m_created)
        {
            if (toKill != null) {

                List<ScratchMono_DeleteThisClone> list = new List<ScratchMono_DeleteThisClone>();
                toKill.GetComponentsInChildren<ScratchMono_DeleteThisClone>(true, list);
                toKill.GetComponents<ScratchMono_DeleteThisClone>(list);
                foreach (var deleter in list)
                {
                    if (deleter != null)
                        deleter.DeleteThisClone();
                }
            }

        }
        m_created.Clear();


    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        CreateClone();
    }
}
