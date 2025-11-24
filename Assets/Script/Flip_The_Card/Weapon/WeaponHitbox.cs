using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public Weapon weaponData; // Weapon 정보 연결

    private bool hitActive = false;
    private List<Collider> hitEnemies = new List<Collider>();

    void OnTriggerEnter(Collider other)
    {
        if (!hitActive) return;
        Debug.Log("Trigger Enter: " + other.name);

        if (!other.CompareTag("Enemy")) return;

        if (!hitEnemies.Contains(other))
        {
            hitEnemies.Add(other);
            // Weapon 데이터를 사용해서 데미지 전달
            other.GetComponent<EnemyHealth>().TakeDamage(weaponData.damage);
            Debug.Log($"Hit {other.name} with {weaponData.weaponName} for {weaponData.damage} damage");
        }
    }

    public void EnableHit()
    {
        hitActive = true;
        hitEnemies.Clear();
        Debug.Log("ㅁㅇㅁ나ㅜㄻ누림낭ㅁㄴ암누이ㅏ");

    }

    public void DisableHit()
    {
        hitActive = false;
    }
}
