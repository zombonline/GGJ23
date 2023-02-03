using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSwitcher : MonoBehaviour
{
    [SerializeField] Movement[] roots;
    int activeRoot;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            activeRoot++;
            if(activeRoot > roots.Length - 1)
            {
                activeRoot = 0;
            }

            foreach(Movement root in roots)
            {
                if(root.rootNumber != activeRoot)
                {
                    root.currentlyControlling = false;
                }
                else
                {
                    root.currentlyControlling = true;
                }
            }
        }
    }

}
