using UnityEngine;

namespace NoviceGuide.Scripts.System.Local
{
    /// <summary>
    /// 檢查初始化時會依據 TaskId 跳轉到對應的場景
    /// </summary>
    public static class NoviceGuideLocalTeleport
    {
         /// <summary>
        /// 開啟對應的場景
        /// </summary>
        /// <param name="taskId"></param>
        public static void Change(NoviceGuideTaskId taskId)
        {
            switch (taskId)
            {
                case NoviceGuideTaskId.Id01:
                    break;
            }
            Debug.Log("開啟場景:"+taskId);
        }
        
    }
}