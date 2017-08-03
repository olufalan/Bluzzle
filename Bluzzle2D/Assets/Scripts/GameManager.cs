using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;
    private int level = 1;
    private float timep = 0f;
    private float timeA = 0f;
    private bool loading;
    public int PlayerLives = 5;
    private bool pauseControl = false;
    private bool NewLevelPause = true;
    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }
    void InitGame()
    {
        boardScript.SetupScene(level);
        NewLevelPause = true;
    }
    // Update is called once per frame

    void Update()
    {
        //for pausing the menu
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("PauseMenu");

        }
        /*
        if (!pauseControl && !NewLevelPause)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                TileType.Player.is_Walked();
                if (TileType.Player.get_neighbor(3).TileTypeNum() == 2)
                {
                    TileType.Player.get_neighbor(3).is_Walked();
                    PlayerLives--;
                }
                else if (TileType.Player.get_neighbor(3).TileTypeNum() == 1)
                {
                    TileType.Player.is_Walked();
                }
                else if (TileType.Player.get_neighbor(3).TileTypeNum() == 6)
                {
                    TileType.Player.is_Walked();
                }
                else if (TileType.Player.get_neighbor(3).TileTypeNum() == 5)
                {
                    pauseControl = true;
                    TileType.Player = TileType.Player.get_neighbor(3);
                    TileType.Player.is_Walked();
                }
                else
                {
                    TileType.Player = TileType.Player.get_neighbor(3);
                    TileType.Player.is_Walked();
                }
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                TileType.Player.is_Walked();
                if (TileType.Player.get_neighbor(1).TileTypeNum() == 2)
                {
                    TileType.Player.get_neighbor(1).is_Walked();
                    PlayerLives--;
                }
                else if (TileType.Player.get_neighbor(1).TileTypeNum() == 1)
                {
                    TileType.Player.is_Walked();
                }
                else if (TileType.Player.get_neighbor(1).TileTypeNum() == 6)
                {
                    TileType.Player.is_Walked();
                }
                else if (TileType.Player.get_neighbor(1).TileTypeNum() == 5)
                {
                    pauseControl = true;
                    TileType.Player = TileType.Player.get_neighbor(1);
                    TileType.Player.is_Walked();
                }
                else
                {
                    TileType.Player = TileType.Player.get_neighbor(1);
                    TileType.Player.is_Walked();
                }
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                TileType.Player.is_Walked();
                if (TileType.Player.get_neighbor(2).TileTypeNum() == 2)
                {
                    TileType.Player.get_neighbor(2).is_Walked();
                    PlayerLives--;
                }
                else if (TileType.Player.get_neighbor(2).TileTypeNum() == 1)
                {
                    TileType.Player.is_Walked();
                }
                else if (TileType.Player.get_neighbor(2).TileTypeNum() == 6)
                {
                    TileType.Player.is_Walked();
                }
                else if (TileType.Player.get_neighbor(2).TileTypeNum() == 5)
                {
                    pauseControl = true;
                    TileType.Player = TileType.Player.get_neighbor(2);
                    TileType.Player.is_Walked();
                }
                else
                {
                    TileType.Player = TileType.Player.get_neighbor(2);
                    TileType.Player.is_Walked();
                }
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                TileType.Player.is_Walked();
                if (TileType.Player.get_neighbor(0).TileTypeNum() == 2)
                {
                    TileType.Player.get_neighbor(0).is_Walked();
                    PlayerLives--;
                }
                else if (TileType.Player.get_neighbor(0).TileTypeNum() == 1)
                {
                    TileType.Player.is_Walked();
                }
                else if (TileType.Player.get_neighbor(0).TileTypeNum() == 6)
                {
                    TileType.Player.is_Walked();
                }
                else if (TileType.Player.get_neighbor(0).TileTypeNum() == 5)
                {
                    pauseControl = true;
                    TileType.Player = TileType.Player.get_neighbor(0);
                    TileType.Player.is_Walked();
                }
                else
                {
                    TileType.Player = TileType.Player.get_neighbor(0);
                    TileType.Player.is_Walked();
                }
            }
        }
        else
        {
            if (!NewLevelPause)
                timep += UnityEngine.Time.deltaTime;
            else
                timeA += UnityEngine.Time.deltaTime;
            if (timep >= 3f)
            {
                if (!loading)
                    ILoadLevel();
                level++;
                boardScript.SetupScene(level);
                timep = 0.0f;
                pauseControl = false;
                NewLevelPause = true;
            }
            if (timeA >= 6.5f)
            {
                timeA = 0;
                NewLevelPause = false;
            }
        }
        */

    }
    IEnumerator ILoadLevel()
    {
        //Don't forget to lock the coroutine from multiple calls.
        loading = true;

        //Calling this gets you the progress, completion and some stuff like that to tinker with while loading.
        AsyncOperation operation = SceneManager.LoadSceneAsync("level");
        while (!operation.isDone)
        {

        }
        //From here you can say something like -   while(!operation.isDone) progressText = operation.progress;

        //Finally, change scenes once we're done loading.
        loading = false;
        yield return operation;
    }
}
