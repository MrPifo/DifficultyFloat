# Dynamic Difficulty based Float
This repository contains 2 Unity scripts.

## FloatGrade.cs
FloatGrade is a struct class that holds a dictionary filled with one value for each defined Difficulty.
```FloatGrade.Value``` returns the currently selected difficulty float value.
Use ```FloatGrade.SetDifficulty(difficulty)``` to change the selected difficulty.
Use the extension method from ```DifficultyExtension``` and call ```SetDifficulty(this UnityEngine.Object mono, Difficulty difficulty)``` on any Object that contains
a FloatGrade field. Afterwards every FloatGrade field will be automaticially changed to the new difficulty, which makes it easier to adjust all FloatGrades at the same time.

### Example of a collaped Field: <br />
![Collapsed Property](https://sperlich.at/assets/pictures/FloatGrade_preview_1.png?raw=true) <br />

### Example of an expanded Field: <br />
![Expanded Property](https://sperlich.at/assets/pictures/FloatGrade_preview_2.png?raw=true) <br />
The current selected difficulty will be highlighted in color. You change it with the dropdown right next to it.
The disabled field shows what Value would be returned at the moment.

## Difficulty.cs
Contains the different available Difficulties. Add, Remove or Change as many Difficulties as you like. The inspector will automaticially adapt to it. Although be sure to use Refactoring instead of manually changing it, otherwise it may break existing code of yours. <br />
![Enum](https://sperlich.at/assets/pictures/FloatGrade_preview_3.png?raw=true)
