using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    private GameManager manager;
    public Camera mainCam;
    public bool dayNight;
    public Vector2 timeRange;
    public bool active;
    public bool collected;

    void Start()
    {
        manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        active = ((manager.dayNight == dayNight) && manager.countdown < timeRange.x && manager.countdown > timeRange.y);
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && active && manager.gaming) {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit)) {
                if (raycastHit.collider.CompareTag("Respawn")) {
                    collected = true;
                }
            }
        }
    }
}