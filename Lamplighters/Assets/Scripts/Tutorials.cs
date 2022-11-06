using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour
{
    public Vector3 start, end;

    void Update()
    {
        float clamp = Mathf.Clamp(Vector3.Distance(transform.localPosition, end), 0.1f, 1.25f);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, end, Time.deltaTime * 4 * clamp);
        if (Vector3.Distance(transform.localPosition, end) < 0.025f) transform.localPosition = start;
    }
}