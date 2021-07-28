using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    public int zoomSpeed;
    public float panSpeed;
    public int minZoom = 10;
    public int maxZoom = 160;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > minZoom)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - zoomSpeed;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < maxZoom)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + zoomSpeed; ;
        }

        if (Input.GetMouseButton(0)) // right mouse button
        {
            var newPosition = new Vector3();
            newPosition.x = Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            newPosition.y = Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;
            // translates to the opposite direction of mouse position.
            transform.Translate(-newPosition);
        }
    }
}
