using UnityEngine;
using UnityEngine.UI;

public class Magazine : MonoBehaviour
{
    public static Magazine Instance;

    public int maxBullets = 10;
    public Image bulletIcon;

    int bullets;
    Image[] icons;

    void Awake()
    {
        Instance = this;
        bullets = maxBullets;

        icons = new Image[maxBullets];
        icons[0] = bulletIcon;
        for (int i = 1; i < maxBullets; i++)
            icons[i] = Instantiate(bulletIcon, bulletIcon.transform.parent);

        UpdateUI();
    }

    public bool TryShoot()
    {
        if (bullets <= 0) return false;
        bullets--;
        UpdateUI();
        return true;
    }

    public void Restore()
    {
        if (bullets >= maxBullets) return;
        bullets++;
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < icons.Length; i++)
            icons[i].enabled = i < bullets;
    }
}
