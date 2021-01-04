using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Move mainPlayer = new Move();
    NeuralNetwork myNetwork;
    Dictionary<Move,NeuralNetwork> enemies = new Dictionary<Move,NeuralNetwork>();
    private int[] layers = new int[] { 1, 10, 10, 1 };
    public void Start() {
        print("Tower initalized");
        myNetwork = new NeuralNetwork(layers);
    }

    public void Update()
    {
        Inform();
    }

    public void Register(Move player, bool isMain) {
        if (isMain)
        {
            mainPlayer = player;
        }
        else
        {
            print("We are trying to register " + player);
            NeuralNetwork net = new NeuralNetwork(layers);
            net.Mutate();
            enemies[player]=net;
        }
        print("REGISTER  " + player.transform.position + ", is main -> " + isMain);
    }

    public void Inform() {
        foreach (var p in enemies)
        {
            var direction = Feed(p);
            print("Trying to inform " + p.Key + " -> " + direction);
            if (p.Key != null)
            {
                p.Key.SetSuggestion(direction);
            }
        }
    }

    public Direction Feed(KeyValuePair<Move, NeuralNetwork> p) 
    {
        float[] inputs = new float[1];

        float dist = Vector2.Distance(mainPlayer.GetPosition().position, p.Key.GetPosition().position);

        inputs[0] = dist;
            
        float[] output = p.Value.FeedForward(inputs);
        return (Direction)(output[0]%4);
    }
}