using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour {


    public float _speed;
    private float curSpeed;

    public float _rayDistance;
    public Vector3 _rayOffset;
    private Rigidbody2D _rigidbody;
    private Transform _transform;

	void Start () {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        curSpeed = _speed;
    }
	

	void Update () {
        Rotate();
        Move();
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(curSpeed, _rigidbody.velocity.y);
    }

    private void Rotate()
    {
        RaycastHit2D hit = Physics2D.Raycast(_transform.position + _rayOffset, Vector2.down, _rayDistance);
        if (hit == false)
        {
            _transform.localScale = new Vector2(_transform.localScale.x * -1f, _transform.localScale.y);
            curSpeed = curSpeed * -1f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + _rayOffset, transform.position + _rayOffset + Vector3.down * _rayDistance);
    }

}
