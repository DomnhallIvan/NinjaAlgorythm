using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.FilePathAttribute;

public class FloodFill : MonoBehaviour
{
    public Tilemap tM;
    [SerializeField] private Queue<Vector3Int> frontier = new Queue<Vector3Int>();
    public Vector3Int startingPoint;
    public Set reached = new Set();
    public Tile tile1;
    public Tile tileFill;
    private Dictionary<Vector3Int, Vector3Int> came_from = new Dictionary<Vector3Int, Vector3Int>();
    private Vector3Int _previous;

    public MouseStuff mouseStuff;

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
        frontier.Enqueue(startingPoint);
        //Funcion que devuelva lista de Vector3.int, a partir de current
        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();

            //Aquí pongo Early Exit
            if (current == mouseStuff._end)
            {
                break;
            }

            foreach (Vector3Int neighbors in getNeighbours(current))
            {
                

                if ( !came_from.ContainsKey(neighbors))
                {
                    if (neighbors != null&& tM.GetSprite(neighbors) != null)
                    {
                        AddReached(neighbors);
                        frontier.Enqueue(neighbors);
                        tM.SetTile(neighbors,tileFill);
                        came_from[neighbors] = current;
                       
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
   

    //Aquí tengo que añadir los vecinos que alcance para no tomarlos en cuenta en siguientes pruebas
    public void AddReached(Vector3Int Reached)
    {    
        reached.set.Add(Reached);
    }


    public void DrawPath(Vector3Int xd)
    {
        _previous = came_from[mouseStuff._end];
        mouseStuff.tM.SetTile(mouseStuff._end, tile1);

        while (_previous!=mouseStuff._start)
        {
            mouseStuff.tM.SetTile(_previous, tile1);
            _previous = came_from[_previous];
        }
    }
}
