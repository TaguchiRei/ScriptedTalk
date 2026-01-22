using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class TalkGroupNode : Node
{
    private readonly ContextData _contextData;
    private readonly int _groupIndex;

    private SerializedObject _serializedContext;
    private SerializedProperty _talkLinesProperty;

    private VisualElement _talkLineContainer;

    public TalkGroupNode(ContextData contextData, int groupIndex)
    {
        _contextData = contextData;
        _groupIndex = groupIndex;

        title = $"Talk Group {_groupIndex}";

        _serializedContext = new SerializedObject(_contextData);

        SerializedProperty contextList =
            _serializedContext.FindProperty("Context");

        SerializedProperty groupProperty =
            contextList.GetArrayElementAtIndex(_groupIndex);

        _talkLinesProperty =
            groupProperty.FindPropertyRelative("TalkLines");

        _talkLineContainer = new VisualElement();
        _talkLineContainer.style.flexDirection = FlexDirection.Column;
        _talkLineContainer.style.marginTop = 4;

        extensionContainer.Add(_talkLineContainer);

        // ★ 先にUIを構築
        DrawTalkLines();
        //DrawTalkGroup();

        // ★ 最後に必ず呼ぶ
        RefreshExpandedState();
        style.minWidth = 500;
        style.minHeight = 180;
    }

    private void DrawTalkLines()
    {
        _talkLineContainer.Clear();
        _serializedContext.Update();

        for (int i = 0; i < _talkLinesProperty.arraySize; i++)
        {
            SerializedProperty element =
                _talkLinesProperty.GetArrayElementAtIndex(i);

            VisualElement talkLineElement =
                CreateTalkLineElement(element);

            _talkLineContainer.Add(talkLineElement);
        }
    }


    private void DrawTalkGroup()
    {
        _talkLineContainer.Clear();

        _serializedContext.Update();


        SerializedProperty selectionProp =
            _serializedContext.FindProperty("Selections");

        PropertyField selectionField =
            new PropertyField(selectionProp);

        selectionField.Bind(_serializedContext);
        _talkLineContainer.Add(selectionField);
    }

    private VisualElement CreateTalkLineElement(SerializedProperty talkLineProp)
    {
        VisualElement root = new VisualElement();
        root.style.flexDirection = FlexDirection.Column;
        root.style.marginBottom = 6;

        SerializedProperty textProp =
            talkLineProp.FindPropertyRelative("Text");

        SerializedProperty highlightIdProp =
            talkLineProp.FindPropertyRelative("HighgLightCharactorId");

        SerializedProperty durationProp =
            talkLineProp.FindPropertyRelative("TextShowDuration");

        SerializedProperty eventsProp =
            talkLineProp.FindPropertyRelative("Events");

        root.Add(new PropertyField(textProp));
        root.Add(new PropertyField(highlightIdProp));
        root.Add(new PropertyField(durationProp));
        root.Add(new PropertyField(eventsProp));

        return root;
    }


    protected void GeneratePorts(
        int count,
        Direction direction,
        Port.Capacity capacity,
        Type portType,
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