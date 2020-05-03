using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _positionMove;
    public  float _speed = 10f;

    private void Start()
    {
        _positionMove = transform.position;
    }

    private void Update()
    {
        UpdateInput();
        UpdateMove();
    }

    private void UpdateInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _positionMove =
                UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _positionMove.z = 0;
        }
    }

    private void UpdateMove()
    {
        float moveDelta = (_positionMove - transform.position).magnitude;
        if (moveDelta <= _speed * Time.deltaTime)
        {
            transform.position = _positionMove;
            return;
        }
        Vector3 moveDir = _positionMove - transform.position;
        //
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector2(0, 0 ));
        //
        moveDir.Normalize();
        transform.position += moveDir * _speed * Time.deltaTime;
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}



