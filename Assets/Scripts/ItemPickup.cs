using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {

    public enum Type { Health, Weapon };

    public Type type;
    public int healAmount = 5;
    public int weaponId = 1;

    public float timeSpawned;

    void Start() {
        timeSpawned = Time.realtimeSinceStartup;
    }

    void OnTriggerEnter(Collider other) {
        if ( other.CompareTag("Player") ) {
            switch ( type ) {
                case Type.Health:
                    Health health = other.GetComponent<Health>();
                    if ( health ) {
                        health.Heal(healAmount);
                        print("healing +"+healAmount);
                    }
                    break;
                case Type.Weapon:
                    WeaponManager weapon = other.GetComponent<WeaponManager>();
                    if ( weapon ) {
                        weapon.ActiveWeapon(weaponId);
                        print("actived weapon id "+weaponId);
                    }
                    break;
            }
            Destroy(gameObject);
        }

        if ( other.CompareTag("Pickup") ) {
            if (timeSpawned > other.GetComponent<ItemPickup>().timeSpawned ) {
                Destroy(other.gameObject);
            }
        }
    }

}
