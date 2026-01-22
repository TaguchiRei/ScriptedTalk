using UnityEditor;
using UnityEngine;

public class ContextNodeEditorWindow : EditorWindow
{
    private ContextData currentContext;

    private Vector2 canvasScroll;
    private Vector2 canvasOffset;

    [MenuItem("ScriptedTalk/Context Node Editor")]
    public static void Open()
    {
        GetWindow<ContextNodeEditorWindow>("Context Node Editor");
    }

    private void OnEnable()
    {
        canvasScroll = Vector2.zero;
        canvasOffset = Vector2.zero;
    }

    private void OnGUI()
    {
        DrawToolbar();
        DrawCanvas();
        HandleEvents(Event.current);
    }

    private void DrawToolbar()
    {
        using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
        {
            currentContext = (ContextData)EditorGUILayout.ObjectField(
                currentContext,
                typeof(ContextData),
                false,
                GUILayout.Width(300)
            );

            GUILayout.FlexibleSpace();
        }
    }

    private void DrawCanvas()
    {
        Rect canvasRect = new Rect(
            0,
            EditorStyles.toolbar.fixedHeight,
            position.width,
            position.height - EditorStyles.toolbar.fixedHeight
        );

        GUILayout.BeginArea(canvasRect);
        canvasScroll = EditorGUILayout.BeginScrollView(canvasScroll);

        DrawBackgroundGrid();

        // ※ 現段階ではノード描画は一切行わない
        // ここは「構造の投影領域」としてのみ存在する

        EditorGUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private void DrawBackgroundGrid()
    {
        Rect rect = GUILayoutUtility.GetRect(
            position.width * 2,
            position.height * 2
        );

        Handles.BeginGUI();
        Handles.color = new Color(0.2f, 0.2f, 0.2f, 1f);

        const float gridSpacing = 20f;

        for (float x = rect.x; x < rect.width; x += gridSpacing)
        {
            Handles.DrawLine(
                new Vector3(x, rect.y),
                new Vector3(x, rect.height)
            );
        }

        for (float y = rect.y; y < rect.height; y += gridSpacing)
        {
            Handles.DrawLine(
                new Vector3(rect.x, y),
                new Vector3(rect.width, y)
            );
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    private void HandleEvents(Event e)
    {
        if (e.type == UnityEngine.EventType.MouseDrag && e.button == 2)
        {
            canvasOffset += e.delta;
            Repaint();
        }
    }
}