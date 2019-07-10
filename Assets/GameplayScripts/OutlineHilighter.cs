using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineHilighter : MonoBehaviour
{
    [SerializeField]
    private Color m_unhilightedColor;

    [SerializeField]
    private Color m_hilightedColor;

    private Color m_colorToUse;

    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;

    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

    // OutlineHilighter has been moved up in the script exicution order so this will run first
    void Update()
    {
        m_colorToUse = m_unhilightedColor;
    }

    public void Hilight()
    {
        m_colorToUse = m_hilightedColor;
    }

    private void LateUpdate()
    {
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_OutlineColor", m_colorToUse);
        _renderer.SetPropertyBlock(_propBlock);
    }
}
