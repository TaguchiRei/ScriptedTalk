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

        DrawContents();

        RefreshExpandedState();
        style.minWidth = 500;
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
        DrawTalkLinesOnly();

        // Selections描画
        DrawSelections();
    }

    private void DrawTalkLinesOnly()
    {
        _talkLinesContainer.Clear();
        _serializedContext.Update();

        for (int i = 0; i < _talkLinesProperty.arraySize; i++)
        {
            var prop = _talkLinesProperty.GetArrayElementAtIndex(i);
            var element = new TalkLineDataElement(prop); // 既存のElementクラス
            element.Bind(_serializedContext);
            _talkLinesContainer.Add(element);
        }

        _talkLinesContainer.Add(new Button(() => {
            _talkLinesProperty.InsertArrayElementAtIndex(_talkLinesProperty.arraySize);
            _serializedContext.ApplyModifiedProperties();
            DrawTalkLinesOnly();
        }) { text = "Add TalkLine" });
    }

    private void DrawSelections()
    {
        if (_selectionContainer != null) _talkLineContainer.Remove(_selectionContainer);
        _selectionContainer = new VisualElement();
        _talkLineContainer.Add(_selectionContainer);

        for (int i = 0; i < Data.Selections.Count; i++)
        {
            var sel = Data.Selections[i];
            var row = new VisualElement { style = { flexDirection = FlexDirection.Row } };
            
            var tf = new TextField { value = sel.SelectionTitle, style = { flexGrow = 1 } };
            int idx = i;
            tf.RegisterValueChangedCallback(evt => {
                sel.SelectionTitle = evt.newValue;
                (outputContainer[idx] as Port).portName = evt.newValue;
            });

            row.Add(tf);
            _selectionContainer.Add(row);

            if (i >= outputContainer.childCount) AddOutputPort(sel.SelectionTitle);
        }

        _selectionContainer.Add(new Button(() => {
            Data.Selections.Add(new SelectionData { SelectionTitle = "New Selection" });
            DrawSelections();
        }) { text = "Add Selection" });
    }

    private void AddOutputPort(string portName)
    {
        Port port = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(TalkGroupNode));
        port.AddManipulator(new EdgeConnector<Edge>(_connectorListener));
        port.portName = portName;
        outputContainer.Add(port);
        RefreshPorts();
    }

    private void AddInputPort()
    {
        Port port = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(TalkGroupNode));
        port.AddManipulator(new EdgeConnector<Edge>(_connectorListener));
        port.portName = "In";
        inputContainer.Add(port);
        RefreshPorts();
    }
}