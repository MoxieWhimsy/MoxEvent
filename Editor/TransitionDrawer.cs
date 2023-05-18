using UnityEditor;
using UnityEngine;

namespace Mox.Events.Editor
{
    [CustomPropertyDrawer(typeof(Transition))]
    public class TransitionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.depth == 0) position = EditorGUI.PrefixLabel(position, label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var halfWidth = position.width / 2;
            var triggerRect = new Rect(position.x, position.y, halfWidth, position.height);
            var targetRect = new Rect(position.x + halfWidth, position.y, halfWidth, position.height);
            EditorGUI.PropertyField(triggerRect, property.FindPropertyRelative(Transition.TriggerProperty));
            EditorGUI.PropertyField(targetRect, property.FindPropertyRelative(Transition.TargetProperty));
            
            
            EditorGUI.indentLevel = indent;
        }
    }
}