using System;
using UnityEngine;
namespace Utils.Constants
{
    public static class Constants
    {
        public static readonly int PIECE_AMOUNTS = Enum.GetNames(typeof(Piece.PieceColor)).Length;
    }
}
