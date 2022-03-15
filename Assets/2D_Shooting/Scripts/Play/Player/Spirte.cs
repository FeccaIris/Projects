using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ss
{
    public class Spirte : MonoBehaviour
    {
        public void OffsetMove()
        {
            Player player = GameManager.I._player;

            Vector3 offset = player._offset_move;

            if (player._flip == true)
            {
                offset.y *= -1;
            }
            player._sword_sp.transform.localPosition = offset;
        }
        public void OffsetShoot()
        {
            Player player = GameManager.I._player;

            Vector3 offset = player._offset_shoot;

            if (player._flip == true)
            {
                offset.y *= -1;
            }
            player._sword_sp.transform.localPosition = offset;
        }
    }
}
