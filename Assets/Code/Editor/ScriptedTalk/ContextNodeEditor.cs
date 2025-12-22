using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ContextNodeEditor : EditorWindow
{
    private ContextNodeEditGraphView _graphView;
    private ContextData _contextData;

    private ObjectField _contextDataField;
    private Label _emptyLabel;

    [MenuItem("Window/ScriptedTalk/ContextNodeEditor")]
    public static void ShowWindow()
    {
        GetWindow<ContextNodeEditor>("ContextNodeEditor");
    }

    private void OnEnable()
    {
        CreateRootUI();
    }

    private void CreateRootUI()
    {
        rootVisualElement.Clear();

        _contextDataField = new ObjectField("Context Data")
        {
            objectType = typeof(ContextData),
            allowSceneObjects = false
        };

        _contextDataField.RegisterValueChangedCallback(evt =>
        {
            _contextData = evt.newValue as ContextData;
            OnContextDataChanged();
        });

        rootVisualElement.Add(_contextDataField);

        _emptyLabel = new Label("ContextData を指定するとグラフが表示されます。");
        _emptyLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
        _emptyLabel.style.flexGrow = 1;

        rootVisualElement.Add(_emptyLabel);
    }

    private void OnContextDataChanged()
    {
        if (_graphView != null)
        {
            rootVisualElement.Remove(_graphView);
            _graphView = null;
        }

        if (_contextData == null)
        {
            _emptyLabel.style.display = DisplayStyle.Flex;
            return;
        }

        _emptyLabel.style.display = DisplayStyle.None;

        _graphView = new ContextNodeEditGraphView(_contextData);
        _graphView.StretchToParentSize();

        rootVisualElement.Add(_graphView);
    }
}