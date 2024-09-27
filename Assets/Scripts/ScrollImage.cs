using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollImage : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI nameText;
    public VehiclePart part;

    public void SetImage(Texture2D texture)
    {
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
