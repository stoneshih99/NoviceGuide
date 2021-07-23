using System;
using Game.NoviceGuide;
using NoviceGuide.Scripts.System.Constraint;
using NoviceGuide.Scripts.System.Nodes;
using UnityEngine;
using UnityEngine.UI;

namespace NoviceGuide.Scripts.System
{
    /// <summary>
    /// 新手指引執行者，依據執行指定的任務編號(taskId)，當玩家點擊後將會發送下一則任務編號到 NoviceGuideTaskSequence ，
    /// 等待下一個執行者來執行應執行的任務
    /// <see cref="TaskId"/>
    /// <see cref="NoviceGuideTaskSequence"/>
    /// </summary>
    public class NoviceGuideExecutor : MonoBehaviour
    {
     

        /// <summary>
        /// 執行 Execute 之前會先通知好讓 <see cref="NoviceGuideConstraintBase"/> 來決定是否要讓此 script.enabled 
        /// </summary>
        public Func<bool> OnPreExecute;

        [Tooltip("可穿透指定的矩形大小")] [SerializeField]
        private RectTransform targetRect;

        [Tooltip("新手導引編號")] [SerializeField] private NoviceGuideTaskId taskId;
        public NoviceGuideTaskId TaskId => taskId;

        [Tooltip("下一個編號(指向 None 則表示等待到下一次條件觸發，例如:等級到5級後觸發)")] [SerializeField]
        private NoviceGuideTaskId nextTaskId;

        [SerializeField] private Button btn;
        [Tooltip("是否有對話內容")] [SerializeField] private bool hasNode;

        [Tooltip("此內容可以存放跟導引相關的資料")] [SerializeField]
        private NoviceGuideNode noviceGuideNode;

        private NoviceGuideSystem _noviceGuideSystem;

        [Tooltip("廷遲時間")] [SerializeField] private float delaySeconds = 0.2f;

        
        [SerializeField] private Color maskColor = new Color(6, 8, 41, 160);

        private float _elapsedSeconds;
        
        /// <summary>
        /// 是否在執行新手導引中
        /// </summary>
        private bool _active;

        #region UNITY Method
       
        private void Awake()
        {
            if (targetRect == null)
            {
                targetRect = GetComponent<RectTransform>();
            }
        }

        private void Start()
        {
            _active = true;
            _noviceGuideSystem = GameObject.FindWithTag(NoviceGuideSystem.TagName).GetComponent<NoviceGuideSystem>();
            if (hasNode)
                noviceGuideNode.SetNoviceGuidePresenterLayer(_noviceGuideSystem.PresenterLayer);
            HideNode();
        }

        private void Update()
        {
            if (!_active) return;
            if (!IsNext()) return;
            var running = OnPreExecute == null || OnPreExecute.Invoke();
            if (!running) return;
            _elapsedSeconds += Time.deltaTime;
            if (_elapsedSeconds <= delaySeconds) return;
            if (!_noviceGuideSystem.IsRunning(taskId))
                return;
            Execute();
        }
        
        #endregion

        private void Execute()
        {
            _noviceGuideSystem.Show();
            _noviceGuideSystem.PlayStoryboard(taskId, nextTaskId);
            _noviceGuideSystem.DoExecutor(maskColor, targetRect, taskId);
            ShowNode();
            btn.onClick.AddListener(OnClick);
            _active = false;
            _elapsedSeconds = 0f;
        }

        private void OnClick()
        {
            // 表示沒有下一筆了
            _noviceGuideSystem.OnDone?.Invoke(taskId, nextTaskId);
            if (taskId == NoviceGuideTaskId.None ||
                NoviceGuideManager.IsClientEndRecordPoint(nextTaskId))
            {
                _noviceGuideSystem.Hide();
                HideNode();
            }
            else
            {
                Next(nextTaskId);
                // OnClickAction?.Inoke(nextTaskId);
                HideNode();
            }
        }

        private void Next(NoviceGuideTaskId nextId)
        {
            _noviceGuideSystem.AddTask(nextId);
        }

        private void ShowNode()
        {
            if (!hasNode) return;
            noviceGuideNode.Show();
        }

        private void HideNode()
        {
            if (!hasNode) return;
            noviceGuideNode.Hide();
        }

        private bool IsNext()
        {
            return taskId >= _noviceGuideSystem.CurrentTaskId;
        }
    }
}