public interface IBackGroundView
{
    void SetBackground(string backgroundKey);

    void AnimateBackground(string animationKey);

    void PlayEffect(string effectKey);
}