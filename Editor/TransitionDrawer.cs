#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Mox.Events.Editor
{
    [CustomPropertyDrawer(typeof(Transition))]
    public class TransitionDrawer : PropertyDrawer
    {
        private const int Lines = 2;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.depth <= 1) position = EditorGUI.PrefixLabel(position, label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var lineHeight = (position.height - EditorGUIUtility.standardVerticalSpacing) / Lines;
            var verticalOffset = lineHeight + EditorGUIUtility.standardVerticalSpacing;
            var fullWidth = position.width;
            var triggerRect = new Rect(position.x, position.y + verticalOffset, fullWidth, lineHeight);
            var targetRect = new Rect(position.x, position.y, fullWidth, lineHeight);
            EditorGUI.PropertyField(triggerRect, property.FindPropertyRelative(Transition.TriggerProperty));
            EditorGUI.PropertyField(targetRect, property.FindPropertyRelative(Transition.TargetProperty));
            
            
            EditorGUI.indentLevel = indent;
        }
    }
}
#endif