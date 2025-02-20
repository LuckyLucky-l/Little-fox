using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//相机视觉差的作用
public class Parallax : MonoBehaviour
{
    public Transform Cam;
    public float moveRate;
    private float startPointX, startPointY;
    public bool lockY;//默认false

    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockY)
        {
            transform.position = new Vector2(startPointX + Cam.position.x * moveRate, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(startPointX + Cam.position.x * moveRate, startPointY + Cam.position.y * moveRate);
        }
    }
}
