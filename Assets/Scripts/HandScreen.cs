using UnityEngine;

public class HandScreen : MonoBehaviour
{
    [SerializeField] private Board board;

    public void Show(string text)
    {
        board.Show(text);
    }
}