using NoviceGuide.Runtime.Scripts.System;
using UnityEngine;

namespace NoviceGuide.Runtime.Example.Scripts
{
    public class GuideDemo : MonoBehaviour
    {
        [SerializeField] private NoviceGuideSystem guideSystem = null;

        private void Start()
        {
            guideSystem.AddTask(NoviceGuideTaskId.Id01);
        }
    }
}