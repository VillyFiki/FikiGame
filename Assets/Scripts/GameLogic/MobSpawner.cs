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

    [SerializeField]
    private Collider2D[] SpawningZones;

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
            var spawningPos = new Vector2(0,0);
            var zone = rnd.Next(SpawningZones.Length);


            var width = SpawningZones[zone].transform.localScale.x;
            var height = SpawningZones[zone].transform.localScale.y;

            var xc = SpawningZones[zone].transform.position.x;
            var yc = SpawningZones[zone].transform.position.y;

            var xl = xc - width / 2;
            var xr = xc + width / 2;

            var yt = yc + height / 2;
            var yb = yc - height / 2;

            var a = new Vector2(xc - width / 2, yc + height / 2);
            var b = new Vector2(xc + width / 2, yc - height / 2);

            timeBeforeSpawning = 0;
            
            spawningPos.x = rnd.Next((int)a.x, (int)b.x) + (float)rnd.NextDouble();
            spawningPos.y = rnd.Next((int)b.y, (int)a.y) + (float)rnd.NextDouble();

            var mob1 = Instantiate(Skeleton);
            mob1.transform.position = spawningPos;
        }

        timeBeforeSpawning += Time.deltaTime;
    }
}
