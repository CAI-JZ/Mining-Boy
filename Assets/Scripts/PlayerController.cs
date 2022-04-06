using System;
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
    //private SpriteRenderer Sprite;
    private Animator Animator;
    float FlipX = 1;

    public Animator PickAnim;
    public AudioSource Footpoint;
    bool IsMove;

    private void Awake()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        //Sprite = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Player.Instance.PickUpdate += NewPick;
    }

    private void NewPick(GameObject pick)
    {
        PickAnim = pick.GetComponent<Animator>();
    }

    void Update()
    {
        MoveUp = Input.GetAxisRaw("Vertical");
        MoveRight = Input.GetAxisRaw("Horizontal");
        bool upInput = !Mathf.Approximately(MoveUp, 0f);
        bool rightInput = !Mathf.Approximately(MoveRight, 0f);
        IsMove = upInput || rightInput;

        Vector2 MoveDir = new Vector2(-MoveUp,MoveRight);
        if (MoveDir != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, MoveDir);
            CheckPoint.transform.rotation = toRotation;
        }

        //Pick
        IsHit = Input.GetKeyDown(KeyCode.Mouse0);
        Interact(IsHit);
        Animation(MoveRight, MoveUp);

        
    }

    private void FixedUpdate()
    {
        Move(MoveRight, MoveUp);

        if (IsMove)
        {
            if (!Footpoint.isPlaying)
            {
                Footpoint.Play();
            }
        }
        else
        {
            Footpoint.Stop();
        }
    }

    void Move(float x, float y)
    {
        Flip(x);
        m_Rigid.MovePosition(new Vector2(transform.position.x + x * Speed * Time.deltaTime, transform.position.y + y * Speed * Time.deltaTime));
     }

    void Animation(float x, float y)
    {
        Animator.SetFloat("PosY", y);
        Animator.SetFloat("PosX", Mathf.Abs(x));
    }

    private void OnLevelWasLoaded(int level)
    {
        transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }

    void Flip(float inputX)
    {
        float Flip = inputX * FlipX;
        if (Flip < 0)
        {
            FlipX = -FlipX;
            transform.localScale = new Vector3(FlipX, 1, 1);
        }
    }


    void Interact(bool Click)
    {
        if (Click)
        {
            //PickAnim.SetTrigger("Pick");
            RaycastHit2D Hitinfo = Physics2D.Raycast(CheckPoint.transform.position, CheckPoint.transform.right, 0.5f);
            #if UNITY_EDITOR
            Debug.DrawRay(CheckPoint.transform.position, CheckPoint.transform.right * 2, Color.red, 0.5f);
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
                            PickAnim.SetTrigger("Pick");
                            Player.Instance.UseOxygen();
                            item.PlayerInteract(Player.Instance.Strength);
                            
                            break;
                        case "Item":
                            item.PlayerInteract(Player.Instance.Strength);
                            break;
                       
                    }
                   
                }
            }
        }
    }

}
