using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NoviceGuide.Scripts.System.Story
{
    /// <summary>
    /// StoryBoard 本身是多個影格的集合體，並控制進行播放
    /// Frame 每一個影格都可以是一頁的說明圖、文字...等等
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class NoviceStoryBoard : MonoBehaviour
    {
        public UnityAction<NoviceGuideTaskId> OnDone;
        [Tooltip("指定對應的任務編號")] [SerializeField] private NoviceGuideTaskId taskId;
        public NoviceGuideTaskId TaskId => taskId;

        [Tooltip("說明圖、文字...等等")] [SerializeField] private GameObject[] frames;
        
        /// <summary>
        /// 按鈕:下一頁
        /// </summary>
        private Button _btnClick;
        
        /// <summary>
        /// 目前播放的影格進度
        /// </summary>
        private int _currentFrame;

        private void Start()
        {
            foreach (var go in frames)
            {
                go.transform.localPosition = Vector3.zero;
                go.SetActive(false);
            }
            _btnClick = GetComponent<Button>();
            _btnClick.onClick.AddListener(Next);
            Hide();
        }

        /// <summary>
        /// 執行播放當前影格
        /// <see cref="_currentFrame"/> 當前影格 
        /// </summary>
        public void Play()
        {
            var go = frames[_currentFrame];
            go.SetActive(true);
            Debug.Log("NoviceStoryboard::Play");
        }

        /// <summary>
        /// 關閉 Storyboard 
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
            Debug.Log("NoviceStoryboard::Hide");
        }

        /// <summary>
        /// 顯示 Storyboard
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
            Debug.Log("NoviceStoryboard::Show");
        }

        /// <summary>
        /// 進行播放下一個影格，直到沒有下一影格將會執行 <see cref="OnDone"/> 提供給外部得知已經播完了
        /// </summary>
        private void Next()
        {
            var oldGo = frames[_currentFrame];
            oldGo.SetActive(false);
            if (IsDone())
            {
                Hide();
                OnDone?.Invoke(taskId);
                return;
            }
            _currentFrame++;
            var newGo = frames[_currentFrame];
            newGo.SetActive(true);
        }

     
        /// <summary>
        /// 是否已經播完了
        /// </summary>
        /// <returns></returns>
        private bool IsDone()
        {
            return _currentFrame >= frames.Length - 1;
        }
    }
}