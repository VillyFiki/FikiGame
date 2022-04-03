using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    public LayerMask Layer;

    #region Properties

    public virtual int MeleeDamage { get => _meleeDamage; set => _meleeDamage = value; }
    [Space][Header("Main")] 
    [SerializeField] private int _meleeDamage;
    public virtual float MeleeCooldown { get => _meleeCooldown; set => _meleeCooldown = value; }
    [SerializeField] private float _meleeCooldown;
    public virtual Transform AttackPoint { get => _attackPoint; set => _attackPoint = value; }
    [SerializeField] private Transform _attackPoint;
    public virtual float AttackRadius { get => _attackRadius; set => _attackRadius = value; }
    [SerializeField] [Min(1)] private float _attackRadius;
    public virtual int Health { get => _health; set => _health = value; }
    [SerializeField] private int _health = 10;
    public virtual float Speed { get => _speed; set => _speed = value; }
    [SerializeField] private float _speed;
    
    #endregion

    #region Private Fields

    protected Rigidbody2D _rb;
    protected Animator _animator;
    protected bool _isFacingRight = false;
    protected System.Random rnd = new System.Random();

    #endregion









    public virtual bool CanAttack(Unit unit)
    {
        if (_cooldown < 0)
        {
            var distance = Vector3.Distance(transform.position, unit.transform.position);
            if (distance < 1)
            {
                _cooldown = MeleeCooldown;
                return true;
            }
        }

        _cooldown -= Time.deltaTime;
        return false;
    }

    protected float _cooldown;
    public virtual void Attack(Unit unit)
    {
        if (_cooldown < 0)
        {
            _cooldown = MeleeCooldown;
            unit.TakeDamage(MeleeDamage);
        }

        _cooldown -= Time.deltaTime;
    }
    public virtual Unit FindTarget()
    {
        AttackPoint = transform;
        var collisions = Physics2D.OverlapCircleAll(AttackPoint.position, 100f, Layer.value);

        foreach (var collision in collisions)
        {
            var unit = collision.GetComponent<Unit>();

            if (unit) return unit;
        }

        return null;
    }



    public virtual void MoveTo(Vector3 position)
    {
        var move = (position - transform.position).normalized;
        move = Vector2.ClampMagnitude(move, Speed);
        _rb.velocity = move;

        if (position.x > 0 && !_isFacingRight) Flip();
        else if (position.x < 0 && _isFacingRight) Flip();
    }
    
    public virtual void Move()
    {
        Wandering();
    }

    public virtual void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    
    private float _moveCooldown = 1f;
    public void Wandering()
    {
        if (_moveCooldown < 0)
        {
            MoveTo(new Vector2((float)(rnd.Next(-1, 2) + rnd.NextDouble()), (float)(rnd.Next(-1, 2) + rnd.NextDouble())));
            _moveCooldown = 1f;
        }
        else _moveCooldown -= Time.deltaTime;
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        _animator.SetTrigger("Damage");
        if (Health < 0) Death();
    }

    public virtual void Death()
    {
        Destroy(this.gameObject);
    }
    
    #region MonoBehaviour Implements

    public virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Layer = Relationships.GetEnemyLayerByCurrentLayer(Layer);
        if(AttackPoint) AttackPoint = transform;
    }

    protected Unit target;
    public virtual void Update()
    {
        Move();
        target = FindTarget();
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        var orb = collider.GetComponent<IProjectile>();

        if (orb != null) TakeDamage(orb.Damage);
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);
    }

    
    #endregion
}
