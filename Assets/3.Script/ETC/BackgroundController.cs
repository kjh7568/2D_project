using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Area"))
        {
            Vector2 tilePos = transform.position;
            Vector2 playerPos = GameManager.GM.playerController.transform.position;
            int playerDirectionX = (int)Mathf.Sign(GameManager.GM.playerController.x);
            int playerDirectionY = (int)Mathf.Sign(GameManager.GM.playerController.y);


            float diffX = Mathf.Abs(playerPos.x - tilePos.x);
            float diffY = Mathf.Abs(playerPos.y - tilePos.y);

            switch (transform.tag)
            {
                case "Background":
                    if (diffX > diffY)
                    {
                        transform.Translate(Vector2.right * playerDirectionX * 40);
                    }
                    else if (diffX < diffY)
                    {
                        transform.Translate(Vector2.up * playerDirectionY * 40);
                    }
                    break;
            }
        }
    }
}
