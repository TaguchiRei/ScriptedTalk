using ScriptedTalk.Code.Scripts.TalkSystem.Entity.Event;
using UnityEngine;

namespace ScriptedTalk.TalkSystem.UseCase.Event
{
    public interface IEventRepository
    {
        /// <summary>
        /// 背景アニメーションを取得
        /// </summary>
        /// <param name="animationID"></param>
        /// <returns></returns>
        public string GetAnimation(int animationID);

        public string GetEffect(int effectID);

        public string GetSound(int soundID);

        public string GetBackGround(int backGroundID);
    }
}