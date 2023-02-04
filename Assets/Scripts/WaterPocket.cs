using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPocket : MonoBehaviour
{
    [SerializeField] Movement rootPointPrefab;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(FindObjectOfType<RootSwitcher>().roots.Count < FindObjectOfType<RootSwitcher>().maxRoots)
        {
            var randomChance = Random.Range(0, 1);
            var rotation = 0f;
            if(randomChance == 0)
            {
                rotation = collision.transform.rotation.z + 0.2f;
            }
            else
            {
                rotation = collision.transform.rotation.z - 0.2f;
            }
            var newRoot = Instantiate(rootPointPrefab, collision.transform.position, new Quaternion(0, 0, rotation, transform.rotation.w));
            FindObjectOfType<RootSwitcher>().AddNewRoot(newRoot);

        }

        Destroy(gameObject);
    }



}
