using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class FloatGrade {

	private Difficulty _difficulty;
	private readonly Dictionary<Difficulty, float> _values;

	/// <summary>
	/// Returns the selected value.
	/// </summary>
	public float Value {
		get {
			if(_values.ContainsKey(_difficulty)) {
				return _values[_difficulty];
			}
			Debug.LogWarning("No value found for: " + _difficulty);
			return 0;
		}
	}
	public Difficulty Difficulty => _difficulty;
	public int IntValue => (int)Value;

	/// <summary>
	/// Changes the corresponding value of the difficulty.
	/// </summary>
	/// <param name="difficulty"></param>
	/// <param name="value"></param>
	public void SetValue(Difficulty difficulty, float value) {
		if (_values.ContainsKey(difficulty)) {
			_values[difficulty] = value;
		}
		Debug.LogWarning("No value found for: " + _difficulty);
	} 
	public float GetValue(Difficulty difficulty) {
		if (_values.ContainsKey(difficulty)) {
			return _values[difficulty];
		}
		Debug.LogWarning("No value found for: " + _difficulty);
		return 0;
	}
	/// <summary>
	/// Change the selected Difficulty.
	/// </summary>
	/// <param name="difficulty"></param>
	public void SetDifficulty(Difficulty difficulty) => _difficulty = difficulty;

	public FloatGrade(Difficulty diff = 0) {
		_difficulty = diff;
		_values = new Dictionary<Difficulty, float>();

		foreach(Difficulty val in Enum.GetValues(typeof(Difficulty))) {
			_values.Add(val, 0);
		}
	}

	public static implicit operator float(FloatGrade other) => other.Value;
}

public static class DifficultyExtension {
	/// <summary>
	/// Set all FloatGrade structs in this class to the given Difficulty. <para></para> This is a shortcut instead of calling every field and changing the difficulty manually.
	/// </summary>
	/// <param name="mono"></param>
	/// <param name="difficulty"></param>
	public static void SetDifficulty(this UnityEngine.Object mono, Difficulty difficulty) {
		var type = mono.GetType();
		foreach (var v in type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)) {
			if (v.FieldType == typeof(FloatGrade)) {
				var field = (FloatGrade)v.GetValue(mono);
				field.SetDifficulty(difficulty);
				v.SetValue(mono, field);
			}
		}
	}
}