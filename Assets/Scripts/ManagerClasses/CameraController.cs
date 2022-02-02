using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;
    [SerializeField] private Player player;

    private Vector2 lastPos;
    [SerializeField] private Transform midBackground;
    

    [SerializeField] private float minCamHeight, maxCamHeight;

    private void Awake()
    {
        instance = this;
        target = player.gameObject.transform;
    }
    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        //lock camera to target
        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minCamHeight, maxCamHeight), transform.position.z);

        //how much should the camera move in relation to target
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);
        //farBackground.position += new Vector3(amountToMove.x*0.8f, amountToMove.y*0.8f, 0f);
        if (midBackground) {
            midBackground.position += new Vector3(amountToMove.x*0.3f, 0, 0f);
        }
        lastPos = transform.position;

        //clamp camera vertically
        //float clampedY = Mathf.Clamp(transform.position.y, minCamHeight, maxCamHeight);
        //transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }
}
