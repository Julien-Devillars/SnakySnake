using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*

[CustomEditor(typeof(EnemiesGenerator))]
public class EnemiesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //The target variable is the selected MyBehaviour.
        EnemiesGenerator enemies_generator = (EnemiesGenerator)target;

        enemies_generator.number_enemies = EditorGUILayout.IntField("Number of Enemies", enemies_generator.number_enemies);

        displayEnemyDirection(enemies_generator);
        displayEnemyPosition(enemies_generator);
    }
    private void displayEnemyDirection(EnemiesGenerator enemies_generator)
    {
        enemies_generator.random_direction = EditorGUILayout.Toggle("Random Direction", enemies_generator.random_direction);
        if (!enemies_generator.random_direction)
        {
            List<Vector2> tmp = new List<Vector2>(enemies_generator.enemies_direction);
            enemies_generator.enemies_direction.Clear();

            for (int i = 0; i < enemies_generator.number_enemies; ++i)
            {
                if (i < tmp.Count)
                {
                    enemies_generator.enemies_direction.Add(tmp[i]);
                }
                else
                {
                    enemies_generator.enemies_direction.Add(new Vector2(0, 0));
                }
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("enemies_direction"), true);
        }
    }
    private void displayEnemyPosition(EnemiesGenerator enemies_generator)
    {
        enemies_generator.default_position = EditorGUILayout.Toggle("Default Position", enemies_generator.default_position);
        if(enemies_generator.default_position)
        {
            return;
        }
        enemies_generator.random_position = EditorGUILayout.Toggle("Random Position", enemies_generator.random_position);
        if (enemies_generator.random_position)
        {
            return;
        }

        List<Vector2> tmp = new List<Vector2>(enemies_generator.enemies_position);
        enemies_generator.enemies_position.Clear();

        for (int i = 0; i < enemies_generator.number_enemies; ++i)
        {
            if (i < tmp.Count)
            {
                enemies_generator.enemies_position.Add(tmp[i]);
            }
            else
            {
                enemies_generator.enemies_position.Add(new Vector2(0, 0));
            }
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemies_position"), true);

    }
    void OnGUI()
    {
        if (EditorGUI.EndChangeCheck())
        {
            SceneView.RepaintAll();
        }
    }
}*/
