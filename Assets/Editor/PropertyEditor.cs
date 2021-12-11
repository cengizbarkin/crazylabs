using UnityEditor;
using UnityEngine;
using Utils;

namespace Editor
{
    [CustomEditor(typeof(Properties))]
    public class PropertyEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var properties = (Properties)target;

            EditorGUILayout.LabelField("Initial Drop Spawn Count");
            properties.initialDropSpawnCount = 
                EditorGUILayout.IntSlider(properties.initialDropSpawnCount, 50, 200);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            EditorGUILayout.HelpBox("A drop does not have to touch to selected drop in order to chose. It should be close enough. " +
                                    "This property is used for distance in between same color drops to chose", MessageType.Info);
            EditorGUILayout.LabelField("Max distance to select a drop");
            properties.maxDistanceToSelectDrop = 
                EditorGUILayout.Slider(properties.maxDistanceToSelectDrop, 0.3f, 2.0f);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            
            EditorGUILayout.HelpBox("Each drop has same amount of mass for the time being",MessageType.Info);
            EditorGUILayout.LabelField("Mass for each drop");
            properties.massForEachDrop = 
                EditorGUILayout.Slider(properties.massForEachDrop, 0.1f, 2.0f);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            
            EditorGUILayout.HelpBox("Each drop has same PhysicsMaterial2D component. The components are added in run-time.",MessageType.Info);
            EditorGUILayout.LabelField("Friction for each drop");
            properties.frictionValue = 
                EditorGUILayout.Slider(properties.frictionValue, 0.1f, 2.0f);
            EditorGUILayout.LabelField("Bounciness for each drop");
            properties.bouncinessValue = 
                EditorGUILayout.Slider(properties.bouncinessValue, 0.0f, 3.0f);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            
            EditorGUILayout.HelpBox("Each drop has random velocity towards -Y direction. " +
                                    "Velocity is given to each drop after every spawn in run-time randomly." +
                                    "There is a Min value and Max value for Y and X direction of the velocity",MessageType.Info);
            
            EditorGUILayout.LabelField("Min value of Y direction of the Velocity");
            properties.minVelocityInY = 
                EditorGUILayout.Slider(properties.minVelocityInY, 0.1f, 5.0f);
            
            EditorGUILayout.LabelField("Max value of Y direction of the Velocity");
            properties.maxVelocityInY = 
                EditorGUILayout.Slider(properties.maxVelocityInY, 5.0f, 20.0f);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            
            
            
            if (GUILayout.Button("Reset Values"))
            {
                properties.ResetValues();
            }
           
        }
    }
}

