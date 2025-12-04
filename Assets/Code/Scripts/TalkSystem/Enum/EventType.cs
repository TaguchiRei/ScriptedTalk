public enum EventType
{
    /// <summary> キャラクターを表示する </summary>
    ShowCharacter,

    /// <summary> キャラクターを隠す </summary>
    HideCharacter,

    /// <summary> キャラクターをハイライトする </summary>
    HighlightCharacter,

    /// <summary> キャラクターをアニメーションさせる </summary>
    AnimateCharacter,
    
    /// <summary> キャラクターを動かす </summary>
    MoveCharacter,

    /// <summary> 背景をアニメーションさせる </summary>
    AnimateBackground,

    /// <summary> 音を鳴らす </summary>
    PlaySound,

    /// <summary> エフェクトを出す </summary>
    PlayEffect,

    /// <summary> 背景を変更する </summary>
    ChangeBackground
}