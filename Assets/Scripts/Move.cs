using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;
    public float speed = 16f;
    public GameObject wallPrefab;
    // public GameObject tower;
    protected Collider2D wall;
    protected Vector2 lastWallEnd;
    protected int currDir;

    public virtual void Start()
    {
        MoveUp();
    }

    public virtual void Update()
    {
        UserMove();
    }

    void UserMove()
    {
        if (Input.GetKeyDown(upKey))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(downKey))
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(rightKey))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(leftKey))
        {
            MoveLeft();
        }
        fitColliderBetween(wall, lastWallEnd, transform.position);
    }

    protected void MoveLeft()
    {
        GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
        spawnWall();
        currDir = 2;
    }
    protected void MoveRight()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        spawnWall();
        currDir = 3;
    }
    protected void MoveUp()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        spawnWall();
        currDir = 0;
    }
    protected void MoveDown()
    {
        GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
        spawnWall();
        currDir = 1;
    }

    void spawnWall()
    {
        lastWallEnd = transform.position;
        GameObject g = Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();
    }

    protected void fitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        // Calculate the Center Position
        co.transform.position = a + (b - a) * 0.5f;

        // Scale it (horizontally or vertically)
        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
        {
            co.transform.localScale = new Vector2(dist + 1, 1);
        }
        else
        {
            co.transform.localScale = new Vector2(1, dist + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co != wall)
        {
            print("Player lost: " + name);
            Destroy(gameObject);
        }
    }

    public void GetSuggestion(string s) {
        print(name + "  tower info:  " + s);
    }
}
