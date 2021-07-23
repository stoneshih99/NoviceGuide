using System;
using UnityEngine;

namespace NoviceGuide.Scripts.System.Nodes
{
    /// <summary>
    /// 此節點主要功能是用來驅動導引相關的動畫、描述內容
    /// 例如:
    /// 1、將所有 dotween 進行播放
    /// 2、將 TextField 動態更新文字
    /// </summary>
    public class NoviceGuideNode : MonoBehaviour
    {
        /// <summary>
        /// 圖層
        /// </summary>
        private NoviceGuidePresenterLayer _presenterLayer;

        /// <summary>
        /// 設置事先劃分好的圖層
        /// </summary>
        /// <param name="presenterLayer"></param>
        public void SetNoviceGuidePresenterLayer(NoviceGuidePresenterLayer presenterLayer)
        {
            _presenterLayer = presenterLayer;
        }

        /// <summary>
        /// 當節點被開啟時將會繪出新手導引教學資源(圖、文字)
        /// </summary>
        public void Show()
        {
            transform.localScale = Vector3.one;
            _presenterLayer.Attach(gameObject);
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 當節點被關閉時教學資源(圖、文字)將不會被繪出
        /// </summary>
        public void Hide()
        {
            transform.localScale = Vector3.zero;
            _presenterLayer.Detach(gameObject);
        }
    }
}