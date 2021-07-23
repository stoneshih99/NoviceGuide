using UnityEditor;

namespace NoviceGuide.Scripts.System.Editor
{
    [CustomEditor(typeof(NoviceGuideExecutor))]
    public class NoviceGuideExecutorEditor : UnityEditor.Editor
    {
        private SerializedProperty _targetRect;
        private SerializedProperty _taskId;
        private SerializedProperty _btn;
        private SerializedProperty _hasNode;
        private SerializedProperty _noviceGuideNode;
        private SerializedProperty _delaySeconds;
        private SerializedProperty _nextTaskId;

        private SerializedProperty _maskColor;

        private void OnEnable()
        {
            _targetRect = serializedObject.FindProperty("targetRect");
            _taskId = serializedObject.FindProperty("taskId");
            _btn = serializedObject.FindProperty("btn");
            _hasNode = serializedObject.FindProperty("hasNode");
            _noviceGuideNode = serializedObject.FindProperty("noviceGuideNode");
            _delaySeconds = serializedObject.FindProperty("delaySeconds");
            _nextTaskId = serializedObject.FindProperty("nextTaskId");
            _maskColor = serializedObject.FindProperty("maskColor");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_targetRect, true);
            EditorGUILayout.PropertyField(_taskId, true);
            EditorGUILayout.PropertyField(_nextTaskId, true);
            EditorGUILayout.PropertyField(_btn, true);
            EditorGUILayout.PropertyField(_hasNode, true);
            EditorGUILayout.PropertyField(_delaySeconds, true);
            EditorGUILayout.PropertyField(_maskColor, true);
            
            if (_hasNode.boolValue)
            {
                EditorGUILayout.PropertyField(_noviceGuideNode, true);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}