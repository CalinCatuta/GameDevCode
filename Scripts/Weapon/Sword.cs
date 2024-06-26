using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private WeaponScriptableObject weaponData;

    [HideInInspector]
    public float currentDamage;

    private void Awake() {
        currentDamage = weaponData.Damage;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(currentDamage);
        }
    }
  
}
