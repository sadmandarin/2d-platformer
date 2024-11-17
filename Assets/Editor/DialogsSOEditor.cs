using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(DialogsSO))]
public class DialogsSOEditor : Editor
{
    private ReorderableList _dialogsList;

    private void OnEnable()
    {
        // Инициализация ReorderableList для списка Dialogs
        _dialogsList = new ReorderableList(serializedObject,
            serializedObject.FindProperty("Dialogs"),
            true, true, true, true);

        _dialogsList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            SerializedProperty element = _dialogsList.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("SpeakerName"), new GUIContent("Speaker"));

            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Line"), new GUIContent("Line"));

            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("HasQuest"), new GUIContent("Has Quest")); // Добавлено поле _hasQuest

            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            SerializedProperty hasChoicesProp = element.FindPropertyRelative("HasChoices");
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                hasChoicesProp, new GUIContent("Has Choices"));

            if (hasChoicesProp.boolValue)
            {
                rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Choices"), new GUIContent("Choices"), true);

                rect.y += EditorGUI.GetPropertyHeight(element.FindPropertyRelative("Choices"), true) + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("NextDialogs"), new GUIContent("Next Dialog Indexes"), true);
            }
        };

        _dialogsList.elementHeightCallback = (index) =>
        {
            var element = _dialogsList.serializedProperty.GetArrayElementAtIndex(index);
            float height = EditorGUIUtility.singleLineHeight * 4 + EditorGUIUtility.standardVerticalSpacing * 3; // Увеличили базовую высоту

            if (element.FindPropertyRelative("HasChoices").boolValue)
            {
                height += EditorGUI.GetPropertyHeight(element.FindPropertyRelative("Choices"), true) +
                          EditorGUIUtility.standardVerticalSpacing;
                height += EditorGUI.GetPropertyHeight(element.FindPropertyRelative("NextDialogs"), true) +
                          EditorGUIUtility.standardVerticalSpacing;
            }

            return height;
        };
    }

    public override void OnInspectorGUI()
    {
        // Обновление сериализованного объекта
        serializedObject.Update();

        // Отображаем ReorderableList
        _dialogsList.DoLayoutList();

        // Применяем изменения
        serializedObject.ApplyModifiedProperties();
    }
}