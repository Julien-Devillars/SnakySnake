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

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        gameObject.transform.Translate(speed_x * Time.deltaTime, speed_y * Time.deltaTime, 0);
        //Rigidbody2D r = gameObject.GetComponent<Rigidbody2D>();
        //r.AddForce(new Vector2(speed_x * Time.deltaTime, speed_y * Time.deltaTime));
    }

    void OnBecameInvisible()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Border") return;
        

        if (collision.gameObject.name == "left" || collision.gameObject.name == "right")
        {
            speed_x = -speed_x;
        }
        if (collision.gameObject.name == "top" || collision.gameObject.name == "bot")
        {
            speed_y = -speed_y;
        }
    }

}
