using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    [Header("Character 01")]
    public GameObject c1w;
    public GameObject c1br;
    public GameObject c1bl;

    [Header("Character 02")]
    public GameObject c2w;
    public GameObject c2br;
    public GameObject c2bl;

    [Header("Character 03")]
    public GameObject c3w;
    public GameObject c3br;
    public GameObject c3bl;

    [Header("Character 04")]
    public GameObject c4w;
    public GameObject c4br;
    public GameObject c4bl;

    [Header("Character 05")]
    public GameObject c5w;
    public GameObject c5br;
    public GameObject c5bl;

    public int cNum;
    public int fNum;
    public int modelNum;

    //This script was created for the purpose of selecting a character when on the character creation part of the main menu.

    void Update()
    {
        if (cNum < 0)
        {
            cNum = 4;
        }
        if (cNum > 4)
        {
            cNum = 0;
        }

        if (fNum < 0)
        {
            fNum = 2;
        }
        if (fNum > 2)
        {
            fNum = 0;
        }

        if (cNum == 0 && fNum == 0) // Character 1 White Hair
        {
            c1w.SetActive(true); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 0;
        }
        if (cNum == 1 && fNum == 0) // Character 2 White Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(true); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 1;
        }
        if (cNum == 2 && fNum == 0) //Character 3 White Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(true); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 2;
        }
        if (cNum == 3 && fNum == 0) // Character 4 White Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(true); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 3;
        }

        if (cNum == 4 && fNum == 0) // Character Chief White Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(true); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 4;
        }

        if (cNum == 0 && fNum == 1) // Character 1 Brown Hair
        {
            c1w.SetActive(false); c1br.SetActive(true); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 5;
        }
        if (cNum == 1 && fNum == 1) // Character 2 Brown Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(true); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 6;
        }
        if (cNum == 2 && fNum == 1) // Character 3 Brown Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(true); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 7;
        }
        if (cNum == 3 && fNum == 1) // Character 4 Brown Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(true); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 8;
        }

        if (cNum == 4 && fNum == 1) // Character Chief Brown Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(true); c5bl.SetActive(false);

            modelNum = 9;
        }

        if (cNum == 0 && fNum == 2) // Character 1 Black Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(true);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 10;
        }
        if (cNum == 1 && fNum == 2) // Character 1 Black Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(true);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 11;
        }
        if (cNum == 2 && fNum == 2) // Character 1 Black Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(true);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 12;
        }
        if (cNum == 3 && fNum == 2) // Character 1 Black Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(true);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(false);

            modelNum = 13;
        }
        if (cNum == 4 && fNum == 2) // Character 1 Black Hair
        {
            c1w.SetActive(false); c1br.SetActive(false); c1bl.SetActive(false);

            c2w.SetActive(false); c2br.SetActive(false); c2bl.SetActive(false);

            c3w.SetActive(false); c3br.SetActive(false); c3bl.SetActive(false);

            c4w.SetActive(false); c4br.SetActive(false); c4bl.SetActive(false);

            c5w.SetActive(false); c5br.SetActive(false); c5bl.SetActive(true);

            modelNum = 14;
        }
    }

    public void IntCDown()
    {
        cNum -= 1;
    }
    public void IntCUp()
    {
        cNum += 1;
    }

    public void IntFDown()
    {
        fNum -= 1;
    }
    public void IntFUp()
    {
        fNum += 1;
    }
}
