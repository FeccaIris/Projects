using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : DoubleClick
{
    public Transform _trans;
    public Rigidbody2D _rigid;
    public float _accel = 2.0f;
    public float _maxSpeed = 40.0f;
    public float _maxDash = 15.0f;
    
    protected override void Start()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        _trans = transform;
    }
    
    protected override void Update()
    {
        base.Update();

        #region Limit Speed
        if (_rigid.velocity.x >= _maxSpeed)
        {
            _rigid.velocity = new Vector2(_maxSpeed, _rigid.velocity.y);
        }
        else if(_rigid.velocity.x <= -_maxSpeed)
        {
            _rigid.velocity = new Vector2(-_maxSpeed, _rigid.velocity.y);
        }
        if (_rigid.velocity.y >= _maxSpeed)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _maxSpeed);
        }
        else if (_rigid.velocity.y <= -_maxSpeed)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, -_maxSpeed);
        }
        #endregion

        if (Input.GetMouseButton(0))
        {
            Vector2 mouse = Input.mousePosition;
            Vector2 pos = _trans.position;
            mouse = Camera.main.ScreenToWorldPoint(mouse);
            Vector2 move = mouse - pos;
            
            _rigid.AddForce(move * _accel);
        }
    }

    protected override void DoubleClicked()
    {
        base.DoubleClicked();
        Vector2 mouse = Input.mousePosition;
        Vector2 pos = _trans.position;

        mouse = Camera.main.ScreenToWorldPoint(mouse);
        Vector2 move = mouse - pos;
        _rigid.velocity = Vector2.zero;
        
        if(move.x >= _maxDash)
        {
            move.x = _maxDash;
        }
        else if(move.x <= -(_maxDash))
        {
            move.x = -(_maxDash);
        }
        if (move.y >= _maxDash)
        {
            move.y = _maxDash;
        }
        else if (move.y <= -(_maxDash))
        {
            move.y = -(_maxDash);
        }
        
        _trans.Translate(move);
    }
}
