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
        if (contextData == null)
            return;

        SerializedObject so = new SerializedObject(contextData);
        SerializedProperty contextProp = so.FindProperty("Context");

        if (contextProp == null ||
            !contextProp.isArray ||
            GroupIndex < 0 ||
            GroupIndex >= contextProp.arraySize)
            return;

        SerializedProperty groupProp = contextProp.GetArrayElementAtIndex(GroupIndex);

        size.y = CalculateHeight(groupProp);

        GUIStyle style = isSelected ? EditorStyles.helpBox : GUI.skin.box;

        GUILayout.BeginArea(Rect, style);
        DrawContents(so, groupProp);
        GUILayout.EndArea();

        so.ApplyModifiedProperties();
    }

    private float CalculateHeight(SerializedProperty groupProp)
    {
        float height = 25f;
        float lineHeight =
            EditorGUIUtility.singleLineHeight +
            EditorGUIUtility.standardVerticalSpacing;

        SerializedProperty talkLinesProp = groupProp.FindPropertyRelative("TalkLines");
        SerializedProperty selectionsProp = groupProp.FindPropertyRelative("Selections");

        if (talkLinesProp != null && talkLinesProp.isArray)
            height += talkLinesProp.arraySize * (lineHeight * 3);

        if (selectionsProp != null && selectionsProp.isArray)
            height += selectionsProp.arraySize * lineHeight;

        height += lineHeight * 2;
        return Mathf.Max(height, 80f);
    }

    private void DrawContents(SerializedObject so, SerializedProperty groupProp)
    {
        SerializedProperty talkLinesProp = groupProp.FindPropertyRelative("TalkLines");
        SerializedProperty selectionsProp = groupProp.FindPropertyRelative("Selections");

        // ---------- TalkLines ----------
        if (talkLinesProp != null)
        {
            if (talkLinesProp.arraySize == 0)
            {
                if (GUILayout.Button("Add TalkLine"))
                {
                    Undo.RecordObject(so.targetObject, "Add TalkLine");
                    talkLinesProp.InsertArrayElementAtIndex(0);
                }
            }

            for (int i = 0; i < talkLinesProp.arraySize; i++)
            {
                SerializedProperty lineProp =
                    talkLinesProp.GetArrayElementAtIndex(i);

                EditorGUILayout.BeginVertical("box");

                EditorGUILayout.PropertyField(
                    lineProp.FindPropertyRelative("Text"));

                EditorGUILayout.PropertyField(
                    lineProp.FindPropertyRelative("HighLightCharacterName"));

                EditorGUILayout.PropertyField(
                    lineProp.FindPropertyRelative("TextShowSpeed"));

                SerializedProperty eventsProp =
                    lineProp.FindPropertyRelative("Events");

                if (eventsProp != null && eventsProp.isArray)
                {
                    for (int j = 0; j < eventsProp.arraySize; j++)
                    {
                        SerializedProperty ev =
                            eventsProp.GetArrayElementAtIndex(j);

                        EditorGUILayout.LabelField(
                            $"Event {j}: {ev.managedReferenceValue?.GetType().Name ?? "null"}");
                    }
                }

                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Delete"))
                {
                    Undo.RecordObject(so.targetObject, "Delete TalkLine");
                    talkLinesProp.DeleteArrayElementAtIndex(i);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                    break;
                }

                if (GUILayout.Button("Add Below"))
                {
                    Undo.RecordObject(so.targetObject, "Add TalkLine");
                    talkLinesProp.InsertArrayElementAtIndex(i + 1);
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }

        // ---------- Selections ----------
        if (selectionsProp != null)
        {
            for (int i = 0; i < selectionsProp.arraySize; i++)
            {
                SerializedProperty selProp =
                    selectionsProp.GetArrayElementAtIndex(i);

                EditorGUILayout.BeginHorizontal("box");

                EditorGUILayout.PropertyField(
                    selProp.FindPropertyRelative("SelectionTitle"));

                EditorGUILayout.PropertyField(
                    selProp.FindPropertyRelative("NextGroupGuid"));

                if (GUILayout.Button("Delete"))
                {
                    Undo.RecordObject(so.targetObject, "Delete Selection");
                    selectionsProp.DeleteArrayElementAtIndex(i);
                    EditorGUILayout.EndHorizontal();
                    break;
                }

                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Selection"))
            {
                Undo.RecordObject(so.targetObject, "Add Selection");
                selectionsProp.InsertArrayElementAtIndex(selectionsProp.arraySize);
            }
        }
    }

    public Vector2 GetConnectionPoint() =>
        new Vector2(Rect.xMax, Rect.center.y);

    public Vector2 GetInputPoint() =>
        new Vector2(Rect.xMin, Rect.center.y);

    public void HandleEvent(Event e)
    {
        if (e.type == UnityEngine.EventType.MouseDown && e.button == 0)
        {
            isSelected = Rect.Contains(e.mousePosition);
            GUI.changed = true;
        }

        if (e.type == UnityEngine.EventType.MouseDrag &&
            e.button == 0 &&
            isSelected)
        {
            position += e.delta;
            e.Use();
        }
    }
}