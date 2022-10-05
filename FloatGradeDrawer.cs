#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatGrade))]
public class FloatGradePropertyDrawer : PropertyDrawer {

    public bool showExtraProps;
    public readonly static Color focusColor = new Color(1, 0.2f, 0.2f, 0.25f);
    private const float fieldPadding = 20;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);
        var field = (FloatGrade)fieldInfo.GetValue(property.serializedObject.targetObject);

        Rect rectFoldout = new Rect(position.min.x, position.y, position.size.x, EditorGUIUtility.singleLineHeight);
        Rect previewRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, (position.size.x - EditorGUIUtility.labelWidth) / 2f, EditorGUIUtility.singleLineHeight);
        Rect rectEnum = new Rect(previewRect.x + previewRect.width, position.y, previewRect.width, EditorGUIUtility.singleLineHeight);
        Rect lockedRect = new Rect(previewRect.x, previewRect.y, previewRect.width - 20, previewRect.height);
        showExtraProps = EditorGUI.Foldout(rectFoldout, showExtraProps, label);
        field.SetDifficulty((Difficulty)EditorGUI.EnumPopup(rectEnum, field.Difficulty));
        EditorGUI.BeginDisabledGroup(true);
        EditorGUI.FloatField(lockedRect, field.Value);
        EditorGUI.EndDisabledGroup();

        if (showExtraProps) {
            int index = 0;
            float lineHeight = EditorGUIUtility.singleLineHeight * 2 + 5;
            foreach (Difficulty diff in Enum.GetValues(typeof(Difficulty)).OfType<Difficulty>()) {
                Rect diffRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y + lineHeight, previewRect.width - fieldPadding, EditorGUIUtility.singleLineHeight);
                Rect labelRect = new Rect(diffRect.x, diffRect.y - EditorGUIUtility.singleLineHeight, diffRect.width, EditorGUIUtility.singleLineHeight);
                if (index % 2 == 1) {
                    diffRect.x += diffRect.width + fieldPadding;
                    diffRect.width += fieldPadding;
                    labelRect.x += labelRect.width + fieldPadding;
                    labelRect.width += fieldPadding;
                }

                EditorGUI.LabelField(labelRect, diff.ToString());
                field.SetValue(diff, EditorGUI.FloatField(diffRect, field.GetValue(diff)));

                if (diff == field.Difficulty) {
                    EditorGUI.DrawRect(diffRect, focusColor);
                }

                index++;
                if (index % 2 == 0 && index > 0) {
                    lineHeight += EditorGUIUtility.singleLineHeight * 2;
                    index = 0;
                }
            }
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        if (showExtraProps) {
            return EditorGUIUtility.singleLineHeight + EditorGUIUtility.singleLineHeight * (float)Math.Round((double)Enum.GetValues(typeof(Difficulty)).Length / 2, MidpointRounding.AwayFromZero) * 2 + 10;
        } else {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
#endif