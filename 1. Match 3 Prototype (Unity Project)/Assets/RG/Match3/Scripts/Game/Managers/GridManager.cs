using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    #region Variables
    [SerializeField]
    private int gridSizeX;
    [SerializeField]
    private int gridSizeY;

    public int GridSizeX => gridSizeX;
    public int GridSizeY => gridSizeY;

    private List<Tile> activeTiles = new List<Tile>();

    public List<Tile> MatchedTiles = new List<Tile>();
    public List<Tile> TilesToDrop = new List<Tile>();

    private Tile[,] activeTiles2DArray; // This array is used for the X and Y positions

    public int TimesRefreshed;
    #endregion

    #region Starting Functions
    public void SpawnTilesAndAssignNeighbours(Tile tilePrefab) {
        activeTiles2DArray = SpawnRandomPieces(gridSizeX, gridSizeY, tilePrefab);
        AssignTileNeighbours(activeTiles2DArray);
        CheckStartingMatches();
    }

    private Tile[,] SpawnRandomPieces(int gridSizeX, int gridSizeY, Tile tilePrefab) {
        var result = new Tile[gridSizeX, gridSizeY];

        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Tile newTile = Instantiate(tilePrefab, transform);
                activeTiles.Add(newTile);
                newTile.Initialize($"{x}_{y}", x, y);
                result[x, y] = newTile;
            }
        }
        return result;
    }

    private void AssignTileNeighbours(Tile[,] tiles) {
        Debug.Log("<color=yellow> [Matching Init Systems]:</color> Start Assigning Tiles");
        for (int width = 0; width < tiles.GetLength(0); width++) {
            for (int height = 0; height < tiles.GetLength(1); height++) {
                bool hasLeftNeighbour = (width != 0);
                bool hasDownNeighbour = (height != 0);
                bool hasRightNeighbour = (width != GridSizeX - 1);
                bool hasUpNeighbour = (height != GridSizeY - 1);

                if (hasLeftNeighbour) {
                    Tile leftNeighbour = tiles[width - 1, height];
                    tiles[width, height].AssignNeighbour(Direction.Left, leftNeighbour);
                }
                if (hasRightNeighbour) {
                    Tile rightNeighbour = tiles[width + 1, height];
                    tiles[width, height].AssignNeighbour(Direction.Right, rightNeighbour);
                }
                if (hasUpNeighbour) {
                    Tile upNeighbour = tiles[width, height + 1];
                    tiles[width, height].AssignNeighbour(Direction.Up, upNeighbour);
                }
                if (hasDownNeighbour) {
                    Tile downNeighbour = tiles[width, height - 1];
                    tiles[width, height].AssignNeighbour(Direction.Down, downNeighbour);
                }
            }
        }
    }
    #endregion

    #region Check-based Starting Functions

    private void CheckStartingMatches() {
        MatchedTiles.Clear();

        TimesRefreshed++;

        for (int t = 0; t < activeTiles.Count; t++) {
            Tile currentTile = activeTiles[t];
            Tile rightTile = null;

            if (currentTile.neighbourLookup.TryGetValue(Direction.Right, out rightTile)) {
                {
                    if (currentTile.TilePiece.PieceColorSelected == rightTile.TilePiece.PieceColorSelected) {
                        Tile thirdTile = null;

                        rightTile.neighbourLookup.TryGetValue(Direction.Right, out thirdTile);

                        if (thirdTile == null) {
                            return;
                        }

                        if (rightTile.TilePiece.PieceColorSelected == thirdTile.TilePiece.PieceColorSelected) {
                            if (!MatchedTiles.Contains(currentTile)) {
                                MatchedTiles.Add(currentTile);
                            }

                            if (!MatchedTiles.Contains(rightTile)) {
                                MatchedTiles.Add(rightTile);
                            }

                            if (!MatchedTiles.Contains(thirdTile)) {
                                MatchedTiles.Add(thirdTile);
                            }
                        }
                    }
                }

                if (MatchedTiles.Count != 0)
                    RandomiseStartingMatches();
            }
        }
    }

    private void RandomiseStartingMatches() {
        for (int i = 0; i < MatchedTiles.Count; i++) {
            MatchedTiles[i].RandomisePiece();
        }

        if (MatchedTiles.Count != 0)
            CheckStartingMatches();
    }

    #endregion

    #region Mid-game Functions

    public void TapPiece(Tile tileClicked) {

        tileClicked.TileClicked();
        DropCurrentTileUpperNeighbours(tileClicked);
    }

    public void DropCurrentTileUpperNeighbours(Tile tileClicked) {
        int tileXPos = (int)tileClicked.TilePiece.transform.position.x;
        int tileYPos = (int)tileClicked.TilePiece.transform.position.y;

        for (int i = tileYPos; i < gridSizeY; i++) {
            if (i < GridSizeY - 1) {
                activeTiles2DArray[tileXPos, i].TilePiece.PieceColorSelected = (activeTiles2DArray[tileXPos, i + 1].TilePiece.PieceColorSelected);
            }
            activeTiles2DArray[tileXPos, i].DropPiece();
        }

        activeTiles2DArray[tileXPos, gridSizeY - 1].TilePiece.PieceColorSelected = Piece.PieceColor.None;
    }

    public void ClearMatchesAndTilesToDropAndCheckForMatchAndAdd() {
        MatchedTiles.Clear();
        TilesToDrop.Clear();

        Tile rightTile = null;

        for (int t = 0; t < activeTiles2DArray.Length; t++) {
            Tile currentTile = activeTiles[t];

            int currentTileXPos = (int)currentTile.transform.position.x;
            int currentTileYPos = (int)currentTile.transform.position.y;

            if (currentTile.TilePiece.PieceColorSelected == Piece.PieceColor.None) {
                continue;
            }

            if (currentTile.neighbourLookup.TryGetValue(Direction.Right, out rightTile)) {
                for (int x = currentTileXPos; x < GridSizeX; x++) {
                    if ((x + 1) < GridSizeX && activeTiles2DArray[currentTileXPos, currentTileYPos].TilePiece.PieceColorSelected == (activeTiles2DArray[currentTileXPos + 1, currentTileYPos].TilePiece.PieceColorSelected)) {
                        if ((x + 2) < GridSizeX && activeTiles2DArray[currentTileXPos, currentTileYPos].TilePiece.PieceColorSelected == (activeTiles2DArray[currentTileXPos + 2, currentTileYPos].TilePiece.PieceColorSelected)) {
                            AddMatchToMatchedList(activeTiles2DArray[currentTileXPos, currentTileYPos]);
                            AddMatchToMatchedList(activeTiles2DArray[currentTileXPos + 1, currentTileYPos]);
                            AddMatchToMatchedList(activeTiles2DArray[currentTileXPos + 2, currentTileYPos]);

                            if ((x + 2) == (GridSizeX - 1)) {
                                SubmitMatchAndWaitForNextMatch();
                                return;
                            }

                            for (int i = 3; i < GridSizeX - x; i++) {
                                if (activeTiles2DArray[currentTileXPos, currentTileYPos].TilePiece.PieceColorSelected == (activeTiles2DArray[currentTileXPos + i, currentTileYPos].TilePiece.PieceColorSelected)) {
                                    AddMatchToMatchedList(activeTiles2DArray[currentTileXPos + i, currentTileYPos]);

                                    if ((x + i) == (GridSizeX - 1)) {
                                        SubmitMatchAndWaitForNextMatch();
                                        return;
                                    }
                                }
                                else {
                                    SubmitMatchAndWaitForNextMatch();
                                    return;
                                    //break;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (MatchedTiles.Count == 0) {
            Debug.Log("<color=yellow> [Matching Systems]:</color> No Match Found");
            GameManager.instance.EnableInput();
        }
    }

    public void AddMatchToMatchedList(Tile matchedTile) {
        if (!MatchedTiles.Contains(matchedTile) && matchedTile.TilePiece.PieceColorSelected != Piece.PieceColor.None)
            MatchedTiles.Add(matchedTile);
    }

    public void SubmitMatchAndWaitForNextMatch() {

        for (int t = 0; t < MatchedTiles.Count; t++) {
            Tile tile = MatchedTiles[t];

            MakeTileEmpty(tile);

            int tileXPos = (int)tile.TilePiece.transform.position.x;
            int tileYPos = (int)tile.TilePiece.transform.position.y;

            ChangeCurrentTileColorToAbove(tile, tileXPos, tileYPos);
            AddToTilesToDropList(tileXPos, tileYPos);
        }
        RemoveTopMostNeighbour();


        StartCoroutine(WaitForMatch());
    }

    public void MakeTileEmpty(Tile tile) {
        tile.RemoveColorAndEmptyTile(true);
    }

    public void ChangeCurrentTileColorToAbove(Tile tile, int xPos, int yPos) {
        tile.AssignCurrentTileColorToTopTileColor(activeTiles2DArray[xPos, yPos + 1]);
    }

    private void AddToTilesToDropList(int xPos, int yPos) {
        for (int i = yPos; i < GridSizeY; i++) {
            if (i < GridSizeY - 1) {
                if (activeTiles2DArray[xPos, i + 1].TilePiece.PieceColorSelected != Piece.PieceColor.None) {
                    if (!TilesToDrop.Contains(activeTiles2DArray[xPos, i + 1])) // List probably not required and can just be done via the for loop for optimisation purposes.
                    {
                        if (activeTiles2DArray[xPos, i + 1].TilePiece.PieceColorSelected != Piece.PieceColor.None) {
                            TilesToDrop.Add(activeTiles2DArray[xPos, i + 1]);
                        }
                    }
                }
                activeTiles2DArray[xPos, i + 1].DropPiece();
            }
        }
    }

    private void RemoveTopMostNeighbour() {
        for (int t = 0; t < TilesToDrop.Count; t++) {
            Tile upTile = TilesToDrop[t];
            int xPos = (int)upTile.TilePiece.transform.position.x;
            int yPos = (int)upTile.TilePiece.transform.position.y;

            if (upTile.HasUpNeighbour) {
                upTile.AssignCurrentTileColorToTopTileColor(activeTiles2DArray[xPos, yPos + 1]);
            }
            else {
                upTile.RemoveColorAndEmptyTile(false);
            }
        }
    }

    public IEnumerator WaitForMatch() {
        Debug.Log("<color=yellow> [Matching Systems]:</color> Wait for Match");
        yield return new WaitForSeconds(1);
        ClearMatchesAndTilesToDropAndCheckForMatchAndAdd();
    }
    #endregion
}
