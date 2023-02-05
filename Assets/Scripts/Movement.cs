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

    [SerializeField] Transform rayCastPoint;
    [SerializeField] float rayCastLength;
    [SerializeField] LayerMask obstacleLayer;
    bool flashSprite = false;


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

    public void CheckForObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayCastPoint.position, -transform.up, rayCastLength, obstacleLayer);
        Debug.DrawRay(rayCastPoint.position, -transform.up * rayCastLength, Color.red);
        if (hit.collider != null)
        {
            if (!flashSprite)
            {
                flashSprite = true;
                StartCoroutine(FlashSprite());
            }
        }
        else
        {
            flashSprite = false;
        }
    }

    IEnumerator FlashSprite()
    {
        while (flashSprite)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

    }

    private void Update()
    {
        if (FindObjectOfType<CanvasController>().GetGamePaused())
        {
            return;
        }
        if (rootFinished)
        {
            flashSprite = false;

            return;
        }
        CheckForObstacles();

        timer -= Time.deltaTime;
        if (currentlyControlling)
        {

            rotation = transform.localEulerAngles.z + Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.deltaTime;

            transform.localEulerAngles = new Vector3(0, 0, rotation);
        }

        if (timer <= 0)
        {
            FindObjectOfType<CanvasController>().metres += timeBetweenLinePosPlacement/2;
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
