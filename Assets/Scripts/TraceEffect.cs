using UnityEngine;
using System.Collections;

// POOR THING. . .

[RequireComponent(typeof(LineRenderer))]
public class TraceEffect : MonoBehaviour {
    LineRenderer line;
    Material mat;

    float lifeTime = 0.07f;

    Vector3 startPos = Vector3.zero, endPos = Vector3.zero;
    float dist;
    float startTime = 0;

    bool animate = false;
    Color col;
    Vector3 _pos;

    void Awake() {
        line = GetComponent<LineRenderer>();
        mat = line.material;
    }

    public void Active(float time = 0.07f) {
        gameObject.SetActive(true);
        line.enabled = true;
        lifeTime = time;
        dist = Vector3.Distance(startPos, endPos);
        startTime = Time.realtimeSinceStartup + lifeTime;
        _pos = startPos;
        col = mat.GetColor("_TintColor");
        animate = true;
    }

    public void SetStartPos(Vector3 pos) {
        line.SetPosition(0, pos);
        startPos = pos;
    }

    public void SetEndPos(Vector3 pos) {
        line.SetPosition(1, pos);
        endPos = pos;
    }

    void Update() {
        if ( !animate ) return;

        if ( startTime > Time.realtimeSinceStartup ) {

            col.a = Mathf.MoveTowards(col.a, 0, 1/lifeTime * Time.deltaTime);
            mat.SetColor("_TintColor", col);

            _pos = Vector3.MoveTowards(_pos, endPos, Random.Range(40f,60f)/lifeTime * Time.deltaTime);
            line.SetPosition(0, _pos);

        } else {
            animate = false;
            line.enabled = false;
            col.a = 1;
            mat.SetColor("_TintColor", col);
            gameObject.SetActive(false);
        }
    }

}
