using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{

    public Sprite PlayerJoinedSprite;
    public Sprite PlayerStandbySprite;

    private Image _image;
    public RawImage _renderTexture;

	void Start ()
	{
        _image = GetComponent<Image>();
	}
	
	void Update ()
	{
	
	}

    public void PlayerJoined()
    {
        if (_image) _image.sprite = PlayerJoinedSprite;
        if (_renderTexture) _renderTexture.gameObject.SetActive(true);
    }
    
    public void PlayerStandby()
    {
        if (_image) _image.sprite = PlayerStandbySprite;
        if (_renderTexture) _renderTexture.gameObject.SetActive(false);
    }
}
