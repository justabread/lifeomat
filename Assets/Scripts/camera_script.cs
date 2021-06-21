using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    public float panSpeed;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 5)
        {
            Camera.main.orthographicSize--;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < 80)
        {
            Camera.main.orthographicSize++;
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
