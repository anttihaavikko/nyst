using System;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private Transform player;

    private string _text = "hello world!";

    private void Update()
    {
        if (Vector3.Distance(player.position, transform.position) > 2f) return;
        
        if (Input.GetKeyDown(KeyCode.A)) Append("a");
        if (Input.GetKeyDown(KeyCode.B)) Append("b");
        if (Input.GetKeyDown(KeyCode.C)) Append("c");
        if (Input.GetKeyDown(KeyCode.D)) Append("d");
        if (Input.GetKeyDown(KeyCode.E)) Append("e");
        if (Input.GetKeyDown(KeyCode.F)) Append("f");
        if (Input.GetKeyDown(KeyCode.G)) Append("g");
        if (Input.GetKeyDown(KeyCode.H)) Append("h");
        if (Input.GetKeyDown(KeyCode.I)) Append("i");
        if (Input.GetKeyDown(KeyCode.J)) Append("j");
        if (Input.GetKeyDown(KeyCode.K)) Append("k");
        if (Input.GetKeyDown(KeyCode.L)) Append("l");
        if (Input.GetKeyDown(KeyCode.M)) Append("m");
        if (Input.GetKeyDown(KeyCode.N)) Append("n");
        if (Input.GetKeyDown(KeyCode.O)) Append("o");
        if (Input.GetKeyDown(KeyCode.P)) Append("p");
        if (Input.GetKeyDown(KeyCode.Q)) Append("q");
        if (Input.GetKeyDown(KeyCode.R)) Append("r");
        if (Input.GetKeyDown(KeyCode.S)) Append("s");
        if (Input.GetKeyDown(KeyCode.T)) Append("t");
        if (Input.GetKeyDown(KeyCode.U)) Append("u");
        if (Input.GetKeyDown(KeyCode.V)) Append("v");
        if (Input.GetKeyDown(KeyCode.W)) Append("w");
        if (Input.GetKeyDown(KeyCode.X)) Append("x");
        if (Input.GetKeyDown(KeyCode.Y)) Append("y");
        if (Input.GetKeyDown(KeyCode.Z)) Append("z");
        if (Input.GetKeyDown(KeyCode.Period)) Append(".");
        if (Input.GetKeyDown(KeyCode.Space)) Append(" ");
    }

    private void Append(string letter)
    {
        if (_text.Length >= 50) return;
        _text += letter;
        board.Show(_text);
    }

    public void Clear()
    {
        _text = "";
        board.Show(_text);
    }
}