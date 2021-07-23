using NoviceGuide.Scripts.GuideMask;
using NoviceGuide.Scripts.System.Story;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace NoviceGuide.Scripts.System
{
    /// <summary>
    /// 新手導引主要系統，它必須要在獨立的 Canvas 底下且此 Canvas 一定要高於其它 UI Canvas
    /// 
    /// </summary>
    public class NoviceGuideSystem : MonoBehaviour
    {
        public UnityAction<NoviceGuideTaskId, NoviceGuideTaskId> OnDone;

        public const string TagName = "NoviceGuiderSystem";

        [FormerlySerializedAs("storeBoardPlayer")] [SerializeField] private NoviceGuideStoryBoardPlayer storyBoardPlayer;
        [SerializeField] private Canvas rootCanvas;
        [SerializeField] private GuideController guideController;
        [SerializeField] private GuideController.GuideType guideType;
        [SerializeField] private GameObject mainView;
        [SerializeField] private NoviceGuidePresenterLayer presenterLayer;
        public NoviceGuidePresenterLayer PresenterLayer => presenterLayer;

        [Tooltip("哨兵主要是用來檔住在動態過程玩家可以任意點擊")] [SerializeField]
        private GameObject guard;

        private NoviceGuideTaskSequence _taskSequence;

        private NoviceGuideTaskId _currentTaskId;
        public NoviceGuideTaskId CurrentTaskId
        {
            get => _currentTaskId;
            set => _currentTaskId = value;
        }
        /// <summary>
        /// 執行任務時將會上鎖，防之進入下一個行為被加入到序列裡
        /// </summary>
        private bool _blocking;
        
        private void Awake()
        {
            _taskSequence = new NoviceGuideTaskSequence();
            rootCanvas = GetComponentInParent<Canvas>();
        }

        private void Start()
        {
            storyBoardPlayer.OnDone = OnPlayerStoryboardCompleted;
        }

        /// <summary>
        /// 如果當前播放的 StoreBoard 已經播放結束所接收到的通知
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="nextTaskId"></param>
        private void OnPlayerStoryboardCompleted(NoviceGuideTaskId taskId, NoviceGuideTaskId nextTaskId)
        {
            _blocking = false;
            guard.SetActive(false);
            // OnDone?.Invoke(taskId, nextTaskId);
        }

        /// <summary>
        /// 依據 taskId 播放 Storyboard
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="nextTaskId"></param>
        public void PlayStoryboard(NoviceGuideTaskId taskId, NoviceGuideTaskId nextTaskId)
        {
            storyBoardPlayer.Execute(taskId, nextTaskId);
        }

        /// <summary>
        /// 顯示且播放新手導引，在播放的動畫結束之前都將會被 guard 遮住，所有的操作都不能，直到動態結束
        /// </summary>
        public void Show()
        {
            mainView.transform.localPosition = Vector3.zero;
            guard.SetActive(true);
            guideController.DoReset();
        }

        /// <summary>
        /// 關閉新手導，並環原展示層裡面的顯示
        /// </summary>
        public void Hide()
        {
            mainView.transform.localPosition = Vector3.one * 10000;
            guard.SetActive(false);
            guideController.DoReset();
            presenterLayer.Clear();
        }

        /// <summary>
        /// 是否正在運行中 <see cref="NoviceGuideExecutor"/>
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public bool IsRunning(NoviceGuideTaskId taskId)
        {
            if (_blocking) return false;
            if (!_taskSequence.IsSuccessful(taskId)) return false;
            return true;
        }

        /// <summary>
        /// 執行導引 mask 動態
        /// </summary>
        /// <param name="rect">可穿透範圍</param>
        /// <param name="taskId">任務編號</param>
        /// <returns>true=有執行、false=尚未執行</returns>
        public void DoExecutor(Color c, RectTransform rect, NoviceGuideTaskId taskId)
        {
            Debug.Log("The task is running now. taskId:" + taskId);
            guideController.Show(c, rootCanvas, rect, guideType, OnAnimationCompleted, GuideBase.TranslateType.Slow);
            _blocking = true;
            _taskSequence.RemoveFirst();
            _currentTaskId = taskId;
        }

        /// <summary>
        /// 將遮罩動畫結束後會執行此 callback 
        /// </summary>
        private void OnAnimationCompleted()
        {
            if (storyBoardPlayer.Running) return;
            _blocking = false;
            guard.SetActive(false);
            Debug.Log("NoviceGuideSystem::OnAnimationCompleted");
        }

        /// <summary>
        /// 新增任務
        /// </summary>
        /// <param name="taskId"></param>
        public void AddTask(NoviceGuideTaskId taskId)
        {
            if (!CanAddTask(taskId)) return;
            ValidateTask(taskId);
            Debug.Log("AddTask:" + taskId);
            Show();
            _taskSequence.Add(taskId);
        }

        /// <summary>
        /// 檢查是否可以加入任務並執行
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        private bool CanAddTask(NoviceGuideTaskId taskId)
        {
            if (taskId == NoviceGuideTaskId.None) return false;
            if (taskId < _currentTaskId) return false;
            return true;
        }

        /// <summary>
        /// 強置新增任務，首先會進行清除的動作
        /// </summary>
        /// <param name="taskId"></param>
        public void ForceAddTask(NoviceGuideTaskId taskId)
        {
            ValidateTask(taskId);
            _taskSequence.Clear();
            Show();
            _taskSequence.Add(taskId);
        }


        private void ValidateTask(NoviceGuideTaskId taskId)
        {
            if ((int) taskId < (int) _currentTaskId)
            {
                Debug.Log("新手道引任務編不符合規則");
            }
        }
    }
}