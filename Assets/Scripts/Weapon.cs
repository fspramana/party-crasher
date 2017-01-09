using UnityEngine;
using System.Collections;
using InControl;

public class Weapon : MonoBehaviour {

    private new Transform transform;

    public enum Type { auto, semiAuto, noAuto };

    public bool debugRay = false;

    private int deviceId = 0;

    public int id = 0;
    public Type type;
    [Range(1,10)]
    public int projectilePerShot = 1;
    public int bulletAmount = 20;
    public int damagePerShot = 4;
    public float fireRate = 9;
    public float maxDistance = 12f;
    public float inaccurationInDegree = 0.02f;
    [Range(0,50)]
    public float slowMovementInPercent = 10f;
    public LayerMask layerMask;

    public Transform  bulletSpawnPos;

    public GameObject traceEffectPrefab;
     
    [HideInInspector]
    public bool fire = false;

    private float nextFire = 0;
    private int bullet;

    InputDevice device;
    WeaponManager manager;
    CapsuleController controller;

    int semiAutoCounter = 0;
    bool semiFire = false;

    int i;
    void Awake() {
        transform = GetComponent<Transform>();
        controller = transform.parent.GetComponent<CapsuleController>();
    }

    void OnEnable() {
        bullet = bulletAmount;
        controller.SetMovementSpeed(slowMovementInPercent);
    }

    void Start() {
        device = controller.GetControllerDevice();
        manager = transform.parent.GetComponent<WeaponManager>();
    }

    void Update() {

        if ( debugRay ) {
            Vector3 direction = bulletSpawnPos.TransformDirection(Vector3.forward);
            Debug.DrawRay(bulletSpawnPos.position, direction * maxDistance, Color.red);
        }
        
        switch ( type ) {
            case Type.auto:
                fire = device.RightTrigger;
                break;
            case Type.semiAuto:
                semiFire = device.RightTrigger.WasPressed;
                if (semiFire) {
                    fire = true;
                    semiAutoCounter = 4;
                }
                
                if (semiAutoCounter <= 0 ) {
                    fire = false;
                }

                break;
            case Type.noAuto:
                fire = device.RightTrigger.WasPressed;
                break;
        }

        if ( fire && Time.time >= nextFire && bullet != 0) {
            for ( i = 0; i < projectilePerShot; i++) {
                Fire();
            }
            if (bullet > 0 ) {
                bullet--;
            }

            semiAutoCounter--;

            nextFire = Time.time + (1/fireRate);
        }

        if (bullet == 0 ) {
            fire = false;
            // active pistol
            manager.ActiveWeapon(0);
        }
    }

    void Fire() {
        Vector3 direction = bulletSpawnPos.TransformDirection(Vector3.forward + (Vector3.right * Random.Range(-inaccurationInDegree/2, inaccurationInDegree/2)));

        RaycastHit hit;

        GameObject obj = ObjectPooler.pooler.GetObject(traceEffectPrefab);
        obj.SetActive(true);

        TraceEffect traceEffect = obj.GetComponent<TraceEffect>();
        traceEffect.SetStartPos(bulletSpawnPos.position);
        if ( Physics.Raycast(bulletSpawnPos.position, direction, out hit, maxDistance, layerMask) )  {

            if ( hit.collider.CompareTag("Player") ) {
                hit.collider.GetComponent<Health>().Damage(damagePerShot);
            }

            traceEffect.SetEndPos(hit.point);
        } else {
            traceEffect.SetEndPos( bulletSpawnPos.position + direction * maxDistance);
        }

        traceEffect.Active(0.2f);
    }

}
