using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private Image first, second, special;
    [SerializeField] private RectTransform rectTransform;

    public void Setup(LetterDefinition a, LetterDefinition b, LetterDefinition c, Color color)
    {
        var width = Mathf.Max(a.offset + a.width, b.offset + b.width);
        rectTransform.sizeDelta = new Vector2(width, 100);
        first.sprite = a.sprite;
        second.sprite = b.sprite;
        special.sprite = c.sprite;
        first.color = second.color = special.color = color;
    }
}