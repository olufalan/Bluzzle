using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileType : MonoBehaviour
{
    public Sprite[] TileArt;
    public static TileType Player;
    private int Type = 0; // 0 standard tile does nothing (WHITE)
                          // 1 Wall tile (BLACK)
                          // 2 Bad tile  (RED)
                          // 3 Path tile (GREEN)
                          // 4 Walked tile  (TEAL)
                          // 5 End tile (YELLOW)
                          // 6 Border tile (BLACK)
    public int hCost = 0;
    public int gCost = 0;
    private int TypeLAST = 0;
    public int TypeNodeStorage = 0;
    private TileType[] neighbors = new TileType[4];
    private bool[] valid_neigh = new bool[4];
    public TileType parent;
    private int x = 0;
    private int y = 0;
    private bool revealed = false;
    private bool first_revealed = true;
    private float Rtime = 6.0f;
    private float ftime = 0.35f;
    private float tColor = 0.0f;
    public SpriteRenderer renderer;
    void Awake()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        parent = this;
        for (int x = 0; x < 4; x++)
            valid_neigh[x] = false;
    }
    void Update()
    {
        if(first_revealed)
        {
            renderer.color = TypeColor(Type);
            first_revealed = false;
        }
        if(tColor <= 1)
        {
            if(!revealed)
            {
                if (Type == 6)
                {
                    renderer.color = Color.black;
                    tColor = 1.01f;
                }
                else
                {
                    tColor += Time.deltaTime / Rtime;
                    renderer.color = Color.Lerp(TypeColor(Type), Color.white, tColor);
                    if (Type == 5 && renderer.sprite != TileArt[3])
                        renderer.sprite = TileArt[3];
                }
            }
            else
            {
                tColor += Time.deltaTime / ftime;
                if (Type == 2)
                {
                    renderer.sprite = TileArt[1];
                }
                else if (Type == 3)
                {
                    renderer.sprite = TileArt[2];
                }
                if (this == Player)
                {
                    renderer.color = Color.Lerp(TypeColor(TypeLAST), Color.cyan, tColor);
                }
                else
                {
                    renderer.color = Color.Lerp(TypeColor(TypeLAST), TypeColor(Type), tColor);
                }
            }
        }
    }
    private Color TypeColor(int ColorType)
    {
        if (ColorType == 0)
            return Color.white;
        else if (ColorType == 1)
            return Color.black;
        else if (ColorType == 2)
            return Color.red;
        else if (ColorType == 3)
            return Color.green;
        else if (ColorType == 4)
            return Color.cyan;
        else if (ColorType == 5)
            return Color.yellow;
        else if (ColorType == 6)
            return Color.gray;
        else
            return Color.black;
    }
    public void is_Walked()
    {
        revealed = true;
        //renderer.color = currentColor();
        tColor = 0.0f;
    }
    public void set_type(int _type)
    {
        TypeLAST = Type;
        Type = _type;
    }
    public void set_pos(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    public void set_neighbor(TileType neighbor, int direction) //0 North, 1 South, 2 East, 3 West
    {
        neighbors[direction] = neighbor;
        valid_neigh[direction] = true;
    }
    public TileType get_neighbor(int direction)
    {
        if (valid_neigh[direction])
            return neighbors[direction];
        else
            return this;
    }
    public int pos_x()
    {
        return x;
    }
    public int pos_y()
    {
        return y;
    }
    public int TileTypeNum()
    {
        return Type;
    }
    public int fCost()
    {
        return gCost + hCost;
    }
    public void storeNodeType()
    {
        TypeNodeStorage = Type;
    }
    public void revertStoreNode()
    {
        Type = TypeNodeStorage;
    }
}
