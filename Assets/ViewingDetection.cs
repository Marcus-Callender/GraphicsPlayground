using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewingDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, transform.forward, out hitInfo, 50.0f);

        OutlineHilighter outlineScript = hitInfo.collider.GetComponent<OutlineHilighter>();

        if (outlineScript != null)
        {
            outlineScript.Hilight();
        }

        Debug.DrawRay(transform.position, transform.forward * 50.0f, Color.blue);
    }
}
