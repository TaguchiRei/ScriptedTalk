using System;

namespace ScriptedTalk.TalkSystem.Entity.TalkData
{
    /// <summary>
    /// 会話１行分のデータ
    /// </summary>
    [Serializable]
    public class TalkLine
    {
        public string Text { get; private set; }
        public int[] HighLightCharacterID { get; private set; }

        public int TextShowDuration { get; private set; }

        public IEvent[] Events { get; private set; }

        public TalkLine(string text, int[] highLightCharacterID, IEvent[] events)
        {
            Text = text;
            HighLightCharacterID = highLightCharacterID;
            Events = events;
        }
    }
}