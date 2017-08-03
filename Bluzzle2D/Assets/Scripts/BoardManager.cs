using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.
public class BoardManager : MonoBehaviour
	{
		// Using Serializable allows us to embed a class with sub properties in the inspector.
		[Serializable]
		public class Count
		{
			public int minimum; 			//Minimum value for our Count class.
			public int maximum; 			//Maximum value for our Count class.
			
			
			//Assignment constructor.
			public Count (int min, int max)
			{
				minimum = min;
				maximum = max;
			}
		}
    float timep = 0.0f;
    private int testcolor = 0;
		public int columns = 12; 										//Number of columns in our game board.
		public int rows = 12;											//Number of rows in our game board.
		public GameObject exit;											//Prefab to spawn for exit.
		public GameObject[] floorTiles;									//Array of floor prefabs.
		public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.

        private string textdisplay;
		private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
		private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.
        private List<TileType> GameTiles = new List<TileType>();
        private int TypeHolder = 0;
        private int PathSize = 0;
        private int StartPoint = 0;
        private int EndPoint = 0;


    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList ()
		{
			//Clear our list gridPositions.
			gridPositions.Clear ();
			
			//Loop through x axis (columns).
			for(int x = 1; x < columns-1; x++)
			{
				//Within each column, loop through y axis (rows).
				for(int y = 1; y < rows-1; y++)
				{
					//At each index add a new Vector3 to our list with the x and y coordinates of that position.
					gridPositions.Add (new Vector3(x, y, 0f));
				}
			}
		}
		
		
		//Sets up the outer walls and floor (background) of the game board.
		void BoardSetup ()
		{
			//Instantiate Board and set boardHolder to its transform.
            if(boardHolder != null)
            {
                Destroy(boardHolder.gameObject);
                GameTiles.Clear();
                testcolor = 0;
                boardHolder = null;
            }
			boardHolder = new GameObject ("Board").transform;
			
			//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
			for(int x = -1; x < columns + 1; x++)
			{
				//Loop along y axis, starting from -1 to place floor or outerwall tiles.
				for(int y = -1; y < rows + 1; y++)
				{
					//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
					GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
                //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                    TypeHolder = 6;
                }
 
                
					
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					GameObject instance =
						Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;

                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.GetComponent<TileType>().set_pos(x, y);
                if (TypeHolder == 6)
                {
                    instance.GetComponent<TileType>().set_type(6);
                    TypeHolder = 0;
                }
                if (x >= 0 && y >= 0 && columns > x && rows > y)
                    GameTiles.Add(instance.GetComponent<TileType>());
                instance.transform.SetParent (boardHolder);
				}
			}
        for(int x = 0; x < GameTiles.Count; x++)
        {
            if(x % rows != rows-1)
                GameTiles[x].set_neighbor(GameTiles[x + 1], 0);
            if(x % rows != 0)
                GameTiles[x].set_neighbor(GameTiles[x - 1], 1);
            if(x + rows < GameTiles.Count)
                GameTiles[x].set_neighbor(GameTiles[x + rows], 2);
            if (x - rows >= 0)
                GameTiles[x].set_neighbor(GameTiles[x - rows], 3);
        }
        GameTiles[Mathf.RoundToInt((columns * rows) / 2)].set_type(6);

    }

    void pathfind_to_end(TileType Start, TileType Endpoint)
    {
        List<TileType> openSet = new List<TileType>();
        HashSet<TileType> closedSet = new HashSet<TileType>();
        TileType Neighbor;
        
        openSet.Add(Start);
        while (openSet.Count > 0)
        {
            TileType currentTile = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost() < currentTile.fCost() || openSet[i].fCost() == currentTile.fCost() && openSet[i].hCost < currentTile.hCost)
                {
                    currentTile = openSet[i];
                }
            }
            openSet.Remove(currentTile);
            closedSet.Add(currentTile);
            if (currentTile == Endpoint)
            {
                RevealPath(Start, Endpoint);
                return;
            }
            for (int i = 0; i < 4; i++)
            {
                Neighbor = currentTile.get_neighbor(i);
                if (Neighbor != currentTile && Neighbor.TileTypeNum()!=6 && !closedSet.Contains(Neighbor))
                {
                    int CostToMovetoNeighbor = currentTile.gCost + get_Distance(currentTile, Neighbor);
                    if(CostToMovetoNeighbor < Neighbor.gCost || !openSet.Contains(Neighbor))
                    {
                        Neighbor.gCost = CostToMovetoNeighbor;
                        Neighbor.hCost = get_Distance(Neighbor, Endpoint);
                        Neighbor.parent = currentTile;

                        if (!openSet.Contains(Neighbor))
                            openSet.Add(Neighbor);
                    }
                }
            }
        }
    }
    private void RevealPath(TileType Startpoint, TileType Endpoint)
    {
        Debug.Log("Greetings");
        TileType p = Endpoint;
        Endpoint.set_type(5);
        PathSize = 0;
        while (p != Startpoint)
        {
            p = p.parent;
            p.set_type(3);
            PathSize++;
        }
        Debug.Log(PathSize);
        p.set_type(4);
    }
    private int get_Distance(TileType A, TileType B)
    {
        int cost_x;
        int cost_y;
        cost_x = Mathf.Abs(A.pos_x() - B.pos_x());
        cost_y = Mathf.Abs(A.pos_y() - B.pos_y());
        return 10*(cost_x + cost_y);
    }
    private bool CheckValidNode(int NodeToCheck)
        {
            int MainPath = 0;
            if (NodeToCheck < GameTiles.Count && NodeToCheck >= 0)
            {
                if (GameTiles[NodeToCheck].TileTypeNum() == 0)
                {
                   if((NodeToCheck + 1) < GameTiles.Count)
                        if (GameTiles[NodeToCheck + 1].TileTypeNum() == 3)
                            MainPath++;
                   if ((NodeToCheck - 1) >= 0)
                        if (GameTiles[NodeToCheck - 1].TileTypeNum() == 3)
                            MainPath++;
                   if ((NodeToCheck + rows) < GameTiles.Count)
                        if (GameTiles[NodeToCheck + rows].TileTypeNum() == 3)
                            MainPath++;
                   if ((NodeToCheck - rows) >= 0)
                        if (GameTiles[NodeToCheck - rows].TileTypeNum() == 3)
                            MainPath++;
                    if (MainPath > 1)
                        return false;
                    else
                        return true;
            }
                else
                    return false;
            }
            else
                return false;
        }
    private void cleanBoard()
        {
            for (int i = 0; i < GameTiles.Count; i++)
                GameTiles[i].set_type(0);
        }
    private void createBadZones()
        {
            int rand;
            for (int i = 0; i < GameTiles.Count; i++)
            {
                if(GameTiles[i].TileTypeNum() != 3 && GameTiles[i].TileTypeNum() != 6 && GameTiles[i].TileTypeNum() != 5)
                {
                    rand = Random.Range(0, 10);
                    if(rand == 0)
                    {
                        GameTiles[i].set_type(6);
                    }
                    else
                    {
                        GameTiles[i].set_type(2);
                    }
                }
            }
        }
	private void createWalls()
        {
            int rand;
            for (int i = 0; i < GameTiles.Count; i++)
            {
                if (GameTiles[i].TileTypeNum() != 3 && GameTiles[i].TileTypeNum() != 6 && GameTiles[i].TileTypeNum() != 5)
                {
                    rand = Random.Range(0, 5);
                    if (rand == 0)
                    {
                        GameTiles[i].set_type(6);
                    }
                }
        }
    }
		
		//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void SetupScene (int level)
		{
			//Creates the outer walls and floor.
			BoardSetup ();
        PathSize = 0;
        while (PathSize < (level * 4 + 1))
        {
            PathSize = 0;

            StartPoint = Random.Range(0, GameTiles.Count);
            while (GameTiles[StartPoint].TileTypeNum() != 0)
                StartPoint = Random.Range(0, GameTiles.Count);

            EndPoint = Random.Range(0, GameTiles.Count);
            while (GameTiles[EndPoint].TileTypeNum() != 0)
                EndPoint = Random.Range(0, GameTiles.Count);

            createWalls();
            while (get_Distance(GameTiles[StartPoint], GameTiles[EndPoint]) < level * 4 + 1 && GameTiles[StartPoint].TileTypeNum() != 6 && GameTiles[EndPoint].TileTypeNum() != 6)
            {
                StartPoint = Random.Range(0, GameTiles.Count);
                EndPoint = Random.Range(0, GameTiles.Count);
            }
            pathfind_to_end(GameTiles[StartPoint], GameTiles[EndPoint]);
            if(PathSize < (level * 4 + 1))
            {
                cleanBoard();
            }
        }
        createBadZones();

        if (rows == columns)
            rows++;
        else
            columns++;
        GameTiles[StartPoint].set_type(4);
        GameTiles[StartPoint].is_Walked();
        TileType.Player = GameTiles[StartPoint];
    }
}