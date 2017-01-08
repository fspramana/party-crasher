using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{

    public Sprite PlayerJoinedSprite;
    public Sprite PlayerStandbySprite;

    private Image _image;

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
    }
    
    public void PlayerStandby()
    {
        if (_image) _image.sprite = PlayerStandbySprite;
    }
}
