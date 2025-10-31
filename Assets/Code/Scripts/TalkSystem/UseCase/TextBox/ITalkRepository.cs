using ScriptedTalk.TalkSystem.Entity.TalkData;

namespace ScriptedTalk.TalkSystem.UseCase.TextBox
{
    /// <summary>
    /// 会話状況の保持をするためのインターフェース。
    /// </summary>
    public interface ITalkRepository
    {
        TalkGroup GetTalkGroup(int groupNumber);
    }
}