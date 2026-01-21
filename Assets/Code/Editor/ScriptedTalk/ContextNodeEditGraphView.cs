using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class ContextNodeEditGraphView : GraphView
{
    public ContextNodeEditGraphView(ContextData contextData)
    {
        style.flexGrow = 1;
        style.flexShrink = 1;

        // ズーム倍率の上限設定
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        // 背景の設定
        Insert(0, new GridBackground());
        // UIElements上でのドラッグ操作などの検知
        this.AddManipulator(new SelectionDragger());
        // イベントの設定、中身は後述
        SetEvents(contextData);
    }

    private void SetEvents(ContextData contextData)
    {
        // SampleGraphViewのメニュー周りのイベントを設定する処理
        nodeCreationRequest += context =>
        {
            var newGroupData = new TalkGroupData();
            contextData.context.Add(newGroupData);
            // 後述するNodeというクラスのインスタンス生成
            var node = new TalkGroupNode(contextData, contextData.context.Count - 1);
            // GraphViewの子要素として追加する
            AddElement(node);
        };
    }
}