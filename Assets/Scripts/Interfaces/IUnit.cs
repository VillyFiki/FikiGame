using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    public int MeleeDamage { get; set; }
    public float MeleeCooldown { get; set; }
    public Transform AttackPoint { get; set; }
    public float AttackRadius { get; set; }
    public int Health { get; set; }
    public float Speed { get; set; }


    public bool CanAttack(Unit unit);
    public void Attack(Unit unit);
    public Unit FindTarget();
    

    public void Move();
    public void Wandering();
    public void MoveTo(Vector3 position);
    public void Flip();

    public void TakeDamage(int damage);

}
