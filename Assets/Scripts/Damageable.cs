using TMPro;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] TMP_Text uiText;

    public int maxHealth = 3;
    int _currentHealt;

    void Start()
    {
        _currentHealt = maxHealth;
        SetUI();
    }

    public void Damage(int damageValue)
    {
        if (enabled)
        {
            if (_currentHealt > 0)
            {
                _currentHealt -= damageValue;
                _currentHealt = Mathf.Max(0, _currentHealt); 
            }
            if (_currentHealt == 0)
                GetComponent<Collider>().enabled = false;

            SetUI();
        }
    }

    void SetUI()
    {
        if (uiText != null)
            uiText.text = "Health: " + _currentHealt.ToString();
    }

    public bool IsAlive()
    {
        return _currentHealt > 0;
    }

}