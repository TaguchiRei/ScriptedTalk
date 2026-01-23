using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EdgeConnectorListener : IEdgeConnectorListener
{
    public void OnDrop(GraphView graphView, Edge edge)
    {
        var fromNode = edge.output.node as TalkGroupNode;
        var toNode = edge.input.node as TalkGroupNode;

        if (fromNode != null && toNode != null)
        {
            int outIndex = fromNode.outputContainer.IndexOf(edge.output);
            if (outIndex >= 0 && outIndex < fromNode.Data.Selections.Count)
            {
                // Guidを保存
                fromNode.Data.Selections[outIndex].NextGroupGuid = toNode.Data.Guid;

                graphView.AddElement(edge);
                edge.input.Connect(edge);
                edge.output.Connect(edge);
                
                UnityEditor.EditorUtility.SetDirty(fromNode.ContextData);
            }
        }
    }

    public void OnDropOutsidePort(Edge edge, Vector2 position) { }
}