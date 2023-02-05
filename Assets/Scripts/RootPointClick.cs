using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootPointClick : MonoBehaviour
{
    CircleCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if(collider.bounds.Contains(hit.point))
            {
                FindObjectOfType<RootSwitcher>().RootPointClick(transform.parent.GetComponent<Movement>());
            }
        }
    }
}
