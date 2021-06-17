using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    #region Variables
    [SerializeField]
    public GridManager currentGrid;

    [SerializeField]
    private Tile tilePrefab;

    private bool canClick = true;
    #endregion

    #region Initilization Functions
    private void Awake() {
        instance = this;
    }

    private void Start() {
        NewGame();
    }

    private void Update() {
        if (!canClick)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag == "Tile") {
                Tile tileClicked = hit.collider.GetComponent<Tile>();

                if (tileClicked.TilePiece.PieceColorSelected == Piece.PieceColor.None) {
                    // Potentially create an effect of clicking on a tile that isn't clickable with a sound effect for future.
                    return;
                }

                if (Input.GetMouseButtonDown(0)) {
                    currentGrid.TapPiece(tileClicked);
                    StartCoroutine(DelayInput());
                }
            }
        }
    }
    #endregion

    #region Core Functions
    private void NewGame() {
        if (currentGrid == null) {
            currentGrid = gameObject.AddComponent<GridManager>();
        }
        currentGrid.SpawnTilesAndAssignNeighbours(tilePrefab);
    }
    #endregion

    #region Input Functions
    public void EnableInput() {
        canClick = true;
        Debug.Log("<color=orange> [Input Systems]:</color> User can click");
    }

    public IEnumerator DelayInput() {
        canClick = false;
        yield return new WaitForSeconds(1);
        currentGrid.ClearMatchesAndTilesToDropAndCheckForMatchAndAdd();
    }
    #endregion
}