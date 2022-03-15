using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector2 direction = Vector2.right;
    private bool up, down, left, right;
    private List<Transform> segments = new List<Transform>();
    [SerializeField] private Transform segmentPrefeb, segmentParent;
    [SerializeField] private int initialSize;
    void Start()
    {
        Reset();
        right = true;
    }

    // Update is called once per frame
    void Update()
    {
        InputController();
    }

    private void FixedUpdate()
    {
        MovementAndSegments();
    }
    private void InputController() {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && !down)
        {
            direction = Vector2.up;
            up = true;
            down = false;
            left = false;
            right = false;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && !up)
        {
            direction = Vector2.down;
            up = false;
            down = true;
            left = false;
            right = false;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && !right)
        {
            direction = Vector2.left;
            up = false;
            down = false;
            left = true;
            right = false;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && !left)
        {
            direction = Vector2.right;
            up = false;
            down = false;
            left = false;
            right = true;
        }
    }
    private void MovementAndSegments() {
        //Mathf.Round - Serve para que números .flutuantes se tornem inteiros
        for (int i = segments.Count - 1 ; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        
        this.transform.position = new Vector2(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y);
    }

    private void Reset()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);

        for (int i = 1; i < initialSize; i++)
        {
            Transform tempSegment = Instantiate(segmentPrefeb);
            tempSegment.transform.parent = segmentParent;
            segments.Add(tempSegment);
        }
        this.transform.position = Vector2.zero;
    }

    private void Grow()
    {
        Transform temSegment = Instantiate(segmentPrefeb, segments[segments.Count - 1].position, Quaternion.identity);
        temSegment.transform.parent = segmentParent;
        segments.Add(temSegment);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Comida"))
        {
            Grow();
        }
    }
}
