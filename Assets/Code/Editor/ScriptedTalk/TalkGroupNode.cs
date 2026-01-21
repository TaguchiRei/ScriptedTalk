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

        // SerializedObject を ContextData で作る（重要）
        _serializedContext = new SerializedObject(_contextData);

        // context[groupIndex].talkLines を取得
        SerializedProperty contextList =
            _serializedContext.FindProperty("context");

        SerializedProperty groupProperty =
            contextList.GetArrayElementAtIndex(_groupIndex);

        _talkLinesProperty =
            groupProperty.FindPropertyRelative("talkLines");

        // UI 構築
        _talkLineContainer = new VisualElement();
        _talkLineContainer.style.flexDirection = FlexDirection.Column;
        _talkLineContainer.style.marginTop = 4;

        extensionContainer.Add(_talkLineContainer);
        RefreshExpandedState();

        DrawTalkLines();
    }

    private void DrawTalkLines()
    {
        _talkLineContainer.Clear();

        _serializedContext.Update();

        for (int i = 0; i < _talkLinesProperty.arraySize; i++)
        {
            int index = i;

            SerializedProperty lineProperty =
                _talkLinesProperty.GetArrayElementAtIndex(index);

            SerializedProperty textProperty =
                lineProperty.FindPropertyRelative("Text");

            VisualElement row = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;
            row.style.marginBottom = 2;

            TextField textField = new TextField();
            textField.style.flexGrow = 1;

            // Inspector と同じバインディング
            textField.BindProperty(textProperty);

            Button removeButton = new Button(() =>
            {
                _serializedContext.Update();
                _talkLinesProperty.DeleteArrayElementAtIndex(index);
                _serializedContext.ApplyModifiedProperties();
                DrawTalkLines();
            })
            {
                text = "-"
            };

            row.Add(textField);
            row.Add(removeButton);
            _talkLineContainer.Add(row);
        }

        Button addButton = new Button(() =>
        {
            _serializedContext.Update();

            int newIndex = _talkLinesProperty.arraySize;
            _talkLinesProperty.InsertArrayElementAtIndex(newIndex);

            SerializedProperty newLine =
                _talkLinesProperty.GetArrayElementAtIndex(newIndex);

            newLine.FindPropertyRelative("Text").stringValue = string.Empty;
            newLine.FindPropertyRelative("HighLightCharacterID").intValue = 0;
            newLine.FindPropertyRelative("TextShowDuration").intValue = 0;

            _serializedContext.ApplyModifiedProperties();
            DrawTalkLines();
        })
        {
            text = "Add Talk Line"
        };

        addButton.style.marginTop = 4;
        _talkLineContainer.Add(addButton);
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
