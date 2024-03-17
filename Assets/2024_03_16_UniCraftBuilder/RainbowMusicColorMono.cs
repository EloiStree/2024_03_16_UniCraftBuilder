using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RainbowMusicColorMono : MonoBehaviour
{
    public UnityEvent<Color> m_onUpdate;
    public Color m_current;
    public Color m_next;
    public float m_minChange=0.3f;
    public float m_maxChange=3;

    IEnumerator Start()
    {

        while (true) {

            m_next.r = Random.value;
            m_next.g = Random.value;
            m_next.b = Random.value;
            m_next.a = 1;
            yield return new WaitForSeconds(Random.Range(m_minChange, m_maxChange));
        }
    }

    void Update()
    {
        m_current = Color.Lerp(m_current, m_next, Time.deltaTime);
        m_onUpdate.Invoke(m_current);
    }
}
