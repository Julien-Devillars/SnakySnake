using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(EnnemiesGenerator))]
public class EnnemiesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //The target variable is the selected MyBehaviour.
        EnnemiesGenerator ennemies_generator = (EnnemiesGenerator)target;

        ennemies_generator.number_ennemies = EditorGUILayout.IntField("Number of Ennemies", ennemies_generator.number_ennemies);

        displayEnnemyDirection(ennemies_generator);
        displayEnnemyPosition(ennemies_generator);
        //ennemies_generator.default_position = EditorGUILayout.Toggle("Default Position", ennemies_generator.default_position);

    }
    private void displayEnnemyDirection(EnnemiesGenerator ennemies_generator)
    {
        ennemies_generator.random_direction = EditorGUILayout.Toggle("Random Direction", ennemies_generator.random_direction);
        if (!ennemies_generator.random_direction)
        {
            List<Vector2> tmp = new List<Vector2>(ennemies_generator.ennemies_direction);
            ennemies_generator.ennemies_direction.Clear();

            for (int i = 0; i < ennemies_generator.number_ennemies; ++i)
            {
                if (i < tmp.Count)
                {
                    ennemies_generator.ennemies_direction.Add(tmp[i]);
                }
                else
                {
                    ennemies_generator.ennemies_direction.Add(new Vector2(0, 0));
                }
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("ennemies_direction"), true);
        }
    }
    private void displayEnnemyPosition(EnnemiesGenerator ennemies_generator)
    {
        ennemies_generator.default_position = EditorGUILayout.Toggle("Default Position", ennemies_generator.default_position);
        if(ennemies_generator.default_position)
        {
            return;
        }
        ennemies_generator.random_position = EditorGUILayout.Toggle("Random Position", ennemies_generator.random_position);
        if (ennemies_generator.random_position)
        {
            return;
        }

        List<Vector2> tmp = new List<Vector2>(ennemies_generator.ennemies_position);
        ennemies_generator.ennemies_position.Clear();

        for (int i = 0; i < ennemies_generator.number_ennemies; ++i)
        {
            if (i < tmp.Count)
            {
                ennemies_generator.ennemies_position.Add(tmp[i]);
            }
            else
            {
                ennemies_generator.ennemies_position.Add(new Vector2(0, 0));
            }
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("ennemies_position"), true);
        
    }
}
