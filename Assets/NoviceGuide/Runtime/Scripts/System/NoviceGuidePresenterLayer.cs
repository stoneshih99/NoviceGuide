using System;
using System.Collections.Generic;
using UnityEngine;

namespace NoviceGuide.Runtime.Scripts.System
{
    /// <summary>
    /// 此別類主要解決在 Canvas 上繪圖深度的問題。
    /// 例如: 壓黑的底圖壓在說明物件上面，在這裡我們可以把說明物件(Target)直接對物件的 Parent 進行做互換，深度也自然就改變
    /// </summary>
    public class NoviceGuidePresenterLayer : MonoBehaviour
    {
        private HashSet<GameObject> _parents;

        private HashSet<GameObject> _clearBuffer;

        private void Awake()
        {
            _parents = new HashSet<GameObject>();
            _clearBuffer = new HashSet<GameObject>();
        }

        /// <summary>
        /// 依據指定的 source 物件將其 parent 儲存到 dict 裡，且將 source.parent 指向目前 GameObject.transform
        /// </summary>
        /// <param name="source"></param>
        public void Attach(GameObject source)
        {
            var scale = source.transform.localScale;
            _parents.Add(source);
            source.transform.SetParent(transform);
            source.transform.localScale = scale;
            source.SetActive(true);
        }

        /// <summary>
        /// 依據指定的 source 物件，從 dict 取回原本的 parent，將它環原。
        /// </summary>
        /// <param name="source"></param>
        public void Detach(GameObject source)
        {
            if (!_parents.Contains(source)) return;

            var scale = source.transform.localScale;
            if (source.transform.parent == null) return;
            
            source.transform.SetParent(FindParent(source).transform);
            source.transform.localScale = scale;
            _clearBuffer.Add(source);
            source.SetActive(false);
        }

        /// <summary>
        /// 將在本身 GameObject 底下的物件都進行清除
        /// </summary>
        public void Clear()
        {
            foreach (var go in _parents)
            {
                Detach(go);
            }

            foreach (var go in _clearBuffer)
            {
                _parents.Remove(go);
            }

            _clearBuffer.Clear();
        }

        /// <summary>
        /// 依據物件實體尋找出儲存在容器裡原本物件的實體
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private GameObject FindParent(GameObject source)
        {
            foreach (var go in _parents)
            {
                if (go.Equals(source)) return go;
            }
            throw new ArgumentOutOfRangeException(nameof(source), "not found");
        }
    }
}