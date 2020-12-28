using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallsInit : MonoBehaviour
{
    public GameObject wall;
    public GameObject player_1_prefab;
    public GameObject player_2_prefab;
    public GameObject player_3_prefab;
    public GameObject player_4_prefab;
    public GameObject tower_prefab;
    GameObject p1;
    GameObject p2;
    GameObject p3;
    GameObject p4;
    GameObject tower;
    int Size;
    bool started = false;
    void Start()
    {
        int players = PlayerPrefs.GetInt("numOfPlayers", 2);
        tower = Instantiate(tower_prefab, new Vector2(0, 0), Quaternion.identity);
        
        Size = 50 * players;
        int start = -this.Size / 2;
        int end = this.Size / 2;
        for (int i = start; i < end; i++)
        {
            for (int j = start; j < end; j++)
            {
                if (i == start || j == start || i == end - 1 || j == end - 1)
                {
                    GameObject g = Instantiate(wall, new Vector2(i, j), Quaternion.identity);
                }
            }
        }
        p1 = Instantiate(player_1_prefab, new Vector2(0, 0), Quaternion.identity);
        Tower towerScript = tower.GetComponent<Tower>();
        var s1 = p1.GetComponent<Move>();
        towerScript.Register(s1);
        if (players > 1)
        {
            Thread.Sleep(119);
            p2 = Instantiate(player_2_prefab, new Vector2(-10, 0), Quaternion.identity);
            var s2 = p1.GetComponent<Move>();
            towerScript.Register(s2);
        }
        if (players > 2)
        {
            Thread.Sleep(121);
            p3 = Instantiate(player_3_prefab, new Vector2(-20, 0), Quaternion.identity);
            var s3 = p1.GetComponent<Move>();
            towerScript.Register(s3);
        }
        if (players > 3)
        {
            Thread.Sleep(123);
            p4 = Instantiate(player_4_prefab, new Vector2(-30, 0), Quaternion.identity);
            var s4 = p1.GetComponent<Move>();
            towerScript.Register(s4);
        }

        started = true;
    }

    void Update()
    {
        int players = PlayerPrefs.GetInt("numOfPlayers", 2);
        if(players == 1) {
            if (started && !p1)
            {
                SceneManager.LoadScene("menu");
            }
        } else {
            List<GameObject> p = new List<GameObject>() { p1, p2, p3, p4 };
            var list = p.Where(x => x != null);

            if (started && list.Count() <= 1)
            {
                Thread.Sleep(3000);
                SceneManager.LoadScene("menu");
            }
        }
    }
}