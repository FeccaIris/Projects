using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public static Sector _self;

    public Transform _mapTrans;

    public Sector_Area[] _sectorArr;
    public Map_Module[] _moduleArr;

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

    public void TransPosition(Transform mvTo)                        // �÷��̾ �� ��⿡ �浹�� �� ȣ��
    {
        transform.position = mvTo.position;

        Collider();
    }

    public void Collider()
    {
        _checkSector.Clear();
        _checkModule.Clear();

        for (int i = 0; i < _sectorArr.Length; i++)                     // �浹ü
        {
            BoxCollider2D col = _sectorArr[i].gameObject.GetComponent<BoxCollider2D>();
            if (col.enabled == false)
            {
                col.enabled = true;
            }
        }

        Invoke("Waiting", 0.11f);
    }

    public void Waiting()                                               // �ൿ ����Ʈ
    {
        List<Sector_Area>  toAssi = new List<Sector_Area>(_sectorArr);
        List<Map_Module> toMove = new List<Map_Module>(_moduleArr);

        int ccs = _checkSector.Count;                                   // check counter sector
        for(int i = 0; i < ccs; i++)
        {
            toAssi.Remove(_checkSector[i]);
        }
        int ccm = _checkModule.Count;
        for (int i = 0; i < ccm; i++)
        {
            toMove.Remove(_checkModule[i]);
        }

        Map._self.MoveMapModule(toAssi, toMove);
    }

}
