using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int maxHitPoint = 30;

    private int hitPoint;
    private Animator _animator;

    public int CurrentHitPoint
    {
        get { return hitPoint; }
    }

    void Start() {
        Reset();
        _animator = GetComponent<Animator>();
    }

    public void Heal(int healAmount) {
        hitPoint += healAmount;
        hitPoint = Mathf.Clamp(hitPoint, 0, maxHitPoint);
    }

    public void Damage(int damage) {
        hitPoint -= damage;
        if ( hitPoint <= 0 ) {
            // dead
            _animator.SetTrigger("Dies");
            Weapon[] weapons = gameObject.GetComponentsInChildren<Weapon>();
            foreach (Weapon weapon in weapons) weapon.gameObject.SetActive(false);
            gameObject.layer = LayerMask.NameToLayer("Dead Player");
            GameManager.singleton.playerRemain.Remove(gameObject);
            Camera.main.GetComponent<CameraFollow>().bounds.Remove(GetComponent<CameraBoundObject>());
        }
    }

    public bool IsLiving()
    {
        return hitPoint > 0;
    }

    void Reset() {
        hitPoint = maxHitPoint;
    }
}
