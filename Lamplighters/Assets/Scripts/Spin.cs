using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Camera view;
    public Rigidbody rig;
    public GameManager manage;
    private int swipes = 0;

    //Raycasts
    private Vector3 prevHit;
    private Vector3 invert;

    void Update()
    {
        //Reset when mouse is released
        if (!Input.GetMouseButton(0)) prevHit = Vector3.zero;
    }

    void OnMouseDrag()
    {
        Ray ray = view.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 50, 3)) {
            //Ignore first click
            if (prevHit == Vector3.zero) prevHit = raycastHit.point;

            //Spin if the raycast has moved
            if (Vector3.Distance(prevHit, raycastHit.point) > 0.05f) {
                invert = (raycastHit.point - prevHit);
                invert = new Vector3(invert.y, -invert.x, invert.z);
                rig.AddTorque(invert * 50 * Vector3.Distance(prevHit, raycastHit.point), ForceMode.Impulse);
                prevHit = raycastHit.point;
                if (manage.gaming) swipes++;
                if (swipes > 20) manage.tutored = true;
            }
        }
    }

    void OnMouseExit()
    {
        prevHit = Vector3.zero;
    }
}
