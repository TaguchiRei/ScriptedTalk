using UnityEngine;
using UnityEditor;

public class TalkGroupNodeView
{
    public int GroupIndex { get; private set; }

    private Vector2 position;
    private readonly Vector2 size = new Vector2(200f, 80f);

    private bool isSelected;

    public Rect Rect => new Rect(position, size);

    public TalkGroupNodeView(int groupIndex, Vector2 startPosition)
    {
        GroupIndex = groupIndex;
        position = startPosition;
    }

    public void Draw(ContextData context)
    {
        if (context == null ||
            GroupIndex < 0 ||
            GroupIndex >= context.Context.Count)
            return;

        GUIStyle style = isSelected
            ? EditorStyles.helpBox
            : GUI.skin.box;

        GUILayout.BeginArea(Rect, style);
        DrawContents(context.Context[GroupIndex]);
        GUILayout.EndArea();
    }

    private void DrawContents(TalkGroupData data)
    {
        EditorGUILayout.LabelField(
            $"Group {GroupIndex}",
            EditorStyles.boldLabel
        );

        if (data.TalkLines != null && data.TalkLines.Length > 0)
        {
            EditorGUILayout.LabelField(
                data.TalkLines[0].Text,
                EditorStyles.wordWrappedLabel
            );
        }
        else
        {
            EditorGUILayout.LabelField("(No TalkLines)");
        }

        if (data.Selections == null || data.Selections.Count == 0)
        {
            EditorGUILayout.LabelField("End", EditorStyles.miniLabel);
        }
    }

    public void HandleEvent(Event e)
    {
        if (e.type == UnityEngine.EventType.MouseDown && e.button == 0)
        {
            isSelected = Rect.Contains(e.mousePosition);
            GUI.changed = true;
        }

        if (e.type == UnityEngine.EventType.MouseDrag && e.button == 0 && isSelected)
        {
            position += e.delta;
            e.Use();
        }
    }
}