using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float hp { get; private set; }
    [SerializeField] float setHp;
    public float damage { get; private set; }
    [SerializeField] private float setDamage;
    public float movementSpeed { get; private set; }
    [SerializeField] private float setMovementSpeed;
    public float acquiredExp { get; private set;}
    [SerializeField] private float setAcquiredExp;

    private void OnEnable()
    {
        hp = setHp;
        damage = setDamage;
        movementSpeed = setMovementSpeed;
        acquiredExp = setAcquiredExp;
    }

    public void SetHp(float dmg)
    {
        hp += dmg;
    }
}
