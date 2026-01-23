using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class TalkLineDataElement : VisualElement
{
    private const float IntFieldWidth = 40f;
    private const float EventsLeftPadding = 30f;

    public TalkLineDataElement(SerializedProperty property)
    {
        style.flexDirection = FlexDirection.Column;
        style.marginBottom = 2;

        var textProp = property.FindPropertyRelative("Text");
        var highlightIdProp = property.FindPropertyRelative("HighLightCharacterID");
        var durationProp = property.FindPropertyRelative("TextShowDuration");
        var eventsProp = property.FindPropertyRelative("Events");

        // ===== 1行目: Text + HighLightCharacterID + TextShowDuration =====
        var row = new VisualElement();
        row.style.flexDirection = FlexDirection.Row;
        row.style.alignItems = Align.Center;

        // Text（残り幅）
        var textField = new PropertyField(textProp);
        textField.style.flexGrow = 1;
        textField.label = ""; // ラベル非表示
        row.Add(textField);

        // HighLightCharacterID（固定幅、ラベル非表示）
        var highlightField = new PropertyField(highlightIdProp);
        highlightField.style.width = IntFieldWidth;
        highlightField.label = ""; // ラベルなし
        row.Add(highlightField);

        // TextShowDuration（固定幅、ラベル非表示）
        var durationField = new PropertyField(durationProp);
        durationField.style.width = IntFieldWidth;
        durationField.label = ""; // ラベルなし
        row.Add(durationField);

        Add(row);

        // ===== 2行目: Events（左にインデント） =====
        var eventsField = new PropertyField(eventsProp, "Events");
        eventsField.style.marginLeft = EventsLeftPadding;
        Add(eventsField);
    }
}