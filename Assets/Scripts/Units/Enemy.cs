using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField]
    public ParticleSystem AttackParticle;


    public override Unit FindTarget()
    {
        AttackPoint = transform;
        var collisions = Physics2D.OverlapCircleAll(AttackPoint.position, 100f, Layer.value);

        foreach (var collision in collisions)
        {
            var unit = collision.GetComponent<Unit>();

            if (unit) target = unit;
        }

        return target;
    }

    public override void Attack(Unit unit)
    {
        var targetPos = target.transform.position;
        var particlePos = AttackParticle.transform.position;

        float angle = 180+Mathf.Atan2(particlePos.y - targetPos.y, particlePos.x - targetPos.x) * Mathf.Rad2Deg;
        
        AttackParticle.transform.rotation = Quaternion.Euler(0, 0, angle);
        AttackParticle.Play();
        unit.TakeDamage(MeleeDamage);
    }
    public override bool CanAttack(Unit unit)
    {
        if (_cooldown < 0)
        {
            var distance = Vector3.Distance(transform.position, unit.transform.position);

            if (distance < AttackRadius)
            {
                _cooldown = MeleeCooldown;
                return true;
            }
        }

        _cooldown -= Time.deltaTime;
        return false;
    }

    public override void Update()
    {
        target = FindTarget();

        if (target != null)
            if (CanAttack(target))
            {
                Attack(target);
            }
    }

    public void FixedUpdate()
    {
        _rb.velocity = Vector2.zero;
        if (target == null) return;

        var distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < AttackRadius-0.3f)
        {
            _rb.mass = 1000;
        }
        else
        {
            Move();
        }
    }


    public override void Move()
    {
        MoveTo(target.transform.position);
    }
}
