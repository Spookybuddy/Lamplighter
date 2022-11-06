using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    private GameManager manager;
    public Camera mainCam;
    public GameObject planet;

    //Values to be entered
    public int ID;
    public bool dayNight;
    public Vector2 timeRange;
    public AudioClip clicked;

    private bool active;
    private MeshRenderer mesh;
    public bool selected;

    void Start()
    {
        manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && active && manager.gaming && !selected) {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit)) {
                if (raycastHit.collider.CompareTag("Respawn") && raycastHit.collider.gameObject == gameObject) {
                    selected = true;
                    manager.tutored2 = true;
                    manager.Selection(gameObject, ID, clicked);
                }
            }
        }

        active = ((manager.dayNight == dayNight) && manager.countdown < timeRange.x && manager.countdown > timeRange.y);
        if (!active) selected = false;
        mesh.enabled = active;

        //When selected move to center cam and deparent from planet
        if (selected) {
            transform.parent = null;
        } else {
            transform.parent = planet.transform;
        }
    }
}