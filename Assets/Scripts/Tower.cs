using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    List<Move> players = new List<Move>();
    System.Random rnd = new System.Random();
    public void Start() {
        print("Tower initalized");
    }

    public void Update() {
        Inform();
    }

    public void Register(Move player) {
        this.players.Add(player);
        print("REGISTER  " + player.transform.position);
    }

    public void Inform() {
        foreach (var p in players)
        {
            if(rnd.Next(10) < 3) {
                print("Trying to inform " + p);
                p.GetSuggestion("GO UP");
            }
        }
    }
}