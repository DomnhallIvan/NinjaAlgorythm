/*using System;
using System.Collections;
using System.Collections.Generic;
using ESarkis;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Djistra : MonoBehaviour
{
    public Vector3Int startingPoint;
    public Vector3Int EndPoint;
    public Set Reached = new Set();
    public Flood flood;
    public Tilemap tiles;
    public TileBase tileBaseFlood;
    public TileBase basePath;
    public Dictionary<Vector3Int, Vector3Int> came_from = new Dictionary<Vector3Int, Vector3Int>();
    public Dictionary<Vector3Int, float> cost_so_far = new Dictionary<Vector3Int, float>();
    public PriorityQueue<Vector3Int> Frontier = new PriorityQueue<Vector3Int>();
    public TileBase grass;
    public TileBase water;
    public TileBase lava;


    private void Update()
    {
        startingPoint = flood.cellStart;
        EndPoint = flood.cellEnd;
        if (Input.GetKeyDown("m"))
        {
            FloodFill();
        }
    }

    private List<Vector3Int> getneighbours(Vector3Int current)
    {
        List<Vector3Int> neighbours = new List<Vector3Int>();
        neighbours.Add(new Vector3Int(current.x + 1, current.y, current.z));
        neighbours.Add(new Vector3Int(current.x - 1, current.y, current.z));
        neighbours.Add(new Vector3Int(current.x, current.y + 1, current.z));
        neighbours.Add(new Vector3Int(current.x, current.y - 1, current.z));
        return neighbours;
    }

    private void FloodFill()
    {
        Frontier.Enqueue(startingPoint, 0);
        came_from.Add(startingPoint, startingPoint);
        cost_so_far.Add(startingPoint, 0);

        while (Frontier.Count > 0)
        {
            var current = Frontier.Dequeue();
            if (current == EndPoint)
            {
                break;
            }
            List<Vector3Int> neighbours = getneighbours(current);
            foreach (var neighbour in neighbours)
            {
                var new_cost = cost_so_far[current] + GetCost(neighbour);
                if (!Reached.set.Contains(neighbour) && tiles.HasTile(neighbour) && !came_from.ContainsKey(neighbour))
                {
                    if (!cost_so_far.ContainsKey(neighbour) || new_cost < cost_so_far[neighbour])
                    {
                        cost_so_far[neighbour] = new_cost;
                        float priority = new_cost;
                        Reached.add(neighbour);
                        Frontier.Enqueue(neighbour, priority);
                        //tiles.SetTile(neighbour, tileBaseFlood);
                        came_from.Add(neighbour, current);
                    }
                }
            }
        }
        DrawPath();
        tiles.SetTile(EndPoint, basePath);
    }

    private void DrawPath()
    {
        var tile = came_from[EndPoint];
        while (tile != startingPoint)
        {
            tiles.SetTile(tile, basePath);
            tile = came_from[tile];
        }
    }

    private int GetCost(Vector3Int neighbour)
    {
        if (tiles.GetTile(neighbour) == grass)
        {
            return 1;
        }
        if (tiles.GetTile(neighbour) == water)
        {
            return 30;
        }
        if (tiles.GetTile(neighbour) == lava)
        {
            return 100;
        }

        return 10000;
    }

}*/