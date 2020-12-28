using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : Move
{
    System.Random rand = new System.Random();

    public override void Start()
    {
        base.Start();
        InvokeRepeating("NewMove", 1.1f, 1.3f);
    }
    public override void Update()
    {
        fitColliderBetween(wall, lastWallEnd, transform.position);
    }

    void NewMove() {
        int d = rand.Next(4);
        while(
            (d == 0 && currDir == 1) ||
            (d == 1 && currDir == 0) ||
            (d == 2 && currDir == 3) ||
            (d == 3 && currDir == 2)) {
                d = rand.Next(4);
            }
        
        if(d == 0) MoveUp();
        else if(d == 1) MoveDown();
        else if(d == 2) MoveLeft();
        else if(d == 3) MoveRight();
    }
}
