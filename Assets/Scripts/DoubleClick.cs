using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClick : MonoBehaviour
{


    public bool _onTimer = false;
    public float _doubleClickInterval = 0.2f;
    public float _timer = 0;
    public int _clickCount = 0;

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        if(_timer < _doubleClickInterval && _clickCount >= 2)
        {
            DoubleClicked();

            _clickCount = 0;
            _onTimer = false;
            _timer = 0;
        }
        if (_clickCount >= 2)
        {
            _clickCount = 0;
            _onTimer = false;
            _timer = 0;
        }
        if(_onTimer == true)
        {
            _timer += Time.deltaTime;
            if(_timer >= _doubleClickInterval)
            {
                _clickCount = 0;
                _onTimer = false;
                _timer = 0;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(_onTimer == false)
            {
                _onTimer = true;
            }
            _clickCount += 1;
        }
    }//업데이트
    protected virtual void DoubleClicked()
    {

    }
}
