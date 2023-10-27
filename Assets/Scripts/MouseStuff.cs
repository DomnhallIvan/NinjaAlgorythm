using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseStuff : MonoBehaviour
{
    public Tilemap tM;
    public FloodFill fF;
    public Djztrack Dj;
    public Tile tile1; //tileStart
    public Tile tileJimmyM; //tileEnd
    public Tile tileFill;
    public Vector3Int _start;
    public Vector3Int _end;


    private void Update()
    {
        var mousePos = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        var location = tM.WorldToCell(mousePos);
        location.z = 0;


        if(Input.GetMouseButtonDown(0) && tM.GetSprite(location) != null)
        {
           if(_end != location && tM.GetSprite(location) != null)
            {
                tM.SetTile(_end, tileFill);
            }
            Debug.Log(location);
            tM.SetTile(location, tile1);
            _end = location;
            
        }
        


        if (Input.GetMouseButtonDown(1)&&tM.GetSprite(location)!=null)
        {
            if (_start != location && tM.GetSprite(location) != null)
            {
                tM.SetTile(_start, tileFill);
            }
           // Debug.Log(location + "Origen");
            fF.startingPoint = location;
            Dj.tileCord = location;
            tM.SetTile(location, tileJimmyM);
            _start = location;

        }
    }
}
