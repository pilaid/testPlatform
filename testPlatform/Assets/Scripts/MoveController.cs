using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor;

public class MoveController : MonoBehaviour
{
    //manager�� Ÿ�ڵ忡 ���� �񵿱������� ȣ���� ���������� ����
    //controller�� update�Լ��� ����ؼ� ���������� ȣ���� ���� �ʴ��� Ÿ ����� �ҷ��� ����ϴ� ��찡 ����
    [Header("�÷��̾� �̵� �� ����")]
    Rigidbody2D rigid; //�̷��� �����ϸ� rigid���� null �� ����
    CapsuleCollider2D coll;
    Animator anim;
    Vector3 moveDir;
    float verticalVelocity = 0f;//�������� �������� ��

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;

    [SerializeField] float groundCheckLength;//�� ���̰� ���ӿ��� �󸶸�ŭ�� ���̷� �������� �������� ������������ �˼��� ����
    [SerializeField] bool showGroundCheck;
    [SerializeField] Color colorGroundCheck;

    [SerializeField] bool isGround;//�ν����Ϳ��� �÷��̾ �÷���Ÿ�Ͽ� ���� �ߴ��� üũ(�ӽ�)

    private void OnDrawGizmos()
    {
        if (showGroundCheck == true)
        {
            Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLength), colorGroundCheck);
        }

        //���� �׸��� �پ��� ���
        //Debug.DrawLine(transform); //����׵� üũ�뵵�� �� ī�޶� ���� �׷��� �� ����
        //Gizmos.DrawSphere(); //����׺��� �� ���� �ð�ȿ���� ����
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
        //�±״� string���� ����� �±׸� ����

        //���̾�� int�� ����� ���̾ ����
        //���̾��� int�� ���� ����ϴ� int�� �ٸ�
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
        //�¿�Ű�� ������ �¿�� �����̰�(���ϴ�x)
        //aŰ�� ���� ����Ű ������ -1, dŰ�� ������ ����Ű ������ 1, �ƹ��͵� �Է����� ������ 0�� ���
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveDir.y = rigid.velocity.y;
        //���ð��� ���鶧�� ������Ʈ�� �ڵ忡 ���ؼ� �����̵��ϰ� ����
        //�÷��� ���ӿ����� ������ ���� ������ �̵��ϰ� ����
        rigid.velocity = moveDir;
    }
}
