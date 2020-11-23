using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GameCamera : MonoBehaviour {
    public Transform target;

    public float smoothSpeed = 5f;
    [SerializeField]
    private Vector3 offset = new Vector3(0, -0.5f, -10);

    public float size = 5f;

    private new Camera camera;

    private void Start() {
        camera = GetComponent<Camera>();
    }

    public void FixedUpdate() {
        try {
            if (target == null) return;

            Vector3 newPosition = target.position + offset;
            newPosition.z = -10f;

            //Smooth
            transform.position -= (transform.position - newPosition) * smoothSpeed * Time.deltaTime;
            camera.orthographicSize -= (camera.orthographicSize - size) * smoothSpeed * Time.deltaTime;
        } catch (Exception e) {
            Debug.LogError(e.ToString());
        }
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }
}