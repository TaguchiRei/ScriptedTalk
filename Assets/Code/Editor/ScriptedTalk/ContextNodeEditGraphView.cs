using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class ContextNodeEditGraphView : GraphView
{
    private ContextData _contextData;

    public ContextNodeEditGraphView(ContextData contextData)
    {
        _contextData = contextData;
        style.flexGrow = 1;
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        Insert(0, new GridBackground());

        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new RectangleSelector());

        // ノード作成リクエスト
        nodeCreationRequest += ctx =>
        {
            var newGroupData = new TalkGroupData();
            // 右クリック位置を保存座標の初期値にする
            newGroupData.Position = contentViewContainer.WorldToLocal(ctx.screenMousePosition);
            _contextData.Context.Add(newGroupData);

            PopulateView(); // データの変更を反映して再描画
        };

        // 削除検知
        graphViewChanged = (changes) =>
        {
            if (changes.elementsToRemove != null)
            {
                foreach (var element in changes.elementsToRemove)
                {
                    if (element is TalkGroupNode node)
                    {
                        _contextData.Context.Remove(node.Data);
                    }
                }
            }

            return changes;
        };

        PopulateView();
    }

    // 接続可能なポートを探す（自分自身も許可）
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(port =>
            startPort != port &&
            startPort.direction != port.direction &&
            startPort.portType == port.portType
        ).ToList();
    }

    public void PopulateView()
    {
        // 一旦全クリア
        graphViewChanged = null; // 削除イベントの連鎖を防ぐ
        DeleteElements(graphElements);
        graphViewChanged = (changes) =>
        {
            if (changes.elementsToRemove != null)
            {
                foreach (var element in changes.elementsToRemove)
                {
                    if (element is TalkGroupNode node) _contextData.Context.Remove(node.Data);
                }
            }

            return changes;
        };

        var nodeDictionary = new Dictionary<string, TalkGroupNode>();

        // 1. ノードの作成
        for (int i = 0; i < _contextData.Context.Count; i++)
        {
            var data = _contextData.Context[i];
            var node = new TalkGroupNode(_contextData, data, i);
            AddElement(node);
            nodeDictionary.Add(data.Guid, node);
        }

        // 2. エッジ（線）の復元
        foreach (var fromNode in nodeDictionary.Values)
        {
            for (int i = 0; i < fromNode.Data.Selections.Count; i++)
            {
                var selection = fromNode.Data.Selections[i];
                if (string.IsNullOrEmpty(selection.NextGroupGuid)) continue;

                if (nodeDictionary.TryGetValue(selection.NextGroupGuid, out var toNode))
                {
                    // インデックスに基づきポートを取得
                    var outputPort = fromNode.outputContainer[i] as Port;
                    var inputPort = toNode.inputContainer[0] as Port;

                    if (outputPort != null && inputPort != null)
                    {
                        var edge = outputPort.ConnectTo(inputPort);
                        AddElement(edge);
                    }
                }
            }
        }
    }
}