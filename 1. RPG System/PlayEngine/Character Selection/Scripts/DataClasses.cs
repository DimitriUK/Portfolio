using System;
using UnityEngine;

    [Serializable]
    public class PlayerData
    {
        public string FirstName;
        public string LastName;
        public int RaceID;
        public int ModelID;

        public PlayerData(string firstName, string lastName, int raceID, int modelID)
        {
            FirstName = firstName;
            LastName = lastName;
            RaceID = raceID;
            ModelID = modelID;
        }
    }

