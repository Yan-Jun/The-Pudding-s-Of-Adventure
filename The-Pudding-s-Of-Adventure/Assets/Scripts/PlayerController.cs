using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public PlayerData m_thePlayerData { get; set; }

    private Vector2 v2_Controller;
    private Rigidbody2D m_rigidbody;
    private Vector2 v2_MousePosition
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x , Input.mousePosition.y , 0f));
        }
    }
    [SerializeField] private SpriteMask m_SprMask;
    [SerializeField] private LayerMask Layer_Floor;
    public PhysicsMaterial2D p;
    private void OnEnable()
    {
        m_thePlayerData = Resources.Load<PlayerData>("Player");
    }
    private void Start()
    {
        m_rigidbody = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        m_SprMask.transform.position = v2_MousePosition;
    }
    private void LateUpdate()
    {
        v2_Controller = new Vector2(Input.GetAxis("Horizontal") , Input.GetAxis("Vertical"));
        if (v2_Controller.sqrMagnitude > 0f)
        {
            m_rigidbody.velocity = new Vector2(v2_Controller.x * m_thePlayerData.m_fMoveSpped, m_rigidbody.velocity.y);
        }
        else
        {
            m_rigidbody.velocity = new Vector2(0f, m_rigidbody.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))            //跳躍
        {
            if (Fn_IsInFloor())
            {
                m_rigidbody.AddForce(Vector2.up * m_thePlayerData.m_fJumpHigh);
            }
        }
    }
    private bool Fn_IsInFloor()                 //判斷是否在地面
    {
        Ray2D ray = new Ray2D(this.transform.position , Vector2.down);
        return Physics2D.Raycast(ray.origin, ray.direction , 0.85f , Layer_Floor);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Bounce")
        {
            if (other.contacts[0].normal == Vector2.up)
            {
                m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, 5f);
            }
        }
    }
}
[System.Serializable]
public class PlayerData : ScriptableObject
{
    public int m_iMaxHp;
    public int m_iHp;
    public float m_fMoveSpped;
    public float m_fJumpHigh;
}