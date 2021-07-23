using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace NoviceGuide.Scripts.GuideMask
{
    /// <summary>
    /// 矩形型狀的教學 Mask
    /// </summary>
    public class RectGuide : GuideBase
    {
        private float _width;
        private float _height;

        private float _scaledWidth;
        private float _scaledHeight;
        private static readonly int SliderX = Shader.PropertyToID("_SliderX");
        private static readonly int SliderY = Shader.PropertyToID("_SliderY");

        protected override void Update()
        {
            base.Update();
            if (!Active) return;
            Mat.SetFloat(SliderX, Mathf.Lerp(_scaledWidth, _width, ElapsedTime));
            Mat.SetFloat(SliderY, Mathf.Lerp(_scaledHeight, _height, ElapsedTime));
        }

        protected override void Setup()
        {
            _width = (TargetCorners[3].x - TargetCorners[0].x) / 2 + maskMargin;
            _height = (TargetCorners[1].y - TargetCorners[0].y) / 2 + maskMargin;
            Mat.SetFloat(SliderX, _width);
            Mat.SetFloat(SliderY, _height);
            _scaledWidth = _width * defaultScale;
            _scaledHeight = _height * defaultScale;
        }

        protected override void Done()
        {
            Mat.SetFloat(SliderX, Mathf.Lerp(_scaledWidth, _width, ElapsedTime));
            Mat.SetFloat(SliderY, Mathf.Lerp(_scaledHeight, _height, ElapsedTime));
        }

        public override void ResetMask()
        {
            if (Mat == null) return;
            Mat.SetFloat(SliderX, 1);
            Mat.SetFloat(SliderY, 1);
        }
    }
}