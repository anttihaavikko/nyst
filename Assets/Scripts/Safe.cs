using UnityEngine;

public class Safe : ButtonPanel
{
    [SerializeField] private string password;

    private string _input = "";

    public bool IsCorrect => _input == password;
    
    public override void Check(PushButton button)
    {
        _input += button.Letter;
    }

    public void Reset()
    {
        _input = "";
    }
}