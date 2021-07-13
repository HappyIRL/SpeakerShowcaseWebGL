using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Image image;

    public TMP_Text Header => header;
    public TMP_Text Description => description;
    public Image Image => image;

}
