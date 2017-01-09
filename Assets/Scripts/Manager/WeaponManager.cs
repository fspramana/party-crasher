using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {

    [HideInInspector]
    public Weapon activedWeapon;

    public Weapon[] weapons;

    void Start() {
        ActiveWeapon(0);
    }

    /// <summary>
    /// Set current active weapon.
    /// pistol = 0, rifle = 1, shotgun = 2, sniper = 3, machinegun = 4.
    /// </summary>
    /// <param name="id">weapon id</param>
    public void ActiveWeapon(int id) {
        bool found = false;
        foreach ( Weapon wpn in weapons ) {
            if ( wpn.id == id ) {
                wpn.gameObject.SetActive(false);
                wpn.gameObject.SetActive(true);
                activedWeapon = wpn;
                found = true;
                continue;
            }
            wpn.gameObject.SetActive(false);
        }

        if ( !found ) {
            Debug.LogError("Weapon with id = "+id+" did not found.");
        }
    }

}
