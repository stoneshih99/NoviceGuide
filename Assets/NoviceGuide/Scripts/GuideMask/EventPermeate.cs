using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NoviceGuide.Scripts.GuideMask
{
    public class EventPermeate: MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    { 
        /// <summary>
        /// 事件穿透的物件
        /// </summary>
        [HideInInspector]
        public GameObject target;
	
        public void OnPointerDown(PointerEventData eventData)
        {
            PassEvent(eventData,ExecuteEvents.pointerDownHandler);
        }
 
        public void OnPointerUp(PointerEventData eventData)
        {
            PassEvent(eventData,ExecuteEvents.pointerUpHandler);
        }
 
        public void OnPointerClick(PointerEventData eventData)
        {
            PassEvent(eventData,ExecuteEvents.submitHandler);
            PassEvent(eventData,ExecuteEvents.pointerClickHandler);
        }

        public void  PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function)
            where T : IEventSystemHandler
        {
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(data, results); 
            foreach (var raycast in results)
            {
                if (target != raycast.gameObject) continue;
                ExecuteEvents.Execute(raycast.gameObject, data,function);
                break;
            }
        }
    }
}