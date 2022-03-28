using System.Collections;
using System.Collections.Generic;
using SV;

using UnityEngine;

namespace ss
{
    public class PlayerSprite : MonoBehaviour
{
        Player _player;
        Vector3 _offset= new Vector3();

        public void Init(Player p)
        {
            _player = p;
        }

        public void OffsetIdle()
        {
            if (_offset== _player._offset_idle)
                return;

            Vector3 offset = _player._offset_idle;

            Offset(offset);
        }
        public void OffsetMove()
        {
            Vector3 offset = _player._offset_move;

            Offset(offset);
        }
        public void OffsetShoot()
        {
            Vector3 offset = _player._offset_shoot;

            Offset(offset);
        }
        public void OffsetMelee()
        {
            Vector3 offset = _player._offset_melee;

            Offset(offset);
        }
        void Offset(Vector3 offset)
        {
            _offset = offset;

            if (_player._flip == true)
            {
                offset.y *= -1;
            }
            _player._sword_sp.transform.localPosition = offset;
        }
    }
}
