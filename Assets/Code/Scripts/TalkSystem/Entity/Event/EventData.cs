using System;

namespace ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event
{
    /// <summary>
    /// イベントのIDとその対象を保持するクラス
    /// </summary>
    [Serializable]
    public class EventData
    {
        public EventType EventType { get; private set; }
        public int EventID { get; private set; }
        public int CharacterID { get; private set; }

        public EventData(EventType eventType, int eventID, int characterID)
        {
            EventType = eventType;
            EventID = eventID;
            CharacterID = characterID;
        }
    }
}