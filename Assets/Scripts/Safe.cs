using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Safe : ButtonPanel
{
    [SerializeField] private List<string> passwords;

    private string _input = "";

    public bool IsCorrect => passwords.Contains(_input);
    
    public override void Check(PushButton button)
    {
        _input += button.Letter;
    }

    public void Reset()
    {
        _input = "";
    }

    public List<int> GetNumbers()
    {
        // Debug.Log(string.Join(",", passwords[0]));
        return passwords[0].ToCharArray().Select(s => int.Parse(s.ToString())).ToList();
    }
}