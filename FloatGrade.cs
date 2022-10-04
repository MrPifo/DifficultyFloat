using System;
using UnityEditor;
using UnityEngine;
using static CampaignV1;

[Serializable]
public struct FloatGrade {

	/// <summary>
	/// Normal Difficulty is treated as default
	/// </summary>
	[SerializeField]
	private Difficulty _difficulty;

	[SerializeField]
	private float value;

	[SerializeField]
	private float easyOverride;
	[SerializeField]
	private float hardOverride;
	[SerializeField]
	private float originalOverride;

	/// <summary>
	/// Current value is determined by the current difficulty
	/// <para>Difficulty.Medium is treated as default</para>
	/// </summary>
	[SerializeField]
	public float Value {
		get {
			switch(_difficulty) {
				case Difficulty.Easy:
					return easyOverride == 0 ? value : easyOverride;
				case Difficulty.Hard:
					return hardOverride == 0 ? value : hardOverride;
				case Difficulty.Extreme:
					return originalOverride == 0 ? value : originalOverride;
			}
			return value;
		}
		set => this.value = value;
	}
	public int IntValue => (int)Value;

	public void SetDifficulty(Difficulty difficulty) => _difficulty = difficulty;

	public FloatGrade(Difficulty diff = Difficulty.Normal) {
		value = 0;
		_difficulty = diff;
		easyOverride = 0;
		hardOverride = 0;
		originalOverride = 0;
	}
	public FloatGrade(float value, Difficulty diff = Difficulty.Normal) {
		this.value = value;
		_difficulty = diff;
		easyOverride = 0;
		hardOverride = 0;
		originalOverride = 0;
	}

	public static implicit operator float(FloatGrade other) => other.Value;
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(FloatGrade))]
public class FloatGradePropertyDrawer : PropertyDrawer {

	public bool showExtraProps;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.BeginProperty(position, label, property);

		// Draw label
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
		position.width -= 75;

		// Calculate rects
		var baseValueRect = new Rect(position.x - 15, position.y, position.width, 20);
		var easyRect = new Rect(position.x, position.y + 45, position.width, 20);
		var hardRect = new Rect(position.x, position.y + 67, position.width, 20);
		var originalRect = new Rect(position.x, position.y + 89, position.width, 20);

		showExtraProps = EditorGUI.Foldout(baseValueRect, showExtraProps, GUIContent.none);
		baseValueRect.x += 15;
		EditorGUI.PropertyField(baseValueRect, property.FindPropertyRelative("value"), GUIContent.none);

		if(showExtraProps) {
			EditorGUI.indentLevel += 4;
			EditorGUI.LabelField(new Rect(easyRect.x - 50, easyRect.y, easyRect.width, easyRect.height), "Easy");
			EditorGUI.LabelField(new Rect(hardRect.x - 50, hardRect.y, hardRect.width, hardRect.height), "Hard");
			EditorGUI.LabelField(new Rect(originalRect.x - 50, originalRect.y, originalRect.width, originalRect.height), "Original");
			EditorGUI.LabelField(new Rect(easyRect.x - 50, easyRect.y - 25, easyRect.width, easyRect.height), "Current:  " + ((Difficulty)property.FindPropertyRelative("_difficulty").enumValueIndex));
			EditorGUI.PropertyField(easyRect, property.FindPropertyRelative("easyOverride"), GUIContent.none);
			EditorGUI.PropertyField(hardRect, property.FindPropertyRelative("hardOverride"), GUIContent.none);
			EditorGUI.PropertyField(originalRect, property.FindPropertyRelative("originalOverride"), GUIContent.none);
		}
		EditorGUI.EndProperty();
		EditorGUI.indentLevel = 0;
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		if(showExtraProps) {
			return 115;
		} else {
			return 20;
		}
	}
}
#endif