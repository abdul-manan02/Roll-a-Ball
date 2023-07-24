using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayCasting : MonoBehaviour
{
    public LayerMask groundLayer;

    private void Update()
    {
        RayCastSingle();
    }

    private void RayCastSingle()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        RaycastHit hit;

        Debug.DrawRay(origin, direction*10f, Color.green);
        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.transform.name);
        }
    }
}
