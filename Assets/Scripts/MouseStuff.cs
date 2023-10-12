using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseStuff : MonoBehaviour
{
    public Tilemap TM;


    private void Update()
    {
        var mousePos = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        var location = TM.WorldToCell(mousePos);
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(location);
        }
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log(location + "Origen");
        }
    }
}
