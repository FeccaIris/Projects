using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector_Center : Sector_Area
{
    private void OnTriggerExit2D(Collider2D col)
    {
        Map map = col.GetComponent<Map>();          // ���� �̵��� ȣ��
        if (map != null)
        {
            Invoke("CheckCall", 0.2f);
        }
        
    }
    public void CheckCall()
    {

    }
}
