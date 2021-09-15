using NoviceGuide.Runtime.Scripts.System.Nodes;
using UnityEngine;

namespace NoviceGuide.Runtime.Scripts.System.Constraint
{
    /// <summary>
    /// 主要功能是用來約束 NoviceGuideExecutor 是否可以執行
    /// <example>
    /// 場景有多隻英雄的項選可以選，但我們只要選擇其中編號為 100 的英雄，此時只有編號 100 的英雄
    /// <see cref="OnConditional"/> 會回傳 true，其餘都將回傳 false
    /// </example>
    /// </summary>
    [RequireComponent(typeof(NoviceGuideExecutor))]
    public abstract class NoviceGuideConstraintBase : MonoBehaviour
    {
        [Tooltip("對指定的任務編號才有所反應")] [SerializeField] protected NoviceGuideTaskId taskId;
        private NoviceGuideExecutor _executor;
        
        protected virtual void Awake()
        {
            _executor = GetComponent<NoviceGuideExecutor>();

            _executor.OnPreExecute = OnConditional;
        }
        
        /// <summary>
        /// 當指定的條件返回 True 才會執行 <see cref="NoviceGuideExecutor"/>
        /// </summary>
        /// <returns></returns>
        private bool OnConditional()
        {
            var result = taskId == _executor.TaskId;
            if (!result)
            {
                return false;
            }
            return IsRunning();
        }
        
        protected abstract bool IsRunning();
    }
}