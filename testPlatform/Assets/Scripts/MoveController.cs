using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor;

public class MoveController : MonoBehaviour
{
    //manager는 타코드에 의해 비동기적으로 호출이 왔을때에만 대응
    //controller는 update함수를 사용해서 동기적으로 호출이 오지 않더라도 타 기능을 불러서 사용하는 경우가 많음
    [Header("플레이어 이동 및 점프")]
    Rigidbody2D rigid; //이렇게 선언하면 rigid에는 null 들어가 있음
    CapsuleCollider2D coll;
    Animator anim;
    Vector3 moveDir;
    float verticalVelocity = 0f;//수직으로 떨어지는 힘

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;

    [SerializeField] float groundCheckLength;//이 길이가 게임에서 얼마만큼의 길이로 나오는지 육안으로 보기전까지는 알수가 없음
    [SerializeField] bool showGroundCheck;
    [SerializeField] Color colorGroundCheck;

    [SerializeField] bool isGround;//인스펙터에서 플레이어가 플랫폼타일에 착지 했는지 체크(임시)

    private void OnDrawGizmos()
    {
        if (showGroundCheck == true)
        {
            Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLength), colorGroundCheck);
        }

        //레이 그리는 다양한 방법
        //Debug.DrawLine(transform); //디버그도 체크용도로 씬 카메라에 선을 그려줄 수 있음
        //Gizmos.DrawSphere(); //디버그보다 더 많은 시각효과를 제공
        //Handles.DrawWireArc
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        checkGround();
        moving();
    }

    private void checkGround()
    {
        //태그는 string으로 대상의 태그를 구분

        //레이어는 int로 대상의 레이어를 구분
        //레이어의 int와 평상시 사용하는 int는 다름
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, LayerMask.GetMask("Ground"));

        if (hit)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    private void moving()
    {
        //좌우키를 누르면 좌우로 움직이게(상하는x)
        //a키나 왼쪽 방향키 누르면 -1, d키나 오른쪽 방향키 누르면 1, 아무것도 입력하지 않으면 0이 출력
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveDir.y = rigid.velocity.y;
        //슈팅게임 만들때는 오브젝트를 코드에 의해서 순간이동하게 만듬
        //플랫폼 게임에서는 물리에 의해 실제로 이동하게 만듬
        rigid.velocity = moveDir;
    }
}
