using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    const int ROWS = 8, COLS = 8;
    public GameObject white, black;
    public Color selectionColor = Color.yellow;
    public Vector2Int selection;
    private GameObject[,] tiles; 
    public GameObject knight, pawn, bishop, rook, queen, king;
    [TextArea(4, 8)]
    public string layout =
        "RNBQKBNR\n" +
        "PPPPPPPP\n" +
        "........\n" +
        "........\n" +
        "........\n" +
        "........\n" +
        "pppppppp\n" +
        "rnbqkbnr\n";
    private void OnValidate()
    {
        UnselectAllSquares();
        SelectSquare();
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
        PopulateBoard();
        SelectSquare();


    }
    private void CreateBoard()
    {
        tiles = new GameObject[ROWS, COLS];
        for (int row = 0; row < ROWS; row++)
        {
            for (int col = 0; col < COLS; col++)
            {
                bool thisTileIsWhite = (row + col) % 2 == 0;
                GameObject thisTile = thisTileIsWhite ? white : black;
                GameObject tile = Instantiate(thisTile, TranslateCoordinate(row, col), Quaternion.identity);

                tile.transform.SetParent(transform);
                tile.SetActive(true);
                tiles[row, col] = tile;
            }

        }
    }

    private void PopulateBoard()
    {
        int row = 0;
        int col = 0;
        for (int i = 0; i < layout.Length; i++)
        {
            char ch = layout[i];
            MakePiece(ch, row, col);
            col++;
            if (ch == '\n') {
                col = 0;
                row++;
            }
        }
    }

    public void SelectSquare()
    {
        if (tiles == null || selection.y < 0 || selection.y >= ROWS || selection.x < 0 || selection.x >= COLS)
        {
            return;
        }
        tiles[selection.y, selection.x].GetComponent<Renderer>().material.color = selectionColor;

    }

    public void UnselectSquare(int row, int col)
    {
        if (tiles == null)
        {
            return;
        }
        bool thisTileIsWhite = (row + col) % 2 == 0;
        GameObject thisTile = thisTileIsWhite ? white : black;
        tiles[row, col].GetComponent<Renderer>().material.color = thisTile.GetComponent<Renderer>().material.color;
    }

    public void UnselectAllSquares()
    {
        for (int row = 0; row < ROWS; row++)
        {
            for (int col = 0; col < COLS; col++)
            {
                UnselectSquare(row, col);
            }

        }
    }

    GameObject MakePiece(char ch, int row, int col)
    {
        float h = 0.5f;
        bool isBlack = true;
        GameObject piece = null;
        switch(ch)
        {
            case 'n':
            case 'N':
                piece = knight;
                isBlack = (ch == 'N');
                break;
            case 'p':
            case 'P':
                piece = pawn;
                isBlack = (ch == 'P');
                break;
            case 'b':
            case 'B':
                piece = bishop;
                isBlack = (ch == 'B');
                break;
            case 'q':
            case 'Q':
                piece = queen;
                isBlack = (ch == 'Q');
                break;
            case 'r':
            case 'R':
                piece = rook;
                isBlack = (ch == 'R');
                break;
            case 'k':
            case 'K':
                piece = king;
                isBlack = (ch == 'K');
                break;
            default: return null;
        }
        piece = Instantiate(piece, TranslateCoordinate(row,col,h), Quaternion.identity);
        if (isBlack)
        {
            ColorThese(piece, black.GetComponent<Renderer>().material.color);
        }
        else
        {
            piece.transform.Rotate(0, -180, 0);
            ColorThese(piece, white.GetComponent<Renderer>().material.color);
        }
        return piece;
    }

    public Vector3 TranslateCoordinate(int row, int col, float height = 0) 
    {
        return new Vector3(-col, height, row);
    }

    private void ColorThese(GameObject root, Color color)
    {
        Renderer[] objects = root.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].material.color = color;
        }
    }
        
    // Update is called once per frame
    void Update()
    {
        
    }
}
