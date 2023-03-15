using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPosition : MonoBehaviour
{
    public GameObject arrow;
    bool move_ball;

    private GameObject current_arrow;
    public float speed_ball = 0.1f;
    private Vector3 last_mouse_position;
    private Vector3 last_ball_position;

    private GameObject lastItBorder;
    private GameObject borderStart;
    private GameObject borderEnd;

    private List<Collider2D> colliders_disabled;

    public GameObject mBackGround;

    private enum BallState
    {
        Free,
        Locked,
        Moving
    };
    private BallState currentBallState;

    // Start is called before the first frame update
    void Start()
    {
        current_arrow = GameObject.Instantiate(arrow);
        drawArrow(false);

        colliders_disabled = new List<Collider2D>();
        currentBallState = BallState.Free;
        MeshFilter mesh_renderer = mBackGround.GetComponent<MeshFilter>();
        Mesh mesh = mesh_renderer.mesh;
        foreach (Vector3 vertex in mesh.vertices)
        {
            Debug.Log(vertex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBallState == BallState.Locked && Input.GetMouseButtonDown(0))
        {
            startMovingBall();
        }
        if (currentBallState == BallState.Free)
        {
            setFirstTimeBallOnBorder();
        }

        if (Input.GetMouseButtonDown(1)) // Right Click
        {
            resetBall();

            MeshFilter mesh_renderer = mBackGround.GetComponent<MeshFilter>();
            Mesh mesh = mesh_renderer.mesh;
            for (int i = 0; i < mesh.vertices.Length; ++i)
            {
                Vector3 vertex = mesh.vertices[i];
                //mesh.Clear
                mesh.vertices[i] = vertex + new Vector3(1,0,0);
            }
            //mBackGround.GetComponent<MeshFilter>().mesh = mesh;
        }

        drawArrow(currentBallState == BallState.Locked);

        if(currentBallState == BallState.Moving)
        {
            moveBall();
        }

    }

    void drawArrow(bool display)
    {
        if(!current_arrow)
        {
            return;
        }

        SpriteRenderer arrow_renderer = current_arrow.gameObject.GetComponent<SpriteRenderer>();

        if (display)
        {
            current_arrow.transform.position = transform.position;
            Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = AngleBetweenTwoPoints(current_arrow.transform.position, mouse_position);
            current_arrow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90));

            arrow_renderer.enabled = true;
        }
        else
        {
            arrow_renderer.enabled = false;
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void startMovingBall()
    {
        Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentBallState = BallState.Moving;
        last_mouse_position = new Vector3(mouse_position.x, mouse_position.y, 0);
        last_ball_position = transform.position;
        borderStart = lastItBorder;

        foreach (Collider2D collider_disabled in colliders_disabled)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collider_disabled, false);
        }
        colliders_disabled.Clear();
    }
    void setFirstTimeBallOnBorder()
    {
        Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        if (currentBallState == BallState.Free)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider && hit.collider.gameObject.tag == "Border")
                {
                    sprite.enabled = true;

                    if (hit.collider.gameObject.name == "left" || hit.collider.gameObject.name == "right")
                    {
                        gameObject.transform.position = new Vector3(hit.collider.bounds.center.x, mouse_position.y, 0);
                    }
                    if (hit.collider.gameObject.name == "top" || hit.collider.gameObject.name == "bot")
                    {
                        gameObject.transform.position = new Vector3(mouse_position.x, hit.collider.bounds.center.y, 0);
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        currentBallState = BallState.Locked;
                    }
                }
            }
        }
        else
        {
            sprite.enabled = false;
            //gameObject.transform.position = new Vector3(mouse_position.x, mouse_position.y, 0);
        }
    }
    void resetBall()
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        currentBallState = BallState.Free;
        sprite.enabled = false;

        foreach (Collider2D collider_disabled in colliders_disabled)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collider_disabled, false);
        }
        colliders_disabled.Clear();
    }
    void moveBall()
    {
        Vector3 normalized_direction = (last_mouse_position - last_ball_position).normalized;
        transform.position += normalized_direction * speed_ball * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Ennemy") return;
        //
        //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
        //colliders_disabled.Add(collision.collider);
        //
        //if (collision.gameObject.tag == "Border")
        //{
        //    lastItBorder = collision.gameObject;
        //}
        //
        //if(currentBallState == BallState.Moving && collision.gameObject != borderStart)
        //{
        //    currentBallState = BallState.Locked;
        //    if (collision.gameObject.name == "left" || collision.gameObject.name == "right")
        //    {
        //        gameObject.transform.position = new Vector3(collision.collider.bounds.center.x, gameObject.transform.position.y, 0);
        //    }
        //    if (collision.gameObject.name == "top" || collision.gameObject.name == "bot")
        //    {
        //        gameObject.transform.position = new Vector3(gameObject.transform.position.x, collision.collider.bounds.center.y, 0);
        //    }
        //    current_arrow.transform.position = Vector3.MoveTowards(current_arrow.transform.position, gameObject.transform.position, speed_ball * Time.deltaTime);
        //}

    }



}
