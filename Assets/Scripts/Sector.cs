using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public static Sector _self;
    public Transform _mapTrans;

    public List<Sector_Area> _sectorList = new List<Sector_Area>();
    public List<Map_Module> _moduleList = new List<Map_Module>();

    public List<Sector_Area> _emptySector = new List<Sector_Area>();
    public List<Map_Module> _movingModule = new List<Map_Module>();

    public int _selected;



    void Start()
    {
        _self = this;

        int cc = transform.childCount;                                      // 섹터 리스트 작성
        for (int i = 0; i < cc; i++)
        {
            GameObject Member = transform.GetChild(i).gameObject;
            _sectorList.Add(Member.GetComponent<Sector_Area>());
            _emptySector.Add(Member.GetComponent<Sector_Area>());
        }
        int cc2 = _mapTrans.childCount;                                     // 모듈 리스트 작성
        for (int i = 0; i < cc2; i++)
        {
            GameObject Member = _mapTrans.GetChild(i).gameObject;
            _moduleList.Add(Member.GetComponent<Map_Module>());
            _movingModule.Add(Member.GetComponent<Map_Module>());
        }


    }

    public void ToSelect()
    {
        _selected = 0;
        _emptySector.Clear();
        _movingModule.Clear();

        int cc = transform.childCount;                                      
        for (int i = 0; i < cc; i++)
        {
            GameObject Member = transform.GetChild(i).gameObject;
            _emptySector.Add(Member.GetComponent<Sector_Area>());
        }
        int cc2 = _mapTrans.childCount;                                     
        for (int i = 0; i < cc2; i++)
        {
            GameObject Member = _mapTrans.GetChild(i).gameObject;
            _movingModule.Add(Member.GetComponent<Map_Module>());
        }
        Debug.Log("ToSelect");
    }

    public void Select(Sector_Area sa, Map_Module mm)
    {
        _selected += 1;
        if (_emptySector.Contains(sa))
        {
            _emptySector.Remove(sa);
        }
        if (_movingModule.Contains(mm))
        {
            _movingModule.Remove(mm);
        }
    }

    public void TransPosition(Transform mvTo)                        // 플레이어가 맵 모듈에 충돌할 때 호출
    {
        transform.position = mvTo.position;

        foreach (Sector_Area sa in _sectorList)
        {
            BoxCollider2D col = sa.gameObject.GetComponent<BoxCollider2D>();
            if (col.enabled == false)
            {
                col.enabled = true;
            }
        }
    }
}
