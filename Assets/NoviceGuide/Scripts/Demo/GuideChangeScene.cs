using System;
using NoviceGuide.Scripts.System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NoviceGuide.Scripts.Demo
{
    public class GuideChangeScene : MonoBehaviour
    {
        [SerializeField] private NoviceGuideExecutor guideExecutor;
        [SerializeField] private string sceneName;

        private void Start()
        {
            guideExecutor.OnClickAction = delegate(NoviceGuideTaskId taskId)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            };
        }
    }
}