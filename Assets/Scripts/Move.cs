using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    UP=0,
    DOWN=1,
    LEFT=2,
    RIGHT=3
}

public class Move : MonoBehaviour
{
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;
    public float speed = 16f;
    public GameObject wallPrefab;
    protected Collider2D wall;
    protected Vector2 lastWallEnd;
    protected Direction currDir;
    protected Direction lastSuggestion;

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
        if (Input.GetKeyDown(upKey) && currDir != Direction.DOWN)
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(downKey) && currDir != Direction.UP)
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(leftKey) && currDir != Direction.RIGHT)
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(rightKey) && currDir != Direction.LEFT)
        {
            MoveRight();
        }
        fitColliderBetween(wall, lastWallEnd, transform.position);
    }

    protected void MoveLeft()
    {
        GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
        spawnWall();
        currDir = Direction.LEFT;
    }
    protected void MoveRight()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        spawnWall();
        currDir = Direction.RIGHT;
    }
    protected void MoveUp()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        spawnWall();
        currDir = Direction.UP;
    }
    protected void MoveDown()
    {
        GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
        spawnWall();
        currDir = Direction.DOWN;
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

    public void SetSuggestion(Direction s) {
        print(name + "  tower info:  " + s);
        this.lastSuggestion = s;
    }

    public Direction GetSuggestion()
    {
        return this.lastSuggestion;
    }

    public Direction GetDirection()
    {
        return this.currDir;
    }

    public Transform GetPosition()
    {
        return transform;
    }
}
