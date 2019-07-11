using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineHilighter : MonoBehaviour
{
    private OutlineVariables m_variables;

    private Color m_colorToUse;

    private Renderer m_renderer;
    private MaterialPropertyBlock m_propBlock;

    void Awake()
    {
        m_propBlock = new MaterialPropertyBlock();
        m_renderer = GetComponent<Renderer>();

        m_variables = OutlineVariables.GetInstance();
    }

    // OutlineHilighter has been moved up in the script exicution order so this will run first
    void Update()
    {
        m_colorToUse = m_variables.m_unhilightedColor;
    }

    public void Hilight()
    {
        m_colorToUse = Color.Lerp(m_variables.m_hilightedColor1, m_variables.m_hilightedColor2, (Mathf.Sin(Time.time * m_variables.m_hilightPulseSpeed) + 1) / 2f);
    }

    private void LateUpdate()
    {
        m_renderer.GetPropertyBlock(m_propBlock);
        m_propBlock.SetColor("_OutlineColor", m_colorToUse);
        m_renderer.SetPropertyBlock(m_propBlock);
    }
}
