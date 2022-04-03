using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] public bool Enabled = false;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private Camera Camera;

    [SerializeField]
    private Enemy Skeleton;

    private float timeBeforeSpawning;
    private const float spawningCooldown = 2f;
    private System.Random rnd = new System.Random();
    void Start()
    {

    }

    void Update()
    {
        if (!Enabled) return;

        if (timeBeforeSpawning >= spawningCooldown)
        {
            var orient1 = RandomExtension.ShuffleRange(0, 1);
            var edge = rnd.Next(2);
            Vector2 spawningPos = new Vector2();
            Vector2 max = Camera.ViewportToWorldPoint(new Vector2(orient1[0], orient1[1]));
            
            Vector2 min = Camera.ViewportToWorldPoint(new Vector2(edge, edge));
            
            if (edge == 1)
            {
                if (orient1[0] == 1)
                {
                    max += Vector2.right * 10;
                    min += Vector2.right * 10;

                    max += Vector2.down * 10;
                    min += Vector2.up * 10;
                }
                else
                {
                    max += Vector2.up * 10;
                    min += Vector2.up * 10;

                    max += Vector2.left * 10;
                    min += Vector2.right * 10;
                }

                var tmp = min;
                min = max;
                max = tmp;
            }
            else
            {
                if (orient1[0] == 1)
                {
                    max += Vector2.down * 10;
                    min += Vector2.down * 10;

                    max += Vector2.right * 10;
                    min += Vector2.left * 10;
                }
                else
                {
                    max += Vector2.left * 10;
                    min += Vector2.left * 10;

                    max += Vector2.up * 10;
                    min += Vector2.down * 10;
                }
            }

            
            
            var randomX = rnd.Next((int)min.x, (int)max.x) + (float)rnd.NextDouble();
            var randomY = rnd.Next((int)min.y, (int)max.y) + (float)rnd.NextDouble();

            spawningPos.x = randomX;
            spawningPos.y = randomY;

            var mob1 = Instantiate(Skeleton);
            mob1.transform.position = spawningPos;
            
            timeBeforeSpawning = 0;
        }

        timeBeforeSpawning += Time.deltaTime;
    }
}
