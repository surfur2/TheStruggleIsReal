using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] energyTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        Debug.Log("Setup");
        //boardHolder = new GameObject("Board").transform;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y <  rows; y++)
            {
                Quaternion rotationAngle;
                GameObject toInstantiate;
                toInstantiate = TileToInstantiate(x, y, out rotationAngle);

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), rotationAngle) as GameObject;

                //toInstantiate.SetParent(gameObject.transform);
                toInstantiate.name = "(" + x.ToString() + "," + (y + 1).ToString() + ")";
            }
        }

    }

    #region CreateWalls
    private void CreatWalls()
    {
        
    }
    #endregion


    #region TileToInstantiate
    private GameObject TileToInstantiate(int x, int y, out Quaternion angle)
    {
        GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)]; ;
        angle = Quaternion.identity;

        if (x == 0 && y == 0)
        {
            toInstantiate = outerWallTiles[0];
        }
        else if (x == (columns - 1) && y == (rows - 1))
        {
            toInstantiate = outerWallTiles[0];
        }
        else if (x == 0 && y == (rows - 1))
        {
            toInstantiate = outerWallTiles[0];
        }
        else if (x == (columns - 1) && y == 0)
        {
            toInstantiate = outerWallTiles[0];
        }
        else if (x == 0)
        {
            toInstantiate = outerWallTiles[1];
            angle = Quaternion.Euler(0, 0, 270);
        }
        else if (y == 0)
        {
            toInstantiate = outerWallTiles[1];
        }
        else if (x == (columns - 1))
        {
            toInstantiate = outerWallTiles[1];
            angle = Quaternion.Euler(0, 0, 90);
        }
        else if (y == (rows - 1))
        {
            toInstantiate = outerWallTiles[1];
            angle = Quaternion.Euler(0, 0, 180);
        }

        return toInstantiate;
    }
    #endregion

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);

        Vector3 randomPosition = gridPositions[randomIndex];

        gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();

            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene()
    {
        BoardSetup();

       //InitialiseList();

        //LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);

        //LayoutObjectAtRandom(enemyTiles, foodCount.minimum, foodCount.maximum);
        //int enemyCount = (int)Mathf.Log(level, 2f);
        //LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        //Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }

    void Start()
    {
        SetupScene();
    }

    void Update()
    {

    }
}