using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pest : MonoBehaviour
{
    [SerializeField] float dir;
    [SerializeField] float speed;
    BoxCollider2D area;

    [SerializeField] int waterPointsToRemove = 1;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3.right * dir), speed * Time.deltaTime);
        if (dir == -1)
        {
            transform.localScale = new Vector3(dir * .25f, transform.localScale.y, transform.localScale.z);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Level"))
        {
            dir = -dir;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //stop root moving
            collision.GetComponent<Movement>().rootFinished = true;
            //disable root icon
            Destroy(collision.GetComponent<SpriteRenderer>());
            Destroy(collision.GetComponent<CircleCollider2D>());
            //remove water points
            FindObjectOfType<TreeGrowth>().UpdateWaterPoints(-waterPointsToRemove);
            //update root switcher so player can not control root
            FindObjectOfType<RootSwitcher>().RemoveRoot(collision.GetComponent<Movement>());
        }
    }
}
