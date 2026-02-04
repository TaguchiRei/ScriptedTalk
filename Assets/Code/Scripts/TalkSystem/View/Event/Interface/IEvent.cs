using System;

/// <summary>
/// イベントの実行の実装を書くクラスが実装するインターフェース
/// </summary>
public interface IEvent
{
    public Action EndAction { get; set; }
    public void Execute();

    public void Skip();
}