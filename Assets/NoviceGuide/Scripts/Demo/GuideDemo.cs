using NoviceGuide.Scripts.System;
using UnityEngine;

namespace NoviceGuide.Scripts.Demo
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