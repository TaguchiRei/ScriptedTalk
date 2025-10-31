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
        public int[] CharacterID { get; private set; }

        public Vector3[] CharacterPositions { get; private set; }

        public int TextShowDuration { get; private set; }

        public EventData[] EventData { get; private set; }

        public TalkLine(string text, int[] characterID, Vector3[] characterPositions, EventData[] eventData)
        {
            Text = text;
            CharacterID = characterID;
            CharacterPositions = characterPositions;
            EventData = eventData;
        }
    }
}