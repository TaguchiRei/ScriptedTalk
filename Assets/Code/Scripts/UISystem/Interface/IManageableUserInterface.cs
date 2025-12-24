using UnityEngine.Events;

public interface IManageableUserInterface
{
    bool Enabled { get; }
    void EnableUi();
    void DisableUi();
    void HideUi();
    void ShowUi();
}