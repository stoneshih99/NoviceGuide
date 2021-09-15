using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NoviceGuide.Runtime.Scripts.GuideMask
{
    [RequireComponent(typeof(RectGuide), typeof(CircleGuide))]
    public class GuideController : MonoBehaviour, ICanvasRaycastFilter
    {
        public enum GuideType
        {
            Rect,
            Circle
        }

        private CircleGuide _circleGuide;
        private RectGuide _rectGuide;
        [SerializeField] private Material circleMat;
        [SerializeField] private Material rectMat;

        private Image _mask;
        private RectTransform _target;


        private void Awake()
        {
            _mask = GetComponent<Image>();
            if (_mask == null)
            {
                throw new ArgumentNullException(nameof(_mask), "The mask can't is null");
            }

            _circleGuide = GetComponent<CircleGuide>();
            _rectGuide = GetComponent<RectGuide>();
        }

        private void Guide(RectTransform target, GuideType guideType)
        {
            _target = target;
            switch (guideType)
            {
                case GuideType.Rect:
                    _mask.material = rectMat;
                    break;
                case GuideType.Circle:
                    _mask.material = circleMat;
                    break;
            }
        }

        public void Show(Color c, Canvas canvas, RectTransform target, GuideType guideType,
            UnityAction onDone,
            GuideBase.TranslateType translateType)
        {
            Guide(target, guideType);
            switch (guideType)
            {
                case GuideType.Circle:
                    _circleGuide.Execute(c, canvas, target, translateType, onDone);
                    break;
                case GuideType.Rect:
                    _rectGuide.Execute(c, canvas, target, translateType, onDone);
                    break;
            }
        }

        public void DoReset()
        {
            _circleGuide.ResetMask();
            _rectGuide.ResetMask();
        }
        

        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            if (_target == null)
            {
                return false;
            }

            return !RectTransformUtility.RectangleContainsScreenPoint(_target, sp);
        }
        
    }
}