public interface ITextView
{
    public bool TextAnimation { get; }

    public void ShowFullText(string text);

    public void AnimationText(string text, int duration);

    public void SkipAnimation();

    public void ShowTextBox();

    public void HideTextBox();
}