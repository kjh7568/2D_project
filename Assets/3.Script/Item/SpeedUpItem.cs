using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : MonoBehaviour
{
    [SerializeField] private float increaseAmount = 5f;
    private Renderer renderer;
    private CapsuleCollider2D capsuleCollider2d;

    private void Start()
    {
        TryGetComponent(out renderer);
        TryGetComponent(out capsuleCollider2d);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        StartCoroutine(ItemEvent_Co(collision));
    //    }
    //}

    //private IEnumerator ItemEvent_Co(Collider2D collision)
    //{
    //    float duration = 5f;

    //    if (collision.transform.TryGetComponent(out PlayerController pc))
    //    {
    //        pc.SetMovementSpeed(increaseAmount);
    //    }

    //    renderer.enabled = false;
    //    capsuleCollider2d.enabled = false; 

    //    yield return new WaitForSeconds(duration);

    //    pc.SetMovementSpeed(-increaseAmount);
        
    //    //오브젝트 풀링으로 만들기
    //    Destroy(gameObject);
    //}
}
