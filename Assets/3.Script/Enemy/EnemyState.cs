using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float hp { get; private set; }
    [SerializeField] float setHp;
    public float damage { get; private set; }
    [SerializeField] private float setDamage;
    public float movementSpeed { get; private set; }
    [SerializeField] private float setMovementSpeed;
    public float acquiredExp { get; private set; }
    [SerializeField] private float setAcquiredExp;

    private void Awake()
    {
        TryGetComponent(out spriteRenderer);
    }
    private void OnEnable()
    {
        hp = setHp;
        damage = setDamage;
        movementSpeed = setMovementSpeed;
        acquiredExp = setAcquiredExp;

        spriteRenderer.color = Color.white;
    }

    public void SetHp(float dmg)
    {
        hp += dmg;
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cold"))
        {
            if (Random.Range(0, 100) < 20)
            {
                StartCoroutine(Freeze());
            }
        }
    }

    public IEnumerator Freeze()
    {
        float temp = movementSpeed;
        movementSpeed = 0;
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(1f);

        movementSpeed = temp;
        spriteRenderer.color = Color.white;
    }

}
