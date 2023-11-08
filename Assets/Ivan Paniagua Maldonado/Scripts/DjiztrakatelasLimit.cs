using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ESarkis;

public class DjiztrakatelasLimit : MonoBehaviour
{
    public MouseStuff mouseStuff;
    [SerializeField] private Tilemap _tM;
    [SerializeField] private Tile _tile1;
    [SerializeField] private Tile _tileFill;
    public Set reached = new Set();
    [SerializeField] private TileBase _pasto;
    [SerializeField] private TileBase _arena;
    [SerializeField] private TileBase _veneno;
    [SerializeField] private PriorityQueue<Vector3Int> _frontier = new PriorityQueue<Vector3Int>();
    private Dictionary<Vector3Int, Vector3Int> _came_from = new Dictionary<Vector3Int, Vector3Int>();
    private Dictionary<Vector3Int, float> _cost_so_far = new Dictionary<Vector3Int, float>();
    private Vector3Int _previous;
    public Vector3Int startingPoint; //Este será asignado por el MouseStuff



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
        _frontier.Enqueue(startingPoint, 0);
        _came_from[startingPoint] = startingPoint;
        _cost_so_far[startingPoint] = 0;

        //Funcion que devuelva lista de Vector3.int, a partir de current
        while (_frontier.Count > 0)
        {
            Vector3Int current = _frontier.Dequeue();

            //Aquí pongo Early Exit
            if (current == mouseStuff._end)
            {
                break;
            }

            foreach (Vector3Int neighbors in getNeighbours(current))
            {
                if (!_came_from.ContainsKey(neighbors))
                {
                    var new_cost = _cost_so_far[current] + Costos(neighbors);
                    //if next not in _came_from:
                    if (_tM.HasTile(neighbors) && neighbors != null && _tM.GetSprite(neighbors) != null)
                    {
                        if (!_cost_so_far.ContainsKey(neighbors) || new_cost < _cost_so_far[neighbors])
                        {
                            _cost_so_far[neighbors] = new_cost;
                            float priority = new_cost;
                            _frontier.Enqueue(neighbors, priority);
                            _came_from[neighbors] = current;
                            _tM.SetTile(neighbors, _tileFill);

                        }
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
        Vector3Int abajo = new Vector3Int(current.x, current.y - 1, current.z);
        Vector3Int arriba = new Vector3Int(current.x, current.y + 1, current.z);

        vecinos.Add(derecha);
        vecinos.Add(izq);
        vecinos.Add(abajo);
        vecinos.Add(arriba);

        return vecinos;
    }

    public void DrawPath(Vector3Int xd)
    {
        //Hago el caminito
        _previous = _came_from[xd];
        mouseStuff.tM.SetTile(xd, _tile1);

        while (_previous != mouseStuff._start)
        {
            mouseStuff.tM.SetTile(_previous, _tile1);
            _previous = _came_from[_previous];
        }
    }

    private int Costos(Vector3Int neighbour)
    {
        //No funciono el diccionario XD
        if (_tM.GetTile(neighbour) == _pasto)
        {
            return 1;
        }
        if (_tM.GetTile(neighbour) == _arena)
        {
            return 30;
        }
        if (_tM.GetTile(neighbour) == _veneno)
        {
            return 100;
        }

        return 10000;
    }
}

