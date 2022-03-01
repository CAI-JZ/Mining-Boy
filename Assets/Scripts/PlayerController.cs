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
 

    private void Awake()
    {
        m_Rigid = GetComponent<Rigidbody2D>();   
    }

 
    void Update()
    {
        MoveUp = Input.GetAxisRaw("Vertical");
        MoveRight = Input.GetAxisRaw("Horizontal");

        Vector2 MoveDir = new Vector2(-MoveUp,MoveRight);
        if (MoveDir != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, MoveDir);
            //CheckPoint.transform.rotation = Quaternion.RotateTowards(CheckPoint.transform.rotation, toRotation,1440*Time.deltaTime);
            CheckPoint.transform.rotation = toRotation;
        }
    }

    private void FixedUpdate()
    {
        Move(MoveRight, MoveUp); 
    }

    void Move(float x, float y)
    {
        m_Rigid.MovePosition(new Vector2(transform.position.x + x * Speed * Time.deltaTime, transform.position.y + y * Speed * Time.deltaTime));
       
    }

}
