using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public int _counter;

    public static Sector _self;
    public Transform _mapTrans;

    public Sector_Area[] _sectorArr;
    public Map_Module[] _moduleArr;

    public List<Sector_Area> _sectorList = new List<Sector_Area>();
    public List<Map_Module> _moduleList = new List<Map_Module>();

    public List<Sector_Area> _checkSector = new List<Sector_Area>();
    public List<Map_Module> _checkModule = new List<Map_Module>();

    void Start()
    {
        _self = this;
        _sectorArr = GetComponentsInChildren<Sector_Area>();
        _moduleArr = _mapTrans.GetComponentsInChildren<Map_Module>();
    }

    void Update()
    {
        
    }

    public void WriteList()
    {
        _sectorList = new List<Sector_Area>(_sectorArr);
        _moduleList = new List<Map_Module>(_moduleArr);
    }

    public void TransPosition(Transform mvTo)                        // 플레이어가 맵 모듈에 충돌할 때 호출
    {
        transform.position = mvTo.position;

        Collider();
    }

    public void Collider()
    {
        WriteList();
        _checkSector.Clear();
        _checkModule.Clear();
        _counter = 9;

        for (int i = 0; i<_sectorArr.Length; i++)                     // 충돌체
        {
            BoxCollider2D col = _sectorArr[i].gameObject.GetComponent<BoxCollider2D>();
            if (col.enabled == false)
            {
                col.enabled = true;
            }
        }
    }

    public void Count()
    {
        _counter -= 1;
    }

    public void Arrange()
    {
        Debug.Log(_checkSector.Count);
        
        Map._self.MoveMapModule(_sectorList, _moduleList);
    }


}
