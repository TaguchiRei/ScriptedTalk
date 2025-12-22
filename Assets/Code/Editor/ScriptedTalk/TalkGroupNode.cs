using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class TalkGroupNode : Node
{
    private readonly ContextData _contextData;
    private readonly TalkGroupData _groupData;

    private readonly VisualElement _talkLineContainer;

    public TalkGroupNode(ContextData contextData, TalkGroupData groupData)
    {
        _contextData = contextData;
        _groupData = groupData;

        title = "Talk Group";

        _talkLineContainer = new VisualElement();
        _talkLineContainer.style.flexDirection = FlexDirection.Column;
        _talkLineContainer.style.marginTop = 4;

        extensionContainer.Add(_talkLineContainer);
        RefreshExpandedState();
        DrawTalkLines();
    }

    private void DrawTalkLines()
    {
        
    }

    protected void GeneratePorts(
        int count,
        Direction direction,
        Port.Capacity capacity,
        System.Type portType,
        string portNamePrefix
    )
    {
        VisualElement container =
            direction == Direction.Input ? inputContainer : outputContainer;

        container.Clear();

        for (int i = 0; i < count; i++)
        {
            Port port = Port.Create<Edge>(
                Orientation.Horizontal,
                direction,
                capacity,
                portType
            );

            port.portName = $"{portNamePrefix} {i}";
            container.Add(port);
        }

        RefreshPorts();
        RefreshExpandedState();
    }
}