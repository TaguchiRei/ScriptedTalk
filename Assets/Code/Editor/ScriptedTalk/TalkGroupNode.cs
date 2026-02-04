using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class TalkGroupNode : Node
{
    public readonly ContextData ContextData;
    public readonly TalkGroupData Data;
    public readonly int GroupIndex;
    private IEdgeConnectorListener _connectorListener;

    private SerializedObject _serializedContext;
    private SerializedProperty _talkLinesProperty;

    private VisualElement _talkLineContainer;
    private VisualElement _talkLinesContainer;
    private VisualElement _selectionContainer;

    public TalkGroupNode(ContextData contextData, TalkGroupData data, int groupIndex)
    {
        ContextData = contextData;
        Data = data;
        GroupIndex = groupIndex;

        _connectorListener = new EdgeConnectorListener();

        title = $"Talk Group {GroupIndex}";
        viewDataKey = Data.Guid;

        // 保存された位置へ移動
        SetPosition(new Rect(Data.Position, Vector2.zero));

        AddInputPort();

        _serializedContext = new SerializedObject(ContextData);
        _talkLinesProperty = _serializedContext.FindProperty("Context")
            .GetArrayElementAtIndex(GroupIndex).FindPropertyRelative("TalkLines");

        _talkLineContainer = new VisualElement();
        _talkLineContainer.style.flexDirection = FlexDirection.Column;
        _talkLineContainer.style.marginTop = 4;
        extensionContainer.Add(_talkLineContainer);

        var setStartButton = new Button(() =>
            {
                ContextData.StartGroupGuid = Data.Guid;
                EditorUtility.SetDirty(ContextData);
                UpdateStartNodeVisual();
            })
            { text = "Set as Start Group" };
        _talkLineContainer.Add(setStartButton);

        DrawContents();

        RefreshExpandedState();
        style.minWidth = 500;
        style.maxWidth = 1000;
    }


    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        if (Data != null) Data.Position = newPos.position;
    }

    private void DrawContents()
    {
        // TalkLines描画
        if (_talkLinesContainer == null)
        {
            _talkLinesContainer = new VisualElement();
            _talkLineContainer.Add(_talkLinesContainer);
        }

        DrawTalkLines();

        // Selections描画
        DrawSelections();

        UpdateStartNodeVisual();
    }

    private void DrawTalkLines()
    {
        _talkLinesContainer.Clear();
        _serializedContext.Update();

        // ★ラベル追加
        _talkLinesContainer.Add(new Label("Talk Lines")
            { style = { unityFontStyleAndWeight = FontStyle.Bold, marginBottom = 4 } });

        for (int i = 0; i < _talkLinesProperty.arraySize; i++)
        {
            int index = i;

            var prop = _talkLinesProperty.GetArrayElementAtIndex(index);

            var row = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;
            row.style.alignItems = Align.Center;

            var element = new TalkLineDataElement(prop);
            element.style.flexGrow = 1;
            element.Bind(_serializedContext);

            var controlArea = new VisualElement();
            controlArea.style.flexDirection = FlexDirection.Row;
            controlArea.style.marginLeft = 4;

            var addDeleteArea = new VisualElement();
            addDeleteArea.style.flexDirection = FlexDirection.Column;
            addDeleteArea.style.justifyContent = Justify.Center;

            var addButton = new Button(() =>
            {
                _talkLinesProperty.InsertArrayElementAtIndex(index + 1);
                _serializedContext.ApplyModifiedProperties();
                DrawTalkLines();
            }) { text = "+" };

            var deleteButton = new Button(() =>
            {
                _talkLinesProperty.DeleteArrayElementAtIndex(index);
                _serializedContext.ApplyModifiedProperties();
                DrawTalkLines();
            }) { text = "−" };

            var moveArea = new VisualElement();
            moveArea.style.flexDirection = FlexDirection.Column;
            moveArea.style.justifyContent = Justify.Center;
            moveArea.style.marginLeft = 2;

            var upButton = new Button(() =>
            {
                if (index <= 0) return;

                _talkLinesProperty.MoveArrayElement(index, index - 1);
                _serializedContext.ApplyModifiedProperties();
                DrawTalkLines();
            }) { text = "↑" };

            var downButton = new Button(() =>
            {
                if (index >= _talkLinesProperty.arraySize - 1) return;

                _talkLinesProperty.MoveArrayElement(index, index + 1);
                _serializedContext.ApplyModifiedProperties();
                DrawTalkLines();
            }) { text = "↓" };

            foreach (var b in new[] { addButton, deleteButton, upButton, downButton })
                b.style.width = 24;

            addDeleteArea.Add(deleteButton);
            addDeleteArea.Add(addButton);

            moveArea.Add(upButton);
            moveArea.Add(downButton);

            controlArea.Add(addDeleteArea);
            controlArea.Add(moveArea);

            row.Add(element);
            row.Add(controlArea);

            _talkLinesContainer.Add(row);
        }

        Data.TalkLines ??= Array.Empty<TalkLineData>();

        if (Data.TalkLines.Length == 0)
        {
            _talkLinesContainer.Add(new Button(() =>
            {
                _talkLinesProperty.InsertArrayElementAtIndex(_talkLinesProperty.arraySize);
                _serializedContext.ApplyModifiedProperties();
                DrawTalkLines();
            }) { text = "Add TalkLine" });
        }
    }


    private void DrawSelections()
    {
        if (_selectionContainer != null) _talkLineContainer.Remove(_selectionContainer);
        _selectionContainer = new VisualElement();
        _talkLineContainer.Add(_selectionContainer);

        if (Data == null) return;

        // ★ラベル追加
        _selectionContainer.Add(new Label("Selections")
            { style = { unityFontStyleAndWeight = FontStyle.Bold, marginBottom = 4 } });

        for (int i = 0; i < Data.Selections.Count; i++)
        {
            var sel = Data.Selections[i];
            var row = new VisualElement { style = { flexDirection = FlexDirection.Row, alignItems = Align.Center } };

            var tf = new TextField { value = sel.SelectionTitle, style = { flexGrow = 1 } };
            int idx = i;
            tf.RegisterValueChangedCallback(evt =>
            {
                sel.SelectionTitle = evt.newValue;
                if (idx < outputContainer.childCount)
                    (outputContainer[idx] as Port).portName = evt.newValue;
            });

            var deleteButton = new Button(() =>
                {
                    if (idx < outputContainer.childCount)
                        outputContainer.Remove(outputContainer[idx]);

                    Data.Selections.RemoveAt(idx);
                    DrawSelections();
                })
                { text = "×" };
            deleteButton.style.width = 24;
            deleteButton.style.marginLeft = 4;

            row.Add(tf);
            row.Add(deleteButton);
            _selectionContainer.Add(row);

            if (i >= outputContainer.childCount) AddOutputPort(sel.SelectionTitle);
        }

        _selectionContainer.Add(new Button(() =>
        {
            Data.Selections.Add(new SelectionData { SelectionTitle = "New Selection" });
            DrawSelections();
        }) { text = "Add Selection" });
    }


    private void AddOutputPort(string portName)
    {
        Port port = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
            typeof(TalkGroupNode));
        port.AddManipulator(new EdgeConnector<Edge>(_connectorListener));
        port.portName = portName;
        outputContainer.Add(port);
        RefreshPorts();
    }

    private void AddInputPort()
    {
        Port port = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi,
            typeof(TalkGroupNode));
        port.AddManipulator(new EdgeConnector<Edge>(_connectorListener));
        port.portName = "In";
        inputContainer.Add(port);
        RefreshPorts();
    }

    private void UpdateStartNodeVisual()
    {
        if (ContextData.StartGroupGuid == Data.Guid)
        {
            style.backgroundColor = Color.gray3;
        }
        else
        {
            style.backgroundColor = Color.gray2;
        }
    }
}