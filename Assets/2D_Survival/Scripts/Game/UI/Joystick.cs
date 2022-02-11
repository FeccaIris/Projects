using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SV
{

    public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        Image _bgImg;
        Image _joystickImg;
        Vector3 _inputVector;

        void Start()
        {
            _bgImg = GetComponent<Image>();
            _joystickImg = transform.GetChild(0).GetComponent<Image>();
        }

        public float GetHorzontal()
        {
            return _inputVector.x;
        }
        public float GetVertical()
        {
            return _inputVector.y;
        }
        public Vector3 GetDirection()
        {
            return _inputVector;
        }

        public virtual void OnDrag(PointerEventData ped)
        {
            RectTransform bg = _bgImg.rectTransform;
            Camera cam = ped.pressEventCamera;
            Vector2 pedPos = ped.position;
            Vector2 pos;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bg, pedPos, cam, out pos))
            {
                pos.x = pos.x / _bgImg.rectTransform.sizeDelta.x * 2;
                pos.y = pos.y / _bgImg.rectTransform.sizeDelta.y * 2;

                _inputVector = new Vector3(pos.x, pos.y, 0);
                _inputVector = _inputVector.normalized;
                //_inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

                float x = _inputVector.x * bg.sizeDelta.x * 0.4f;
                float y = _inputVector.y * bg.sizeDelta.y * 0.4f;

                _joystickImg.rectTransform.anchoredPosition = new Vector3(x, y, 0);
            }
        }
        public virtual void OnPointerDown(PointerEventData ped)
        {
            OnDrag(ped);
        }
        public virtual void OnPointerUp(PointerEventData ped)
        {
            _inputVector = Vector3.zero;
            _joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        }

    }
}
