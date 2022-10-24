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
    }

    void FixedUpdate()
    {
        lampLight.SetActive(onOff);
        //Light can only be turned on/off when its 5 seconds to Sunrise/set
        if (Input.GetMouseButton(0) && !hit && manager.countdown < 5 && manager.gaming) {
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
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        hit = false;
    }
}