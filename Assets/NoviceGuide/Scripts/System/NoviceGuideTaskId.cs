using System.ComponentModel;

namespace NoviceGuide.Scripts.System
{
    public enum NoviceGuideTaskId
    {
        [Description("None")] None,
        [Description("[大廳]指引轉蛋按鈕")] Id01 = 1,
        [Description("[轉蛋商店]單抽轉蛋")] Id02,
        [Description("[轉蛋商店]轉蛋確認訊息(Server 記錄點)")] Id03,
        [Description("[轉蛋商店]開始抽轉蛋")] Id04,
        [Description("[轉蛋商店]轉蛋結束")] Id05,
        [Description("[轉蛋商店]返回大廳")] Id06,
        [Description("[大廳]指引戰鬥(出戰)(如果 Rejoin 必須要額外處理)")] Id07,
        [Description("[戰鬥結算]進入個人統計")] Id08,
        [Description("[戰鬥結算]離開結算")] Id09,
        [Description("[大廳]指引修羅之道")] Id10,
        [Description("[修羅之道]領取燕")] Id11,
        [Description("[修羅之道]返回大廳")] Id12,
        [Description("[大廳]指引選取燕")] Id13,
        [Description("[選角介面]點選燕")] Id14,
        [Description("[角色介面]配置當前選擇的英雄燕")] Id15,
        [Description("第一階級結束點")] EndStage1 = 9000,
    }

    /// <summary>
    /// Lobby Server 記錄點
    /// 新手教學中的每一個斷點，當 Server 送此結點編號過來時將不會執行新手任務教程
    /// 例如: 第一階段結束點(配置當前選擇的英雄燕)。
    /// 如果要新增第二階級的新手教學，起始任務編號將會是 16 而不是廷續 15 繼續往下走
    /// </summary>
    public enum NoviceGuideServerRecordPoint
    {
        [Description("[角色介面]配置當前選擇的英雄燕")] Id15 = 15,
    }


    /// <summary>
    /// Client 操作結束點
    /// </summary>
    public enum NoviceGuideClientRecordPoint
    {
        [Description("None")] None = 0,
        [Description("第一階級結束點")] EndStage1 = 9000,
    }
}