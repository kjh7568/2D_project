using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerState playerState;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public float x;
    public float y;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out playerState);

        TryGetComponent(out spriteRenderer);
        TryGetComponent(out animator);
    }

    // Update is called once per frame
    void Update()
    {
        //if(GameManager.GM.playerState.isGetReward)
        //{
        //    return;
        //}

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if(x != 0 || y != 0)
        {
            animator.SetBool("isMove", true);
        } else
        {
            animator.SetBool("isMove", false);
        }


        transform.Translate(new Vector2(x, y) * Time.deltaTime * playerState.movementSpeed);
    }

    //public void SetMovementSpeed(float add)
    //{
    //    playerState.movementSpeed += add;
    //}
}
