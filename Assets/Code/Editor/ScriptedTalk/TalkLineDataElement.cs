using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class TalkLineDataElement : VisualElement
{
    private const float IntFieldWidth = 40f;
    private const float EventsLeftPadding = 30f;
    private const float CharacterFieldWidth = 120f;

    public TalkLineDataElement(SerializedProperty property)
    {
        style.flexDirection = FlexDirection.Column;
        style.marginBottom = 2;

        var textProp = property.FindPropertyRelative("Text");
        var highlightNameProp = property.FindPropertyRelative("HighLightCharacterName");
        var speedProp = property.FindPropertyRelative("TextShowSpeed");
        var eventsProp = property.FindPropertyRelative("Events");

        // ===== 1行目 =====
        var row = new VisualElement
        {
            style =
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center
            }
        };

        // Text
        var textField = new PropertyField(textProp)
        {
            label = ""
        };
        textField.style.flexGrow = 1;
        row.Add(textField);

        // CharacterName (string ←→ enum)
        var characterEnumField = CreateCharacterEnumField(highlightNameProp);
        characterEnumField.style.width = CharacterFieldWidth;
        row.Add(characterEnumField);

        // TextShowSpeed
        var speedField = new PropertyField(speedProp)
        {
            label = ""
        };
        speedField.style.width = IntFieldWidth;
        row.Add(speedField);

        Add(row);

        // ===== 2行目 Events =====
        var eventsField = new PropertyField(eventsProp, "Events");
        eventsField.style.marginLeft = EventsLeftPadding;
        Add(eventsField);
    }

    private EnumField CreateCharacterEnumField(SerializedProperty stringProperty)
    {
        CharacterName currentValue = ParseEnum(stringProperty.stringValue);

        var enumField = new EnumField(currentValue)
        {
            label = ""
        };

        enumField.RegisterValueChangedCallback(evt =>
        {
            var newEnum = (CharacterName)evt.newValue;

            stringProperty.serializedObject.Update();
            stringProperty.stringValue = newEnum.ToString();
            stringProperty.serializedObject.ApplyModifiedProperties();
        });

        return enumField;
    }

    private CharacterName ParseEnum(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return CharacterName.None;
        }

        if (Enum.TryParse(value, out CharacterName result))
        {
            return result;
        }

        return CharacterName.None;
    }
}
