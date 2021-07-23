using System;
using System.Collections.Generic;
using UnityEngine;

namespace NoviceGuide.Scripts.System
{
    /// <summary>
    /// 儲存任務編號的地方，理論上永遠只會有一筆資料
    /// </summary>
    public class NoviceGuideTaskSequence
    {
        private readonly Queue<NoviceGuideTaskId> _taskIds;

        public NoviceGuideTaskSequence()
        {
            _taskIds = new Queue<NoviceGuideTaskId>();
        }

        /// <summary>
        /// 新增 Queue 到最後面的序列中
        /// </summary>
        /// <param name="taskId"></param>
        public void Add(NoviceGuideTaskId taskId)
        {
            if (_taskIds.Contains(taskId))
            {
                Debug.LogWarning("The taskId already has pushed into the queue.");
            }
            _taskIds.Enqueue(taskId);
        }

        /// <summary>
        /// 移除 queue 第一個元素
        /// </summary>
        public void RemoveFirst()
        {
            _taskIds.Dequeue();
        }
        
        /// <summary>
        /// 檢查 TaskId 是否有符合規則
        /// 1、已經在 Queue 裡面
        /// 2、taskId一定要在 queue 的頭部
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public bool IsSuccessful(NoviceGuideTaskId taskId)
        {
            if (!_taskIds.Contains(taskId)) return false;
            if (_taskIds.Peek() != taskId) return false;
            return true;
        }
        
        /// <summary>
        /// 清空所有在容器內的編號
        /// </summary>
        public void Clear()
        {
            _taskIds.Clear();
        }
    }
}