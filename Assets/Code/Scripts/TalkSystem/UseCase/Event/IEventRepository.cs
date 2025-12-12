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

        /// <summary>
        /// エフェクトを取得する
        /// </summary>
        /// <param name="effectID"></param>
        /// <returns></returns>
        public string GetEffect(int effectID);

        /// <summary>
        /// 効果音およびBGMを取得する
        /// </summary>
        /// <param name="soundID"></param>
        /// <param name="volume"></param>
        /// <returns></returns>
        public string GetSound(int soundID, out float volume);

        /// <summary>
        /// 背景画像を取得する
        /// </summary>
        /// <param name="backGroundID"></param>
        /// <returns></returns>
        public string GetBackGround(int backGroundID);
    }
}