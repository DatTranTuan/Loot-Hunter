using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask GroupLayer;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool isJumping;
    [SerializeField] private Animator anim;

    private string currentAnimName;
    private float horizontal;

    void Update()
    {
        isGrounded = CheckGrounded();
        horizontal = Input.GetAxisRaw("Horizontal");
        if (isGrounded)
        {
            //jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
                ChangeAnim("jump");
            }

            //change anim run
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }
            //attack
            if (Input.GetKeyDown(KeyCode.X) && isGrounded)
            {
                Attack();
                Debug.Log("attack");
            }
            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                Attackcb();
            }
        }

        // falling
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            isJumping = false;
        }

        //moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);  
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }

        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }

    private void Attack()
    {
        ChangeAnim("attack");
        
        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void Attackcb()
    {
        ChangeAnim("attackcb");
        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void ResetAttack()
    {
        ChangeAnim("ilde");
    }


    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.9f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, GroupLayer);
        
        return hit.collider != null;
    }

  
    private void Jump()
    {
        isJumping = true; 
        rb.AddForce(new Vector2(0f, jumpForce)); 
    }

    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }

    }
}
