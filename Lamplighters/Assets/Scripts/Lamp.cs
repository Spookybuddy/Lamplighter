using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public bool hit;
    public bool onOff;
    public GameObject lampLight;
    public Camera mainCam;
    private GameManager manager;

    void Start()
    {
        manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        Fluctuate();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !hit && manager.countdown < 5) {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit)) {
                if (raycastHit.collider.CompareTag("Finish")) {
                    hit = true;
                    StartCoroutine(Delay());
                    Fluctuate();
                }
            }
        }
    }

    private void Fluctuate()
    {
        onOff = !onOff;
        manager.dayNight = !manager.dayNight;
        lampLight.SetActive(onOff);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        hit = false;
    }
}