using ScriptedTalk.TalkSystem.Entity.TalkData;

namespace ScriptedTalk.TalkSystem.UseCase.TextBox
{
    /// <summary>
    /// 会話状況の保持をするためのインターフェース
    /// </summary>
    public interface IContextCache
    {
        public int GroupNumber { get; }
        public int LineNumber { get; }
        public Context GetContext();
        public void SetContext(Context context);
    }
}