using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


public class UScratchMono_AnyIEnumeratorDisplayList : MonoBehaviour
{

    public bool m_inChildrenToo = true;
    [Header("Debug Enumarable action")]
    public List<string> m_listOfMethods= new List<string>();


    void Reset()
    {
        RefreshList();
    }
    void Start()
    {
        RefreshList();
    }

    [ContextMenu("Refresh List")]
    void RefreshList()
    {

        m_listOfMethods.Clear();
        ListMethodsInComponents(gameObject);

        m_listOfMethods = m_listOfMethods.Distinct().ToList();
    }

    void ListMethodsInComponents(GameObject gameObject)
    {

        Component[] components = m_inChildrenToo ?
            gameObject.GetComponentsInChildren<Component>() :
            gameObject.GetComponents<Component>();
        foreach (Component component in components)
        {
            Type type = component.GetType();
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (MethodInfo method in methods)
            {
                if (method.ReturnType == typeof(IEnumerator) && method.GetParameters().Length == 0)
                {
                    m_listOfMethods.Add($"Method '{method.Name}' in component '{type.Name}' attached to GameObject '{gameObject.name}'");
                }
            }
        }
    }
}

public class UScratchMono_AnyIEnumerator : A_ScratchBlockableMono
{


    [ContextMenu("Add list dipslayer")]
    public void AddListDisplayer() {
        gameObject.AddComponent<UScratchMono_AnyIEnumeratorDisplayList>();
    }

    public string m_methodeToExecuteOnAll = "DoTheStuff";

    public bool m_inChildrenToo=true;
    public bool m_useDebugLogOnExecute=false;
  

 


    public override IEnumerator DoTheScratchableStuff()
    {
        yield return ExecuteOnAllFollowing();
    }

    private IEnumerator ExecuteOnAllFollowing()
    {
        yield return ScratchExecuteMethodeUtility.ExecuteOnAllFollowing(m_methodeToExecuteOnAll, m_inChildrenToo, m_useDebugLogOnExecute, this.gameObject, this);
    }

    public override void DoTheScratchableStuffWithoutCoroutine()
    {
        IEnumerator e = ExecuteOnAllFollowing();
        /**
        Debug.Log("Te");

        while (e.MoveNext()) ;
        Debug.Log("Test");
        */
        // Code above shoudl work but don't in the waiting.
        StartCoroutine(e);
           
    }
}



public class ScratchExecuteMethodeUtility {

    public static IEnumerator ExecuteOnAllFollowing(string methodeName, bool useChildrent, bool useDebugLog, GameObject targetRoot, params MonoBehaviour [] ignore)
    {


        List<MonoBehaviour> list = new List<MonoBehaviour>();
        if (useChildrent) targetRoot.GetComponentsInChildren<MonoBehaviour>(list);
        else if (useChildrent) targetRoot.GetComponents<MonoBehaviour>(list);
        foreach (var item in ignore)
        {
            list.Remove(item);
        }
        foreach (var item in list)
        {
            yield return ExecuteFollowing(item, methodeName, useDebugLog, ignore);
        }
    }
    public static IEnumerator ExecuteFollowing(MonoBehaviour targetComponent, string methodName, bool useDebugLog, params MonoBehaviour[] ignore)
    {
        if (targetComponent == null || string.IsNullOrEmpty(methodName) )
        {
            yield return null;
        } 
        foreach (var item in ignore)
        {
            if (targetComponent == item)
                yield return null;
        }


        // Get the type of the target component
        Type type = targetComponent.GetType();

        // Get the method by its name
        MethodInfo methodInfo = type.GetMethod(methodName);

        if (methodInfo != null)
        {

            if (useDebugLog)
                Debug.Log("Running " + targetComponent.GetType(), targetComponent);
            IEnumerator enumerator = (IEnumerator)methodInfo.Invoke(targetComponent, null);
            yield return enumerator;
            if (useDebugLog)
                Debug.Log("Stop Running " + targetComponent.GetType(), targetComponent);
        }
        else
        {
            Debug.LogError("Method not found.");
        }
        yield return null;
    }
}