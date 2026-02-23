using UnityEngine;

public interface IBackgroundView
{
    void ChangeBackGround(Sprite sprite);
    void ShowBackGroundTexture(Sprite sprite);

    void HideBackGroundTexture(Sprite sprite);
}