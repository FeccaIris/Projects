using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Cam : MonoBehaviour
    {
        void Update()
        {
            if (Player.I != null)
            {
                Vector3 t = Player.I.transform.position;
                Vector3 pos = new Vector3(t.x, t.y, transform.position.z);
                if(transform.position != pos)
                    transform.position = pos;
            }
        }
    }
}
