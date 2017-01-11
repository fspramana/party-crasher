using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    public GameObject Character;

    public GameObject Container;
    public Image YoureDeadImage;
    public Image Portrait;

    public Image HealthBar;

    public Image WeaponIcon;
    public Text AmmunitionAmount;

    public Sprite[] WeaponIcons;

    private Health _health;
    private WeaponManager _weaponManager;

    private bool saveToUpdate = false;

	void Start ()
	{
        YoureDeadImage.gameObject.SetActive(false);
	}
	
	void Update ()
	{
        if (saveToUpdate) ActualUpdate();
	}

    private void ActualUpdate()
    {
        if (!_health.IsLiving()) YoureDeadImage.gameObject.SetActive(true);
        if(_weaponManager.activedWeapon.id == 0)
        {
            AmmunitionAmount.text = "INF";
        }
        else
        {
            AmmunitionAmount.text = _weaponManager.activedWeapon.BulletLeft.ToString();
        }

        WeaponIcon.sprite = WeaponIcons[_weaponManager.activedWeapon.id];

        HealthBar.fillAmount = _health.CurrentHitPoint / (float) _health.maxHitPoint;
    }

    public void SetIsVisible(bool visible)
    {
        Container.SetActive(visible);
        Portrait.gameObject.SetActive(visible);
    }

    public void SetCharacter(GameObject character)
    {
        this.Character = character;
        _health = this.Character.GetComponent<Health>();
        _weaponManager = this.Character.GetComponent<WeaponManager>();
        if(_health && _weaponManager) saveToUpdate = true;
    }
}
