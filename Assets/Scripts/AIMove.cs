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
        var d = (int)GetSuggestion();
        while(
            (d == 0 && currDir == Direction.DOWN) ||
            (d == 1 && currDir == Direction.UP) ||
            (d == 2 && currDir == Direction.RIGHT) ||
            (d == 3 && currDir == Direction.LEFT)) {
                d = rand.Next(4);
            }
        
        if(d == 0) MoveUp();
        else if(d == 1) MoveDown();
        else if(d == 2) MoveLeft();
        else if(d == 3) MoveRight();
    }
}
