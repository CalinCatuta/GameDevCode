using UnityEngine;


[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject {
    [SerializeField]
    private float damage;
    public float Damage { get => damage; private set => damage = value; }

}