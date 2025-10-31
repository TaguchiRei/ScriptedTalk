using ScriptedTalk.TalkSystem.Entity.TalkData;

namespace ScriptedTalk.TalkSystem.UseCase.TextBox
{
    public interface IContextCache
    {
        public Context GetContext();
        public void SetContext(Context context);
    }
}