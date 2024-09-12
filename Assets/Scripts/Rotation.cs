using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    private new Transform transform;

    public Vector3 rotation;

    void Start() {
        transform = GetComponent<Transform>();
    }

    void Update() {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
