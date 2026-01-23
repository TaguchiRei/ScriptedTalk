using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TalkGroupNodeView
{
    public int GroupIndex { get; private set; }
    private Vector2 position;
    private Vector2 size = new Vector2(200f, 80f);

    private bool isSelected;
    public bool IsSelected => isSelected;

    public Rect Rect => new Rect(position, size);

    public TalkGroupNodeView(int groupIndex, Vector2 startPosition)
    {
        GroupIndex = groupIndex;
        position = startPosition;
    }

    public void Draw(ContextData contextData)
    {
        if (contextData == null || GroupIndex < 0 || GroupIndex >= contextData.Context.Count)
            return;

        size.y = CalculateHeight(contextData.Context[GroupIndex]);

        GUIStyle style = isSelected ? EditorStyles.helpBox : GUI.skin.box;

        GUILayout.BeginArea(Rect, style);
        DrawContents(contextData.Context[GroupIndex], contextData);
        GUILayout.EndArea();
    }

    private float CalculateHeight(TalkGroupData data)
    {
        float height = 25f;
        float lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        if (data.TalkLines != null)
            height += data.TalkLines.Length * (lineHeight * 3); // TalkLine分の高さ

        if (data.Selections != null)
            height += data.Selections.Count * lineHeight;

        height += lineHeight * 2; // Addボタン分
        return Mathf.Max(height, 80f);
    }

    public void DrawContents(TalkGroupData data, ContextData contextData)
    {
        if (data == null) return;

        float lineHeight = EditorGUIUtility.singleLineHeight;

        // --- TalkLines ---
        if (data.TalkLines == null)
            data.TalkLines = new TalkLineData[0];

        // 「TalkLineがないとき用の Add ボタン」
        if (data.TalkLines.Length == 0)
        {
            if (GUILayout.Button("Add TalkLine"))
            {
                data.TalkLines = new TalkLineData[] { new TalkLineData() };
            }
        }

        for (int i = 0; i < data.TalkLines.Length; i++)
        {
            var line = data.TalkLines[i];
            bool removeLine = false;

            EditorGUILayout.BeginVertical("box");

            Undo.RecordObject(contextData, "Edit TalkLine");

            line.Text = EditorGUILayout.TextField("Text", line.Text);
            line.HighLightCharacterID = EditorGUILayout.IntField("HighLightCharacterID", line.HighLightCharacterID);
            line.TextShowDuration = EditorGUILayout.IntField("TextShowDuration", line.TextShowDuration);

            // Events 表示
            if (line.Events != null)
            {
                for (int j = 0; j < line.Events.Length; j++)
                {
                    EditorGUILayout.LabelField($"Event {j}: {line.Events[j]?.GetType().Name ?? "null"}");
                }
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Delete"))
                removeLine = true;

            if (GUILayout.Button("Add Below"))
            {
                var temp = new List<TalkLineData>(data.TalkLines);
                temp.Insert(i + 1, new TalkLineData());
                data.TalkLines = temp.ToArray();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            if (removeLine)
            {
                var temp = new List<TalkLineData>(data.TalkLines);
                temp.RemoveAt(i);
                data.TalkLines = temp.ToArray();
                i--;
            }
        }

        // --- Selections ---
        if (data.Selections == null)
            data.Selections = new List<SelectionData>();

        for (int i = 0; i < data.Selections.Count; i++)
        {
            var sel = data.Selections[i];
            bool removeSel = false;

            EditorGUILayout.BeginHorizontal("box");
            Undo.RecordObject(contextData, "Edit Selection");

            sel.SelectionTitle = EditorGUILayout.TextField("Title", sel.SelectionTitle);
            sel.NextGroupGuid = EditorGUILayout.TextField("NextGroupGuid", sel.NextGroupGuid);

            if (GUILayout.Button("Delete"))
                removeSel = true;

            EditorGUILayout.EndHorizontal();

            if (removeSel)
            {
                data.Selections.RemoveAt(i);
                i--;
            }
        }

        if (GUILayout.Button("Add Selection"))
            data.Selections.Add(new SelectionData());
    }


    public Vector2 GetConnectionPoint() => new Vector2(Rect.xMax, Rect.center.y);
    public Vector2 GetInputPoint() => new Vector2(Rect.xMin, Rect.center.y);

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