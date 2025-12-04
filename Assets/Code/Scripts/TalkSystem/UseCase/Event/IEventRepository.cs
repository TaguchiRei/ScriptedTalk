using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;
using UnityEngine;

namespace ScriptedTalk.TalkSystem.UseCase.Event
{
    public interface IEventRepository
    {
        public string GetAnimation(int animationID);

        public string GetEffect(int effectID);

        public string GetSound(int soundID);

        public string GetBackGround(int backGroundID);
    }
}