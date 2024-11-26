using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AnttiStarterKit.Extensions;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dictionary", menuName = "Dictionary", order = 0)]
public class WordDictionary : ScriptableObject
{
    private Dictionary<string, string> words;
    private List<string> letterPool;

    private string next;
    public TextAsset dictionaryFile;

    // Start is called before the first frame update
    public void Setup()
    {
        letterPool = new List<string>();
        Prep();
    }

    private void Prep()
    {
        words = dictionaryFile.text.Split('\n').Select(w => {
            var word = w.Trim().ToLower();
            return word.Split('\t')[0];
        }).Distinct().ToDictionary(x => x, x => x);

        // Debug.Log("Loaded dictionary of " + words.Count + " words.");
        //
        // var sample = RandomWord();
        // Debug.Log("Random word sample: '" + sample + "'");
    }

    public string IsWord(string word, bool checkReverseToo)
    {
        var reversed = string.Join("", word.Reverse()).ToLower();
        var ok = words.ContainsKey(word.ToLower());
        var rok = checkReverseToo && words.ContainsKey(reversed);
        if (ok) return word;
        if (rok) return reversed;
        return null;
    }

    public string GetRandomLetter()
    {
        return GetRandomLetter(Random.Range(0, 9999));
    }

    public string GetRandomLetter(int seed, bool remove = true)
    {
        if (!letterPool.Any())
            PopulateLetterPool(seed);

        var letter = letterPool[0];
        if (remove)
        {
            letterPool.RemoveAt(0);
            next = GetRandomLetter(seed, false);
        }

        return letter;
    }

    public string RandomWord()
    {
        var key = words.Keys.ToArray()[Random.Range(0, words.Count)];
        var word = words[key];
        return word;
    }

    void PopulateLetterPool(int seed)
    {
        Random.InitState(seed);
        var word = RandomWord();
        // Debug.Log("Seeding random letters with word '" + word + "'");
        letterPool.AddRange(Regex.Split(word, string.Empty).Where(IsOk).OrderBy(l => Random.value));
    }

    private bool IsOk(string letter)
    {
        return !string.IsNullOrEmpty(letter) && char.IsLetter(letter[0]);
    }

    public string GetNext()
    {
        return next;
    }
}
