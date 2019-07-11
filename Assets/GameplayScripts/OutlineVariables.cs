using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// static serialized variables can't be edited in editor
public class OutlineVariables : MonoBehaviour
{
    private static OutlineVariables m_instance;

    public static OutlineVariables GetInstance()
    {
        return m_instance;
    }
    
    [SerializeField]
    public Color m_unhilightedColor;

    [SerializeField]
    public Color m_hilightedColor1;

    [SerializeField]
    public Color m_hilightedColor2;

    [SerializeField]
    public float m_hilightPulseSpeed = 2.0f;
    
    void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        else
        {
            Debug.LogError("OutlineVariables instance already exists.");
            Destroy(gameObject);
        }
    }
}
