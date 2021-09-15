using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NoviceGuide.Runtime.Scripts.GuideMask
{
    [RequireComponent(typeof(Image))]
    public abstract class GuideBase : MonoBehaviour
    {
        // 中心點位移的方式
        public enum TranslateType
        {
            Direct,
            Slow
        }

        [Tooltip("邊框大小")] [SerializeField] protected float maskMargin = 0f;
        
        [Tooltip("動態持續時間")] [Range(0f, 3f)] [SerializeField]
        private float duration = 0f;

        [Tooltip("預設縮放比例")] [SerializeField] protected float defaultScale = 0f;

        protected Material Mat;

        private Vector3 _center;

        /// <summary>
        /// 準備顯示目標物的四個角落
        /// </summary>
        protected readonly Vector3[] TargetCorners = new Vector3[4];

        private float _elapsedTime;

        protected float ElapsedTime
        {
            get
            {
                _elapsedTime = Mathf.Clamp(_elapsedTime, 0f, 1f);
                return _elapsedTime;
            }
        }

        private Vector3 _startCenter;
        private UnityAction _onDone;

        protected bool Active;
        private static readonly int CenterPropertyId = Shader.PropertyToID("_Center");

        /// <summary>
        /// 
        /// <example>
        /// distance = speed * time;
        /// 1 = speed * duration => speed = 1 / duration
        /// </example>
        /// </summary>
        protected virtual void Update()
        {
            if (!Active) return;
            var speed = 1 / duration;
            _elapsedTime += Time.deltaTime * speed;
            var v = Vector3.Lerp(_startCenter, _center, ElapsedTime);
            Mat.SetVector(CenterPropertyId, v);
            if (_elapsedTime < 1.0f) return;
            Done();
            DoReset();
            _onDone?.Invoke();
        }

        private void DoReset()
        {
            Active = false;
            _elapsedTime = 0f;
        }

        protected abstract void Done();

        public abstract void ResetMask();

        public void Execute(Color c, Canvas canvas, RectTransform target, TranslateType translateType, UnityAction onDone)
        {
            _onDone = onDone;
            var img = transform.GetComponent<Image>();
            Mat = img.material;
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target), "The target can't is null");
            }
            target.GetWorldCorners(TargetCorners);
            img.color = c;
            for (int i = 0; i < TargetCorners.Length; i++)
            {
                TargetCorners[i] = WorldToScreenPoint(canvas, TargetCorners[i]);
            }
            _center.x = TargetCorners[0].x + (TargetCorners[3].x - TargetCorners[0].x) / 2;
            _center.y = TargetCorners[0].y + (TargetCorners[1].y - TargetCorners[0].y) / 2;
            switch (translateType)
            {
                case TranslateType.Direct:
                    Mat.SetVector(CenterPropertyId, _center);
                    break;
                case TranslateType.Slow:
                    _startCenter = Mat.GetVector(CenterPropertyId);
                    break;
            }
            Active = true;
            Setup();
        }

        protected abstract void Setup();

        /// <summary>
        /// 將世界座標轉換成 Screen Space 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="world"></param>
        /// <returns></returns>
        private Vector2 WorldToScreenPoint(Canvas canvas, Vector3 world)
        {
            var screenPoint = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, world);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPoint,
                canvas.worldCamera, out var localPoint);
            return localPoint;
        }
        
    }
}