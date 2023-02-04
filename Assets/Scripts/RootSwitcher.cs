using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSwitcher : MonoBehaviour
{
    public List<Movement> roots;
    public int maxRoots = 10;
    public int activeRoot;
    [SerializeField] Movement rootPrefab;

    private void Awake()
    {
        var startRoot = Instantiate(rootPrefab, transform.position, Quaternion.identity);

        AddNewRoot(startRoot);

    }

    public void AddNewRoot(Movement newRoot)
    {
        roots.Add(newRoot);

        var i = 0;
        foreach(Movement root in roots)
        {
            root.rootNumber = i;
            i++;
        }

        UpdateCurrentlyControlled();
    }

    public void RemoveRoot(Movement rootToRemove)
    {
        
        roots.Remove(rootToRemove);


        var i = 0;
        foreach (Movement root in roots)
        {
            root.rootNumber = i;
            i++;
        }

        UpdateCurrentlyControlled();
    }


    private void Update()
    {
        if (FindObjectOfType<CanvasController>().GetGamePaused())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activeRoot++;
            if (activeRoot > roots.Count - 1)
            {
                activeRoot = 0;
            }

            UpdateCurrentlyControlled();
        }
    }

    private void UpdateCurrentlyControlled()
    {
        foreach (Movement root in roots)
        {
            if (root.rootNumber != activeRoot)
            {
                root.currentlyControlling = false;
                root.GetComponent<SpriteRenderer>().color = Color.white;

            }
            else
            {
                root.currentlyControlling = true;
                root.GetComponent<SpriteRenderer>().color = Color.green;

            }
        }
    }
}
