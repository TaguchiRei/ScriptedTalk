using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TalkLineData))]
public class TalkLineDataDrawer : PropertyDrawer
{
    private const float IntFieldWidth = 40f;
    private const float EventsLeftPadding = 30f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, GUIContent.none, property);

        var textProp = property.FindPropertyRelative("Text");
        var highlightIdProp = property.FindPropertyRelative("HighLightCharacterID");
        var durationProp = property.FindPropertyRelative("TextShowDuration");
        var eventsProp = property.FindPropertyRelative("Events");

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        int oldIndent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // ===== 1行目：Text（可変） + int（40px固定） =====
        float textWidth = position.width - IntFieldWidth * 2;
        Rect textRect = new Rect(position.x, position.y, textWidth, lineHeight);
        Rect highlightRect = new Rect(textRect.xMax, position.y, IntFieldWidth, lineHeight);
        Rect durationRect = new Rect(highlightRect.xMax, position.y, IntFieldWidth, lineHeight);

        EditorGUI.PropertyField(textRect, textProp, GUIContent.none);
        EditorGUI.PropertyField(highlightRect, highlightIdProp, GUIContent.none);
        EditorGUI.PropertyField(durationRect, durationProp, GUIContent.none);

        // ===== 2行目：Events（左に少しスペース） =====
        float eventsHeight = EditorGUI.GetPropertyHeight(eventsProp, true);
        Rect eventsRect = new Rect(position.x + EventsLeftPadding, position.y + lineHeight + spacing, position.width - EventsLeftPadding, eventsHeight);
        EditorGUI.PropertyField(eventsRect, eventsProp, true);

        EditorGUI.indentLevel = oldIndent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var eventsProp = property.FindPropertyRelative("Events");
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;
        float eventsHeight = EditorGUI.GetPropertyHeight(eventsProp, true);

        return lineHeight + spacing + eventsHeight;
    }
}
