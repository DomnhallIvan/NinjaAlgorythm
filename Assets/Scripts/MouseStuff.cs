using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseStuff : MonoBehaviour
{
    public Tilemap tM;
    public FloodFill fF;
    public Tile tile1; //tileStart
    public Tile tileJimmyM; //tileEnd



    private void Update()
    {
        var mousePos = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        var location = tM.WorldToCell(mousePos);
        location.z = 0;


        if(Input.GetMouseButtonDown(0) && tM.GetSprite(location) != null)
        {
            
            Debug.Log(location);
            tM.SetTile(location, tile1);
        }
        if(Input.GetMouseButtonDown(1)&&tM.GetSprite(location)!=null)
        {
            Debug.Log(location + "Origen");
            fF.startingPoint = location;
            tM.SetTile(location, tileJimmyM);

        }
    }
}
