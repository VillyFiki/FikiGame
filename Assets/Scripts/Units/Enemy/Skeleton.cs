using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Unit
{
    public Transform target;

    public override void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

        _rb = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();

        pos = transform.position;
    }

    private Vector2 pos;

    public override void Update()
    {
        _rb.MovePosition(Vector3.MoveTowards(transform.position, target.position, 2 * Time.deltaTime));
        _animator.SetFloat("Walking", Mathf.Abs(pos.x - transform.position.x)+Mathf.Abs(pos.y - transform.position.y));

        Vector3 theScale = transform.localScale;

        if (target.position.x > transform.position.x && !_isFacingRight) theScale.x *= -1;
        else if(target.position.x < transform.position.x && _isFacingRight) theScale.x *= -1;

        _isFacingRight = (target.position.x > transform.position.x);

        transform.localScale = theScale;
    }
}
