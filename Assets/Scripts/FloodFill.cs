using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill : MonoBehaviour
{
    
    private Queue<Vector3Int> frontier = new Queue<Vector3Int>();
    public Vector3 startingPoint;
    public Set reached = new Set();


    public void Update()
    {
        while(frontier.Count > 0)
        {
            Vector3Int current= frontier.Dequeue();
            foreach (Vector3Int neighbor in getNeighbours(current))
            {
                
            }

        }
        
       //Funcion que devuelva lista de Vector3.int, a partir de current
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

    /*
    public Vector3Int getNeighbourZ(Vector3Int current)
    {
        List<Vector3Int> neighbours = getNeighbourZ(current);
        return current;
    }*/


}
