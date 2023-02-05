using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1f;
    private void Update()
    {
        Vector3 rotation = new Vector3(0, 0, rotateSpeed);
        GetComponent<RectTransform>().Rotate(rotation * Time.deltaTime);
    }
}
