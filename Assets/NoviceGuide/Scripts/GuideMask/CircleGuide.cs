using UnityEngine;

namespace NoviceGuide.Scripts.GuideMask
{
    /// <summary>
    /// 圓型狀的教學 Mask
    /// </summary>
    public class CircleGuide : GuideBase
    {
        /// <summary>
        /// 半徑
        /// </summary>
        private float _radius;

        /// <summary>
        /// 變化後的半徑大小
        /// </summary>
        private float _scaleRadius;

        private static readonly int Slider = Shader.PropertyToID("_Slider");


        protected override void Setup()
        {
            var width = (TargetCorners[3].x - TargetCorners[0].x) / 2;
            var height = (TargetCorners[1].y - TargetCorners[0].y) / 2;
            _radius = Mathf.Sqrt(width * width + height * height) + maskMargin;
            _scaleRadius = _radius * defaultScale;
            Mat.SetFloat(Slider, _radius);
        }

        protected override void Update()
        {
            base.Update();
            if (!Active) return;
            Mat.SetFloat(Slider, Mathf.Lerp(_scaleRadius, _radius, ElapsedTime));
        }
        
        protected override void Done()
        {
            Mat.SetFloat(Slider, Mathf.Lerp(_scaleRadius, _radius, ElapsedTime));
        }
        
        public override void ResetMask()
        {
            if (Mat == null) return;
            Mat.SetFloat(Slider, 1);
        }

    }
}