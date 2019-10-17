using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Cell
{
    int _x, _y;
    bool[] _walls = { true, true, true, true }; //top, right, bottom, left - like the clock works

    public Cell(int x, int y) //left top corner of square
    {
        this._x = x;
        this._y = y;
    }

    public int X
    {
        get
        {
            return this._x;
        }
    }
    public int Y
    {
        get
        {
            return this._y;
        }
    }

    public bool[] Walls
    {
        get
        {
            return this._walls;
        }
        set
        {
            this._walls = value;
        }
    }

    public void Randomize()
    {
        this._walls[0] = true;
        this._walls[1] = true;
        this._walls[2] = true;
        this._walls[3] = true;
    }
}