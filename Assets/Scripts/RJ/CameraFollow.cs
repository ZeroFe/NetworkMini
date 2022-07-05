using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 3;
    public Vector2 offset;
    public float limitMinX, limitMaxX, limitMinY, limitMaxY;
    float cameraHalfWidth, cameraHalfHeight;

    private void Start()
    {
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),   // X
            Mathf.Clamp(target.position.y + offset.y, limitMinY + cameraHalfHeight, limitMaxY - cameraHalfHeight), // Y
            -10);                                                                                                  // Z
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }
    //public GameObject target;

    //float offsetX = 0;
    //float offsetY = 0;
    //public float offsetZ;

    //public float DelayTime;

    //private void Update()
    //{
    //    Vector3 FixedPos = new Vector3(target.transform.position.x + offsetX,
    //                                   target.transform.position.y + offsetY,
    //                                   target.transform.position.z + offsetZ);
    //    transform.position = Vector3.Lerp(transform.position, FixedPos, Time.deltaTime * DelayTime);
    //}
}
//var curPos = transform.position;
//curPos += new Vector3(h, v, 0) * speed * Time.deltaTime;

////x,y범위를 지정해서범위에서만 움직이고 싶다
//curPos.x = Clamp(curPos.x, -16, 16);
//curPos.y = Clamp(curPos.y, -7, 6);

//transform.position = curPos;

