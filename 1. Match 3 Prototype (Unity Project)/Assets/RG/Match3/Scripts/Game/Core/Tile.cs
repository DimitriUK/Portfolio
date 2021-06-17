using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Constants;

public class Tile : MonoBehaviour {
    #region Variables
    public string TileID { get; private set; }

    [Header("Neighbour Tiles")]
    public Dictionary<Direction, Tile> neighbourLookup = new Dictionary<Direction, Tile>();

    [Header("Color Piece in Tile")]
    public Piece TilePiece;

    private bool isFalling;

    private Vector3 originalPos;
    private Vector3 intendedDropPos;

    public bool HasUpNeighbour;
    public bool HasRightNeighbour;
    public bool HasDownNeighbour;
    public bool HasLeftNeighbour;
    #endregion

    #region Initilization Functions
    private void FixedUpdate() {
        /// Maybe good to avoid using Update and have a while loop or IEnumerator with a timer
        if (isFalling) {
            bool dropCalculation = TilePiece.transform.localPosition.y <= intendedDropPos.y + 0.02f;

            if (!dropCalculation) {
                TilePiece.transform.localPosition = Vector3.Lerp(TilePiece.transform.localPosition, intendedDropPos, Time.deltaTime * 4);
            }
            else {
                TilePiece.transform.localPosition = originalPos;
                TilePiece.ShowPiece((int)TilePiece.PieceColorSelected);
                isFalling = false;
                ShowAndActivateDownNeighbour();
            }
        }
    }
    #endregion

    #region Core Startup Functions
    public void Initialize(string tileID, int x, int y) {
        gameObject.name = x + ", " + y;

        gameObject.transform.position = new Vector3(x, y, 0);

        RandomisePiece();

        this.TileID = tileID;
    }

    public void AssignNeighbour(Direction direction, Tile tile) {
        if (!neighbourLookup.ContainsKey(direction)) {
            neighbourLookup.Add(direction, tile);

            if (direction == Direction.Up)
                HasUpNeighbour = true;
            if (direction == Direction.Right)
                HasRightNeighbour = true;
            if (direction == Direction.Down)
                HasDownNeighbour = true;
            if (direction == Direction.Left)
                HasLeftNeighbour = true;
        }
        else {
            neighbourLookup[direction] = tile;
        }
    }

    public void RandomisePiece() {
        TilePiece.PieceColorSelected = (Piece.PieceColor)Random.Range(1, Constants.PIECE_AMOUNTS);
        int randomPieceId = (int)TilePiece.PieceColorSelected;
        TilePiece.ShowPiece(randomPieceId);
    }
    #endregion

    #region Core Mid-game Functions
    public void TileClicked() {
        TilePiece.RemoveColorAndEmptyTile();
    }

    public void AssignCurrentTileColorToTopTileColor(Tile topTile) {
        TilePiece.PieceColorSelected = topTile.TilePiece.PieceColorSelected;
    }

    public void ShowPiece() {
        TilePiece.ShowPiece((int)TilePiece.PieceColorSelected);
    }

    public void DropPiece() {
        originalPos = TilePiece.transform.localPosition;
        intendedDropPos = new Vector3(TilePiece.transform.localPosition.x, TilePiece.transform.localPosition.y - 1, TilePiece.transform.localPosition.z);
        isFalling = true;
    }

    public IEnumerator DropPieceTimer() {
        yield return new WaitForSeconds(1);
        isFalling = false;
        TilePiece.ShowPiece((int)TilePiece.PieceColorSelected);
    }

    public void RemoveColorAndEmptyTile(bool instantShow) {
        TilePiece.PieceColorSelected = Piece.PieceColor.None;

        if (instantShow)
            TilePiece.ShowPiece((int)TilePiece.PieceColorSelected);
    }

    public void ShowAndActivateDownNeighbour() {
        Tile downTile = null;
        neighbourLookup.TryGetValue(Direction.Down, out downTile);

        if (HasDownNeighbour) {
            downTile.ShowPiece();
        }
    }
    #endregion
}

public enum Direction {
    Up,
    Right,
    Down,
    Left
}