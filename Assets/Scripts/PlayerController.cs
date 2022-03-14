using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D m_Rigid;

    [SerializeField]
    private float Speed = 4;
    private float MoveUp;
    private float MoveRight;
    public float num;
    public GameObject CheckPoint;
    private bool IsHit;



    private void Awake()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(gameObject);
    }

 
    void Update()
    {
        MoveUp = Input.GetAxisRaw("Vertical");
        MoveRight = Input.GetAxisRaw("Horizontal");

        Vector2 MoveDir = new Vector2(-MoveUp,MoveRight);
        if (MoveDir != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, MoveDir);
            CheckPoint.transform.rotation = toRotation;
        }

        //Pick
        IsHit = Input.GetKeyDown(KeyCode.Mouse0);
        Interact(IsHit);

    }

    private void FixedUpdate()
    {
        Move(MoveRight, MoveUp); 
    }

    void Move(float x, float y)
    {
        m_Rigid.MovePosition(new Vector2(transform.position.x + x * Speed * Time.deltaTime, transform.position.y + y * Speed * Time.deltaTime));  
    }

    private void OnLevelWasLoaded(int level)
    {
        transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }


    void Interact(bool Click)
    {
        if (Click)
        {
            RaycastHit2D Hitinfo = Physics2D.Raycast(CheckPoint.transform.position, CheckPoint.transform.right, 1f);
            #if UNITY_EDITOR
            Debug.DrawRay(CheckPoint.transform.position, CheckPoint.transform.right * 2, Color.red, 10f);
            #endif

            if (Hitinfo.collider != null)
            {
                //print(Hitinfo.collider.tag);
                Inf_Interact item = Hitinfo.collider.GetComponent<Inf_Interact>();
                if (item != null)
                {
                    switch (Hitinfo.collider.tag)
                    {
                        case "Rock":
                            Player.Instance.UseOxygen();
                            item.PlayerInteract(Player.Instance.Strength);
                            break;
                        case "Door":
                            item.PlayerInteract(Player.Instance.Strength);
                            break;
                    }
                   
                }
            }
        }
    }

}
