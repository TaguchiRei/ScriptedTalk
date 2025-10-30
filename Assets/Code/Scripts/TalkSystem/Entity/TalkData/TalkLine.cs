using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;
using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Text;

namespace ScriptedTalk.TalkSystem.Entity.TalkData
{
    /// <summary>
    /// 会話１行分のデータ
    /// </summary>
    public class TalkLine
    {
        public TextData  TextData { get; private set; }
        public EventData EventData { get; private set; }

        public TalkLine()
        {
            
        }
    }
}
