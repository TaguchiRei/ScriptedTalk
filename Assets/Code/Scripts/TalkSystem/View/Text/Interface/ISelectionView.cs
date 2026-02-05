using System;

public  interface ISelectionView
{
    public void ShowSelection(string[] selections, Action<int> selectCallback);
}