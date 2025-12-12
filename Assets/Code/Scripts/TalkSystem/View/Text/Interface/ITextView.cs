public interface ITextView
{
    public bool TextAnimation { get; }

    public void ShowFullText(string text);

    public void AnimationText(string text);

    public void SkipAnimation();
}