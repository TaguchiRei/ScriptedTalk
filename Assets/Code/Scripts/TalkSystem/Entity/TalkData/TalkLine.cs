using System;
using System.Numerics;
using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;

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

        public EventEntity[] EventData { get; private set; }

        public TalkLine(string text, int[] highLightCharacterID, EventEntity[] eventData)
        {
            Text = text;
            HighLightCharacterID = highLightCharacterID;
            EventData = eventData;
        }
    }
}