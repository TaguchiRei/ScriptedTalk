using ScriptedTalk.TalkSystem.Entity.TalkData;

namespace ScriptedTalk.TalkSystem.UseCase.TextBox
{
    /// <summary>
    /// 会話内容を取得するためのインターフェース。データベースからの取得を行うクラスに付与、そこで会話状況の保持も行う
    /// </summary>
    public interface ITalkRepository
    {
        TalkGroup GetTalkGroup(int groupNumber);
    }
}