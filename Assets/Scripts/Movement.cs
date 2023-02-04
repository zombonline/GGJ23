using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] public int rootNumber;

    public bool currentlyControlling;

    [SerializeField] float startRotation;

    float rotation;

    [SerializeField] float speed = 1.5f, rotateSpeed = 5f;
    int posCount = 0;

    [SerializeField] float timeBetweenLinePosPlacement = 0.1f;
    float timer;

    LineRenderer lineRenderer;
    EdgeCollider2D lineCollider;
    [SerializeField] GameObject rootPrefab;
    GameObject root;
    Mesh mesh;

    public bool rootFinished = false;


    private void Awake()
    {
        root = Instantiate(rootPrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = root.GetComponent<LineRenderer>();
        lineCollider = root.GetComponent<EdgeCollider2D>();
        lineRenderer.SetPosition(posCount, transform.position);
        mesh = new Mesh();
        InvokeRepeating(nameof(GenerateLineCollider), 1f, 1f);
        Invoke(nameof(ActivateCollider), 1f);
        rotation = startRotation;
    }

    void ActivateCollider()
    {
        GetComponent<CircleCollider2D>().enabled = true;
    }
    void GenerateLineCollider()
    {
        List<Vector2> points = new List<Vector2>();

        for(int i = 0; i < lineRenderer.positionCount; i++)
        {
            points.Add(lineRenderer.GetPosition(i));
        }

        lineCollider.SetPoints(points);
    }

    private void Update()
    {
        if (FindObjectOfType<CanvasController>().GetGamePaused())
        {
            return;
        }
        if (rootFinished)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (currentlyControlling)
        {

            rotation = transform.localEulerAngles.z + Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.deltaTime;

            transform.localEulerAngles = new Vector3(0, 0, rotation);
        }

        if (timer <= 0)
        {
            posCount++;
            lineRenderer.SetVertexCount(posCount + 2);

            timer = timeBetweenLinePosPlacement;
            for (int i = posCount; i < lineRenderer.positionCount; i++)
            {
                lineRenderer.SetPosition(i, new Vector2(transform.position.x + Random.Range(-0.1f,0.1f), transform.position.y + Random.Range(-0.1f,0.1f)));
            }
        }

        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
