using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundCamera : MonoBehaviour
{
    [SerializeField] float panBetweenTargetSpeed = 0.1f;
    RootSwitcher rootSwitcher;
    Vector3 targetPos;
    bool followingTarget = false;

    [SerializeField] float highestHeight, lowestHeight;
    private void Start()
    {
        rootSwitcher = FindObjectOfType<RootSwitcher>();
        targetPos = new Vector3(transform.position.x, rootSwitcher.roots[rootSwitcher.activeRoot].transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, panBetweenTargetSpeed * Time.deltaTime);


    }
    private void Update()
    {
      

        if(FindObjectOfType<CanvasController>().GetGamePaused())
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space) || followingTarget)
        {
            targetPos = new Vector3(transform.position.x, rootSwitcher.roots[rootSwitcher.activeRoot].transform.position.y, transform.position.z);
            followingTarget= true;
        }
        if (Input.mouseScrollDelta.y != 0)
        {
            targetPos = new Vector3(transform.position.x, targetPos.y + Input.mouseScrollDelta.y * 2.5f, transform.position.z);
            followingTarget = false;
        }

        if (targetPos.y < lowestHeight)
        {
            targetPos.y = lowestHeight;
        }
        if (targetPos.y > highestHeight)
        {
            targetPos.y = highestHeight;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, panBetweenTargetSpeed * Time.deltaTime);
    }

}
