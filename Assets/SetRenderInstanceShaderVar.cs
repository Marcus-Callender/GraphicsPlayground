using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRenderInstanceShaderVar : MonoBehaviour
{
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;

    [SerializeField]
    private int var = 1;

    [SerializeField]
    private Color Color;

    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.GetPropertyBlock(_propBlock);

        Debug.Log(gameObject.name + ", RefNumber: " + _propBlock.GetInt("_RefNumber"));

        //_propBlock.SetInt("_RefNumber", var);
        _propBlock.SetColor("_Color", Color);
        _renderer.SetPropertyBlock(_propBlock);
    }
}
