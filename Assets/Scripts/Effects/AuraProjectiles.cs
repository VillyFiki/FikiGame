using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


class Orb
{
    public Orb(IProjectile projectile, float angle)
    {
        Angle = angle;
        AnglePosition = angle;
        Projectile = projectile;
    }

    public IProjectile Projectile;
    public float Angle;
    public float AnglePosition;
}

public class AuraProjectiles : MonoBehaviour, IEffect
{
    [SerializeField]
    public IProjectile projectile;
    [SerializeField]
    private List<Orb> orbs = new List<Orb>();
    private int orbCount = 6;

    [SerializeField]
    public float radius = 1f;
    [SerializeField]
    public float speed = 1f;
    [SerializeField]
    public float speedFocus = 1f;
    [SerializeField]
    public float speedBoost = 3f;

    void Start()
    {

        for (int i = 0; i < orbCount; i++)
        {
            var proj = Instantiate(projectile, transform);
            
            orbs.Add(new Orb(proj, (2 * Mathf.PI * i) / orbCount));
        }
    }

    private int _offset = 0;

    void Update()
    {
        foreach (var orb in orbs)
        {
            var x = transform.position.x + ((_offset / (2 * Mathf.PI)) * (Mathf.Cos(orb.Angle) * radius));
            var y = transform.position.y + ((_offset / (2 * Mathf.PI)) * (Mathf.Sin(orb.Angle) * radius));
            orb.Projectile.transform.position = new Vector3(x, y);

            orb.Angle += Time.deltaTime * (speed);

            if (orb.Angle > orb.AnglePosition+(Mathf.PI * 2))
            {
                orb.Angle = orb.AnglePosition;

                if(_offset<=7)
                    _offset += 1;
            }
        }
    }


    void OnDestroy()
    {
        foreach (var orb in orbs)
        {
            Destroy(orb.Projectile.gameObject);
        }
    }


}
