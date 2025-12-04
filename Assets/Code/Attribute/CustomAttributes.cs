using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GamesKeystoneFramework.Attributes
{
    /// <summary>
    /// インスペクター上で閲覧専用にする
    /// </summary>
    public class KeyReadOnlyAttribute : PropertyAttribute
    {
    }

    /// <summary>
    /// インスペクター上で表示非表示を切り替えられるブロックにする
    /// </summary>
    public class KeyGroupingAttribute : PropertyAttribute
    {
    }

    public class StringLengthLimitAttribute : PropertyAttribute
    {
        public int MaxLength { get; private set; }

        public StringLengthLimitAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }
    }

    //エディター専用のアテリビュート
#if UNITY_EDITOR

    #region ReadOnly

    [CustomPropertyDrawer(typeof(KeyReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false; // 編集を無効化
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true; // 元に戻す
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }

    #endregion

    #region Highlight

    [CustomPropertyDrawer(typeof(KeyGroupingAttribute))]
    public class HighlightDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            KeyGroupingAttribute keyGrouping = (KeyGroupingAttribute)attribute;

            // 元のGUIカラーを保存
            Color previousColor = GUI.backgroundColor;

            // 背景カラーを変更
            GUI.backgroundColor = Color.white;

            // ボックス風の背景を描画
            GUI.Box(position, GUIContent.none);

            // GUIカラーを元に戻す
            GUI.backgroundColor = previousColor;

            // 変数のフィールドを描画（ちょっと内側に）
            Rect innerRect = new Rect(position.x + 4, position.y + 2, position.width - 8, position.height - 4);
            EditorGUI.PropertyField(innerRect, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true) + 4;
        }
    }

    #endregion

    #region StringLimiter
    
    [CustomPropertyDrawer(typeof(StringLengthLimitAttribute))]
    public class StringLengthLimiterDrawer : PropertyDrawer
    {
        /// <summary>
        /// Unityのインスペクター上でどのように描画するかを定義するメソッド
        /// </summary>
        /// <param name="position">描画する座標</param>
        /// <param name="property">描画する物本体(stringやint等インスペクターに表示するもの)</param>
        /// <param name="label">あまり考えなくてよし</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //プロパティの中身をstringに限定。stringでなければ代わりにメッセージを出す
            if (property.propertyType == SerializedPropertyType.String)
            {
                //プロパティドロワーのフィールドのattributeをStringLimitAttributeにキャスト
                StringLengthLimitAttribute limit = (StringLengthLimitAttribute)attribute;
                //ここからプロパティフィールドなどのエディタGUIの値が変化したかを調べ、EndChangeCheckで変更された場合のコードをかける
                EditorGUI.BeginChangeCheck();
                
                //一度受け取った入力をキャッシュして下で長さを調べる
                string value = EditorGUI.TextField(position, label, property.stringValue);

                if (value.Length > limit.MaxLength)
                {
                    value = value.Substring(0, limit.MaxLength);
                }

                //値が変更された場合にそれを適用する。実際にインスペクターの値を変えているのはこれ
                if (EditorGUI.EndChangeCheck())
                {
                    property.stringValue = value;
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use StringLengthLimit with string.");
            }
        }
    }

    #endregion

#endif
}