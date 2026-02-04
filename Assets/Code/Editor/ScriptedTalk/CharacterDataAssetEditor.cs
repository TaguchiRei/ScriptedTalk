#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;

[CustomEditor(typeof(CharacterDataAsset))]
public class CharacterDataAssetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();

        if (GUILayout.Button("Character Enum を生成"))
        {
            GenerateEnum((CharacterDataAsset)target);
        }
    }

    private void GenerateEnum(CharacterDataAsset asset)
    {
        string assetName = Path.GetFileNameWithoutExtension(
            AssetDatabase.GetAssetPath(asset));

        StringBuilder code = new StringBuilder();
        code.AppendLine("// 自動生成ファイルの為、手動での編集は上書きされます。");
        code.AppendLine("using System;");
        code.AppendLine($"public enum CharacterName");
        code.AppendLine("{");
        code.AppendLine("    None,");

        foreach (var character in asset.Characters)
        {
            if (string.IsNullOrEmpty(character.Name))
                continue;

            // enum 識別子として最低限安全な形にする
            string enumName = character.Name
                .Replace(" ", "_")
                .Replace("-", "_");

            code.AppendLine($"    {enumName},");
        }

        code.AppendLine("}");

        string outputDir = "Assets/Code/AutoGenerate/CharacterEnums";
        string outputPath = $"{outputDir}/CharacterName{assetName}.cs";

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        File.WriteAllText(outputPath, code.ToString(), Encoding.UTF8);
        AssetDatabase.Refresh();

        Debug.Log($"Character enum generated: {outputPath}");
    }
}
#endif