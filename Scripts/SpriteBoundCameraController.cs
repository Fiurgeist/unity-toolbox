using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic 2D camera controller providing scroll and zoom within the bounds of a background sprite.
/// </summary>
public class SpriteBoundCameraController : MonoBehaviour {
    public int speed = 10;
    
    private Camera cam;
    private Vector3 spriteBounds;
    private float maxOrthographicSize;

    private float horizontalExtent;
    private float rightBound;
    private float leftBound;
    private float topBound;
    private float bottomBound;

    void Awake() {
        cam = GetComponent<Camera>();
    }

    public void Setup(Vector3 spriteBounds) {
        this.spriteBounds = spriteBounds;

        int maxOrthographicSizeVertical = (int)(spriteBounds.y / 2.0f);
        int maxOrthographicSizeHorizontal = (int)((spriteBounds.x * Screen.height) / (2 * Screen.width));
        maxOrthographicSize = Mathf.Min(maxOrthographicSizeVertical, maxOrthographicSizeHorizontal);

        CalcCameraBounds(GetComponent<Camera>().orthographicSize);
    }

    private void CalcCameraBounds(float verticalExtent) {
        horizontalExtent = verticalExtent * Screen.width / Screen.height;

        leftBound = (float)(horizontalExtent - spriteBounds.x / 2.0f);
        rightBound = (float)(spriteBounds.x / 2.0f - horizontalExtent);
        bottomBound = (float)(verticalExtent - spriteBounds.y / 2.0f);
        topBound = (float)(spriteBounds.y / 2.0f - verticalExtent);
    }
    
    void Update() {
        bool changed = false;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float zoom = Input.GetAxisRaw("Mouse ScrollWheel");


        if (horizontal != 0) {
            transform.Translate(transform.right * speed * Time.deltaTime * horizontal);
            changed = true;
        }
        if (vertical != 0) {
            transform.Translate(transform.up * speed * Time.deltaTime * vertical);
            changed = true;
        }
        if (zoom != 0) {
            float newSize = zoom < 0 ? cam.orthographicSize + 1 : cam.orthographicSize - 1;
            cam.orthographicSize = Mathf.Clamp(newSize, 1f, maxOrthographicSize);
            CalcCameraBounds(cam.orthographicSize);
            changed = true;
        }

        if (changed) {
            var pos = new Vector3(
                Mathf.Clamp(transform.position.x, leftBound, rightBound),
                Mathf.Clamp(transform.position.y, bottomBound, topBound),
                transform.position.z);
            transform.position = pos;
        }
    }
}
