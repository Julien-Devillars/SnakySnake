using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_ennemies : MonoBehaviour
{
    public float speed_x;
    public float speed_y;
    // Start is called before the first frame update
    void Start()
    {
        speed_x = Random.Range(-10.0f, 10.0f);;
        speed_y = Random.Range(-10.0f, 10.0f); ;
    }
    private void FixedUpdate()
    {
        gameObject.transform.Translate(speed_x * Time.deltaTime, speed_y * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.name.Contains("Border")) return;
        
        if (collision.gameObject.tag == "VerticalBorder")
        {
            speed_x = -speed_x;
        }
        if (collision.gameObject.tag == "HorizontalBorder")
        {
            speed_y = -speed_y;
        }
    }

}
