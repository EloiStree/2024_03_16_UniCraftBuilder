using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScratchMono_DeleteThisClone : MonoBehaviour
{

    public bool m_destroyActive;
    public UnityEvent m_callCloneDestructer;
    public bool m_destroyThisObjectOnCall;


    public void SetDestroyerOn(bool isOn) {
        m_destroyActive = isOn;
    }
    [ContextMenu("Destroy Clone")]
    public void DeleteThisClone() {

        if (!m_destroyActive) return;
        m_callCloneDestructer.Invoke();
        if (m_destroyThisObjectOnCall) {
            if (this != null && this.gameObject != null) { 
               Destroy(this.gameObject);
            }
        }
    }

    public void DestroyTarget(GameObject target) {

        if (!m_destroyActive) return;
        if (target != null )
            {
                Destroy(target);
            
        }
    }

}
