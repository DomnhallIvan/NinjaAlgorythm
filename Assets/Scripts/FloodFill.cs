using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloodFill : MonoBehaviour
{
    public Tilemap tM;
    private Queue<Vector3Int> frontier = new Queue<Vector3Int>();
    public Vector3Int startingPoint;
    public Set reached = new Set();
    public Tile tile1;


    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CumJar();
        }
       
    }
    void CumJar()
    {
        //Funcion que devuelva lista de Vector3.int, a partir de current
        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();
            foreach (Vector3Int neighbors in getNeighbours(current))
            {
                if (!reached.set.Contains(neighbors))
                {
                    if (neighbors != null)
                    {
                        AddReached(neighbors);
                        frontier.Enqueue(neighbors);
                        tM.SetTile(neighbors,tile1);

                    }
                }
            }
        }
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
        Reached = startingPoint;   
        reached.set.Add(Reached);
    }
    
   
    /*
    public Vector3Int getNeighbourZ(Vector3Int current)
    {
        List<Vector3Int> neighbours = getNeighbourZ(current);
        return current;
    }*/


}
