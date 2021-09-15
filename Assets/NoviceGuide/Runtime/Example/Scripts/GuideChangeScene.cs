using NoviceGuide.Runtime.Scripts.System;
using NoviceGuide.Runtime.Scripts.System.Nodes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NoviceGuide.Runtime.Example.Scripts
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