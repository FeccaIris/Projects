using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public static Sector _self;
    public Transform _mapTrans;

    public List<Sector_Area> _sectorList = new List<Sector_Area>();
    public List<Map_Module> _moduleList = new List<Map_Module>();

    void Start()
    {
        _self = this;

        int cc = transform.childCount;                                      // 섹터 리스트 작성
        for (int i = 0; i < cc; i++)
        {
            GameObject listMember = transform.GetChild(i).gameObject;
            _sectorList.Add(listMember.GetComponent<Sector_Area>());
        }
        int cc2 = _mapTrans.childCount;                                     // 모듈 리스트 작성
        for (int i = 0; i < cc2; i++)
        {
            GameObject listMember = _mapTrans.GetChild(i).gameObject;
            _moduleList.Add(listMember.GetComponent<Map_Module>());
        }
    }
    public void TransPosition(Transform mvTo)                        // 플레이어가 맵 모듈에 충돌할 때
    {
        transform.position = mvTo.position;

        foreach (Sector_Area x in _sectorList)
        {
            BoxCollider2D col = x.gameObject.GetComponent<BoxCollider2D>();
            if (col.enabled == false)
            {
                col.enabled = true;
            }
        }
    }

    public void Compare(Map_Module asMM, Sector_Area asSA)
    {
        List<Sector_Area> emptySector = _sectorList;
        List<Map_Module> moveList = _moduleList;

        emptySector.Remove(asSA);
        moveList.Remove(asMM);

        Map._self.MoveModule(emptySector, moveList);
    }
}
