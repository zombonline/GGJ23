using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootSwitcher : MonoBehaviour
{
    public List<Movement> roots;
    public int maxRoots = 10;
    public int activeRoot;
    [SerializeField] Movement rootPrefab;
    [SerializeField] Sprite active, inactive;
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

        if(roots.Count == 0)
        {
            FindObjectOfType<CanvasController>().GameOver();
            FindObjectOfType<BGMManager>().FadeToLose(FindObjectOfType<TreeGrowth>().currentLevel);
            return;
        }
        var i = 0;

        Movement storedRoot = null;
        foreach (Movement root in roots)
        {
            if(root.rootNumber == activeRoot)
            {
                storedRoot = root;
            }
            root.rootNumber = i;
            i++;
        }
        activeRoot = storedRoot.rootNumber;

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

    public void RootPointClick(Movement root)
    {
        activeRoot = root.rootNumber;

        UpdateCurrentlyControlled();
    }

    private void UpdateCurrentlyControlled()
    {

        foreach (Movement root in roots)
        {
            if (root.rootNumber != activeRoot)
            {
                root.currentlyControlling = false;
                root.GetComponent<SpriteRenderer>().sprite = inactive;

            }
            else
            {
                root.currentlyControlling = true;
                root.GetComponent<SpriteRenderer>().sprite = active;

            }
        }
    }
}
