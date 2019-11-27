using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Camera))]
public class MultipleTargetCamera : MonoBehaviour {
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = .5f;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;
    private Camera cam;

    static public int cantTargets;
    static public Transform target;
    void Start () {
        cam = GetComponent<Camera> ();
        cantTargets = 2;
    }

    void LateUpdate () {

        if (targets.Count == 0)
            return;

        Move ();
        Zoom ();

    }
    void Zoom () {
        float newZoom = Mathf.Lerp (maxZoom, minZoom, GetGreatestDistance () / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, newZoom, Time.deltaTime);
    }

    void Move () {
        Vector3 centerPoint = GetCenterPoint ();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp (transform.position, newPosition, ref velocity, smoothTime);
    }
    float GetGreatestDistance () {
        Bounds bounds;
        if (cantTargets != 1) {
            bounds = new Bounds (targets[0].position, Vector3.zero);
        } else {
            bounds = new Bounds (target.position, Vector3.zero);
        }
        for (int i = 0; i < targets.Count; i++) {
            if (cantTargets != 1) {
                bounds.Encapsulate (targets[i].position);
            } else {
                bounds.Encapsulate (target.position);
            }
        }
        return bounds.size.x;
    }
    Vector3 GetCenterPoint () {
        if (targets.Count == 1) {
            return targets[0].position;
        }
        Bounds bounds;
        if (cantTargets != 1) {
            bounds = new Bounds (targets[0].position, Vector3.zero);
        } else {
            bounds = new Bounds (target.position, Vector3.zero);
        }

        for (int i = 0; i < targets.Count; i++) {
            if (cantTargets != 1) {
                bounds.Encapsulate (targets[i].position);
            } else {
                bounds.Encapsulate (target.position);
            }

        }
        return bounds.center;
    }
    static public void SetTargets (Transform _target, int _cantTargets) {
        target = _target;
        cantTargets = _cantTargets;
    }
}