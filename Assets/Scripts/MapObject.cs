using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapObject : MonoBehaviour
{
    public Image _hpbar;
    public GameObject _dealt;
    public GameObject _explo;

    public int _hp = 0;
    public int _hpMax = 10;

    protected virtual void Start()
    {
        _hp = _hpMax;
        UpdateHPBar();
        _dealt.SetActive(false);
        _explo.SetActive(false);
    }

    protected virtual void Update()
    {
        
    }

    public void UpdateHPBar()
    {
        if (_hpbar != null)
        {
            _hpbar.fillAmount = (float)_hp / (float)_hpMax;
        }
    }

    public virtual void Damaged(int dmg = 1)
    {
        _hp -= dmg;
        UpdateHPBar();

        if (_hp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
