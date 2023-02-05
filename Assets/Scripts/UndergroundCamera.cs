using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundCamera : MonoBehaviour
{
    [SerializeField] float panBetweenTargetSpeed = 0.1f;
    RootSwitcher rootSwitcher;

    private void Awake()
    {
        rootSwitcher = FindObjectOfType<RootSwitcher>();
    }
    private void Update()
    {
        if(FindObjectOfType<CanvasController>().GetGamePaused())
        {
            return;
        }
        var targetPos = new Vector3(transform.position.x, rootSwitcher.roots[rootSwitcher.activeRoot].transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, panBetweenTargetSpeed * Time.deltaTime);
    }

}
