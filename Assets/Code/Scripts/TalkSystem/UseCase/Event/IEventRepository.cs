using System.Numerics;
using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;
using ScriptedTalk.TalkSystem.Entity.Character;

namespace ScriptedTalk.TalkSystem.UseCase.Event
{
    public interface IEventRepository
    {
        public Vector3 GetCharacterPosition(EventData eventData);

        public string GetAnimationClip(EventData eventData);

        public CharacterData GetCharacter(EventData eventData);
    }
}