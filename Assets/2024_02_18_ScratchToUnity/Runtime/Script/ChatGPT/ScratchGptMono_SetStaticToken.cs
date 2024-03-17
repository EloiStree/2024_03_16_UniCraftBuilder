using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScratchGptMono_SetStaticToken : MonoBehaviour
{

    public Token m_tokenToUse;

    [System.Serializable]
    public class Token {
        public string m_token;
    }

    public void Awake()
    {
        ScratchGptUtility.SetTokenApiKey(m_tokenToUse.m_token);
    }
}
