using System;
using UnityEngine;

[RequireComponent(typeof(Activator))]
public class Computer : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private Transform player;
    [SerializeField] private int maxLength = 12;
    [SerializeField] private string password;

    private Activator _activator;
    private string _text = "hello world!";

    public string Password => password;
    public Action PasswordInput { get; set; }

    private void Start()
    {
        _activator = GetComponent<Activator>();
        _text = board.GetText();
    }

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
        if (_text.Length >= maxLength) return;
        _text += letter;
        board.Show(_text);
        
        PasswordInput?.Invoke();

        if (Clean(password) == Clean(_text))
        {
            _activator.Activate();
        }
    }

    public bool HasCorrect(int start, int length = 2)
    {
        return _text.Length >= start + length &&
               Clean(_text).Substring(start, length) == Clean(password).Substring(start, length);
    }

    public string Clean(string input)
    {
        return input.Replace("!", ".");
    }

    public void Clear()
    {
        _text = "";
        board.Show(_text);
        PasswordInput?.Invoke();
    }
}