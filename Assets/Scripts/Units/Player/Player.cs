using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : Unit
{
    [SerializeField]
    private Vector3 _change;
    [SerializeField] 
    public Transform Effects;
    [SerializeField]
    public Inventory Inventory;

    public override void Update()
    {
        if(Input.GetMouseButtonDown(0))Inventory.EquipAll();
    }

    void FixedUpdate()
    {
        _change = Vector2.zero;
        _change.x = Input.GetAxis("Horizontal");
        _change.y = Input.GetAxis("Vertical");

        Move();
    }
    

    public override void Move()
    {
        _animator.SetBool("IsMoving", Mathf.Abs(_change.x) + Mathf.Abs(_change.y) > 0);

        //_rb.MovePosition(change * 2 * Speed * Time.deltaTime + transform.position);

        var move = new Vector2(_change.x, _change.y) * Speed;
        move = Vector2.ClampMagnitude(move, Speed);
        _rb.velocity = move;

        if (_change.x > 0 && !_isFacingRight)
            Flip();
        else if (_change.x < 0 && _isFacingRight)
            Flip();
    }
}
