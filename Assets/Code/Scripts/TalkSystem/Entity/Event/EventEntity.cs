using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event
{
    /// <summary>
    /// イベントのIDとその対象を保持するクラス
    /// </summary>
    [Serializable]
    public class EventEntity
    {
        public EventType EventType { get; private set; }

        /// <summary>
        /// EventTypeに対応するイベントのID。背景など画像ならそのID
        /// </summary>
        public int EventID { get; private set; }

        public int CharacterID { get; private set; }

        public EventEntity(EventType eventType, int eventID, int characterID)
        {
            EventType = eventType;
            EventID = eventID;
            CharacterID = characterID;
        }
    }
}