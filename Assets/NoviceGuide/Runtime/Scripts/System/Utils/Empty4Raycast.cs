using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NoviceGuide.Runtime.Scripts.System.Utils
{
    public class Empty4Raycast : MaskableGraphic , IPointerClickHandler
    {
        public Action onClick;
        
        protected Empty4Raycast()
        {
            useLegacyMeshGeneration = false;
        }

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            toFill.Clear();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke();
        }
    }
}