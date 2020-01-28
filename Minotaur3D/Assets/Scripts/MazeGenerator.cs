using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cell[,] grid;

        // tutaj musicie wczytać argumenty przesłane z uruchomieniem: ścieżke do json'a, start, koniec

        // string path = "D:\\Semestr5\\IO\\Minotaur\\Minotaur\\Maze\\10-17-2019_2-48-16.json";
        string path = "";
        string json = "";

        using (StreamReader r = new StreamReader(path))
        {
            json = r.ReadToEnd();
        }

        grid = JsonConvert.DeserializeObject<Cell[,]>(json);
        
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        //////////////////////////////////////////////////////
        bool[,] horizontal = new bool[width , height + 1];
        bool[,] vertical = new bool[width + 1, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                if (grid[i, j].Walls[0])
                    horizontal[i, j] = true;
                if (grid[i, j].Walls[1])
                    vertical[i + 1, j] = true;
                if (grid[i, j].Walls[2])
                    horizontal[i, j + 1] = true;
                if (grid[i, j].Walls[3])
                    vertical[i, j] = true;
            }

        for (int i = 0; i < width; i++)
            for(int j = 0; j < height; j++)
            {
                GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Plane);
                temp.transform.position = new Vector3(i * 10, 0, j * 10);
                temp.GetComponent<Renderer>().material.color = Color.red;
            }

        for (int i = 0; i < vertical.GetLength(0); i++)
            for (int j = 0; j < vertical.GetLength(1); j++)
            {
                if(vertical[i, j])
                {
                    GameObject Wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Wall.transform.position = new Vector3(i * 10 - 5, 5, j * 10);
                    Wall.transform.localScale = new Vector3(0.05f, 10, 10);
                    Wall.GetComponent<Renderer>().material.color = Color.magenta;
                }
            }

        for (int i = 0; i < horizontal.GetLength(0); i++)
            for (int j = 0; j < horizontal.GetLength(1); j++)
            {
                if (horizontal[i, j])
                {
                    GameObject Wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    Wall.transform.position = new Vector3(i * 10, 5, j * 10 - 5);
                    Wall.transform.localScale = new Vector3(10, 10, 0.05f);
                    Wall.GetComponent<Renderer>().material.color = Color.green;
                }
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
