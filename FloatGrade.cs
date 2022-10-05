using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[Serializable]
public class FloatGrade {

	[SerializeField]
	public Difficulty _difficulty;
	[SerializeField]
	public List<ValuePair> _values = new List<ValuePair>();

	/// <summary>
	/// Returns the selected value.
	/// </summary>
	public float Value {
		get {
			CheckAndAdd(_difficulty);
            return GetValue(_difficulty);
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
		CheckAndAdd(difficulty);
        ValuePair pair = _values.Find(p => p.difficulty == difficulty);
		pair.value = value;
        int index = _values.FindIndex(0, _values.Count, p => p.difficulty == difficulty);
        _values[index] = pair;
    } 
	public float GetValue(Difficulty difficulty) {
		CheckAndAdd(difficulty);
        return _values.Where(d => d.difficulty == difficulty).First().value;
    }
	/// <summary>
	/// Change the selected Difficulty.
	/// </summary>
	/// <param name="difficulty"></param>
	public void SetDifficulty(Difficulty difficulty) => _difficulty = difficulty;
	private void CheckAndAdd(Difficulty diff) {
		if(_values == null) {
			_values = new List<ValuePair>();
		}
		if(_values.Exists(p => p.difficulty == diff) == false) {
			_values.Add(new ValuePair() {
				difficulty = diff,
				value = 0
			});
		}
	}
	public void Reset() {
		_values = new List<ValuePair>();
        foreach (Difficulty val in Enum.GetValues(typeof(Difficulty))) {
            _values.Add(new ValuePair() {
                difficulty = val,
                value = 0,
            });
        }
    }

	public FloatGrade(Difficulty diff = 0) {
		_difficulty = diff;
		_values = new List<ValuePair>();

		Reset();
	}
	public static implicit operator float(FloatGrade other) => other.Value;

	[Serializable]
	public struct ValuePair {
		public Difficulty difficulty;
		public float value;
	}
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