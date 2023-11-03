using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.FilePathAttribute;
using ESarkis;

public class FloodFill : MonoBehaviour
{
    public MouseStuff mouseStuff;
    [SerializeField] private Tilemap _tM;
    [SerializeField] private Tile _tile1;
    [SerializeField] private Tile _tileFill;
    public Set reached = new Set();
    [SerializeField] private PriorityQueue<Vector3Int> _frontier = new PriorityQueue<Vector3Int>();
    private Dictionary<Vector3Int, Vector3Int> _came_from = new Dictionary<Vector3Int, Vector3Int>();
    private Vector3Int _previous;
    public Vector3Int startingPoint;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CumJar();
            Debug.Log("IMBOUTTOCUM");
        }       
    }
    void CumJar()
    {
        _frontier.Enqueue(startingPoint,0);
        //Funcion que devuelva lista de Vector3.int, a partir de current
        while (_frontier.Count > 0)
        {
            Vector3Int current = _frontier.Dequeue();

            //Aqu� pongo Early Exit
            if (current == mouseStuff._end)
            {
                break;
            }

            foreach (Vector3Int neighbors in getNeighbours(current))
            {
                if ( !_came_from.ContainsKey(neighbors))
                {
                    //if next not in _came_from:
                    if (neighbors != null&& _tM.GetSprite(neighbors) != null)
                    {
                        _frontier.Enqueue(neighbors,0);                      
                        //came_from reemplaza a AddReached
                        _came_from[neighbors] = current;
                        _tM.SetTile(neighbors, _tileFill);
                        //dicTionary.Add(neighbors) = current;
                    }
                }
            }
        }
        DrawPath(mouseStuff._end);
    }

    List<Vector3Int> getNeighbours(Vector3Int current)
    {
        List<Vector3Int> vecinos = new List<Vector3Int>();

        Vector3Int derecha = new Vector3Int(current.x + 1, current.y, current.z);
        Vector3Int izq = new Vector3Int(current.x - 1, current.y, current.z);
        Vector3Int abajo = new Vector3Int(current.x , current.y-1, current.z);
        Vector3Int arriba= new Vector3Int(current.x , current.y+1, current.z);

        vecinos.Add(derecha);
        vecinos.Add(izq);
        vecinos.Add(abajo);
        vecinos.Add(arriba);

        return vecinos;
    }
   
    //Aqu� tengo que a�adir los vecinos que alcance para no tomarlos en cuenta en siguientes pruebas
    /*public void AddReached(Vector3Int Reached)
    {    
        reached.set.Add(Reached);
    }*/

    public void DrawPath(Vector3Int xd)
    {
        _previous = _came_from[mouseStuff._end];
        mouseStuff.tM.SetTile(mouseStuff._end, _tile1);

        while (_previous!=mouseStuff._start)
        {
            mouseStuff.tM.SetTile(_previous, _tile1);
            _previous = _came_from[_previous];
        }
    }

}
