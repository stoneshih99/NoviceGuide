
using System.ComponentModel;

namespace NoviceGuide.Runtime.Scripts.System
{
    /// <summary>
    /// 編輯事件流程
    /// </summary>
    public enum NoviceGuideTaskId
    {
        None,
        [Description("進入轉蛋")] Id01 = 1,
        [Description("選擇轉蛋機台")] Id02,
        [Description("確定是否付費")] Id03,
        Id04,
        Id05,
        Id06,
        Id07,
        Id08,
        Id09,
        Id10,
    }
}