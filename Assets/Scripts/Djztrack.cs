using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.FilePathAttribute;
using ESarkis;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Djztrack : MonoBehaviour
{

    public Tilemap tM;
    [SerializeField] private PriorityQueue<Vector3Int> frontier = new PriorityQueue<Vector3Int>();
    public Vector3Int startingPoint;
    public Vector3Int tileCord;
    public Tile tile1;
    public Tile tileFill;
    public Set reached = new Set();
    public TileBase pasto;
    public TileBase arena;
    public TileBase veneno;  
    private Dictionary<Vector3Int, Vector3Int> came_from = new Dictionary<Vector3Int, Vector3Int>();
    private Dictionary<Vector3Int, int> cost_so_far = new Dictionary<Vector3Int, int>();
    public Dictionary<TileBase, int> TileCost= new Dictionary<TileBase, int>();  
    private Vector3Int _previous;

    public MouseStuff mouseStuff;

    private void Start()
    {
        //&came_from.Add(startingPoint,startingPoint);
        //cost_so_far.Add(startingPoint, 0);
        TileCost.Add(pasto, 1);
        TileCost.Add(arena, 5);
        TileCost.Add(veneno, 8);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CumJar();
            Debug.Log("IMBOUTTOCUM");
           
        }

    }
    void CumJar()
    {
                //came_from.Add(mouseStuff._end, mouseStuff._end);  
        came_from.Add(startingPoint, startingPoint);
      
        //cost_so_far.Add(startingPoint,0);
        frontier.Enqueue(startingPoint, 0);
        cost_so_far[startingPoint] = 0;
        //Funcion que devuelva lista de Vector3.int, a partir de current
        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();

            //Early Exit
            if (current == mouseStuff._end)
            {
                break;
                //Debug.Log("Ya llegue");
            }
            foreach (Vector3Int neighbors in getNeighbours(current))
            {
                if (!came_from.ContainsKey(neighbors))
                {
                    var new_cost = cost_so_far[current] + Costos(neighbors);
                    if (!cost_so_far.ContainsKey(neighbors) || new_cost < cost_so_far[current])
                    {
                        //if next not in _came_from:
                        if (neighbors != null && tM.GetSprite(neighbors) != null)
                        {

                            cost_so_far[neighbors] = new_cost;
                            float priority = new_cost;                          
                            frontier.Enqueue(neighbors, priority);
                            AddReached(neighbors);
                            tM.SetTile(neighbors, tileFill);
                            came_from.Add(neighbors, current);
                          
                            //dicTionary.Add(neighbors) = current;
                        }
                    }
                }
                    
                
            }
        }

        DrawPath();
        tM.SetTile(mouseStuff._end, tile1);

    }

    List<Vector3Int> getNeighbours(Vector3Int current)
    {
        List<Vector3Int> vecinos = new List<Vector3Int>();

        Vector3Int derecha = new Vector3Int(current.x + 1, current.y, current.z);
        Vector3Int izq = new Vector3Int(current.x - 1, current.y, current.z);
        Vector3Int abajo = new Vector3Int(current.x, current.y - 1, current.z);
        Vector3Int arriba = new Vector3Int(current.x, current.y + 1, current.z);

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


    public void DrawPath()
    {
        _previous = came_from[mouseStuff._end];
       mouseStuff.tM.SetTile(mouseStuff._end, tile1);

        while (_previous != mouseStuff._start)
        {
            mouseStuff.tM.SetTile(_previous, tile1);
            _previous = came_from[_previous];
        }
    }

    private int Costos(Vector3Int neighbour)
    {
        

        if (tM.GetTile(neighbour) == pasto)
        {
            return 1;
        }
        if (tM.GetTile(neighbour) == arena)
        {
            return 30;
        }
        if (tM.GetTile(neighbour) == veneno)
        {
            return 100;
        }

        return 10000;
        
    }  
}

