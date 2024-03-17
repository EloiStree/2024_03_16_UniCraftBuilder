using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchTagsMono : MonoBehaviour
{

    public ScratchTags m_tags;

    public bool ContainsOneTag(params string[] tags)
    {
       return  m_tags.ContainsOneTag(tags);
    }
}

[System.Serializable]
public class ScratchTags {
    public string[] m_tags;
    public bool ContainsOneTag(params string[] tags)
    {
        foreach (var item1 in tags)
        {
            foreach (var item2 in m_tags)
            {
                if (item1 == item2) return true;
            }
        }
        return false;
    }
    public bool ContainsOneTag( string[] tags, bool useIgnoreCase, bool useTrim)
    {
        foreach (var item1 in tags)
        {
            foreach (var item2 in m_tags)
            {
                if(useIgnoreCase && useTrim)
                {
                    if (item1.ToLower().Trim() == item2.ToLower().Trim()) return true;
                }
                else if(useIgnoreCase && !useTrim)
                {
                    if (item1.ToLower() == item2.ToLower()) return true;
                }
                else if(!useIgnoreCase && useTrim)
                {
                    if (item1.Trim() == item2.Trim()) return true;
                }
                else {
                    if (item1 == item2) return true;
                }
            }
        }
        return false;
    }

}
