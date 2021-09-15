using UnityEngine;
using UnityEngine.Events;

namespace NoviceGuide.Runtime.Scripts.System.Story
{
    /// <summary>
    /// 新手指定 StoreBoard 播放器，依據指定的 boardId 進行播放它底下的內容
    /// </summary>
    public class NoviceGuideStoryBoardPlayer : MonoBehaviour
    {
        /// <summary>
        /// 當前的 StoryBoard 全部執行完畢後會觸發
        /// </summary>
        public UnityAction<NoviceGuideTaskId, NoviceGuideTaskId> OnDone;
        
        /// <summary>
        /// 儲存此 MonoBehavior 底下的所擁有的 <see cref="NoviceStoryBoard"/> 
        /// </summary>
        private NoviceStoryBoard[] _noviceStoryBoards;

        /// <summary>
        /// 當前播放的 StoryBoard 
        /// </summary>
        private NoviceStoryBoard _currentStoryBoard;

        private bool _running;
        /// <summary>
        /// 是否正在運行中
        /// </summary>
        public bool Running => _running;

        private NoviceGuideTaskId _nextTaskId;

        private void Awake()
        {
            _noviceStoryBoards = GetComponentsInChildren<NoviceStoryBoard>();
        }

        /// <summary>
        /// 依據 taskId 執行播放 StoryBoard，播到 StoryBoard 裡面的每一個影格都播放結束會執行 <see cref="NoviceStoryBoard.OnDone"/>
        /// <see cref="NoviceGuideTaskId"/>
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="nextTaskId"></param>
        public void Execute(NoviceGuideTaskId taskId, NoviceGuideTaskId nextTaskId)
        {
            var board = Find(taskId);
            if (board == null) return;
            _nextTaskId = nextTaskId;
            _currentStoryBoard = board;
            _currentStoryBoard.Show();
            _currentStoryBoard.Play();
            _currentStoryBoard.OnDone = delegate(NoviceGuideTaskId id)
            {
                OnDone?.Invoke(id, _nextTaskId);
                _running = false;
            };
            _running = true;
            Debug.Log("show store board by taskId:" + taskId);
        }

        
        /// <summary>
        /// 依據編號編號尋找出對應的 StoryBoard 的實體
        /// <see cref="NoviceGuideTaskId"/>
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        private NoviceStoryBoard Find(NoviceGuideTaskId taskId)
        {
            foreach (var board in _noviceStoryBoards)
            {
                if (board.TaskId == taskId) return board;
            }
            return null;
        }
    }
}