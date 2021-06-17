using UnityEngine;
using Utils.Constants;

public class Piece : MonoBehaviour {
    #region Variables
    public enum PieceColor /// Do NOT change the order of these, if you want to add new colors, please add it at the bottom.
    {
        None = 0,
        White,
        Purple,
        Gold,
        Cyan
    }

    [Header("Assign Piece Color")]
    public PieceColor PieceColorSelected;

    public GameObject[] ColorPieces = new GameObject[Constants.PIECE_AMOUNTS];

    #endregion

    #region Core Functions
    public void ShowPiece(int pieceId) {
        for (int i = 0; i < Constants.PIECE_AMOUNTS; i++) {
            ColorPieces[i].gameObject.SetActive(false);

            if (i == pieceId)
                ColorPieces[i].gameObject.SetActive(true);
        }
    }
    public void RemoveColorAndEmptyTile() {
        PieceColorSelected = PieceColor.None;
        ShowPiece(0);
    }
    #endregion
}