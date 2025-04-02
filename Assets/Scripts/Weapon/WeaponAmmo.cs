using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int ClipSize { get; private set; }
    public int ExtraAmmo { get; private set; }
    public int CurrentAmmo { get; private set; }
    public bool IsHasBullets => CurrentAmmo > 0;

    private void Start()
    {
        CurrentAmmo = ClipSize;
        ExtraAmmo = GameUtils.StartExtraAmmo;
    }

    public void SetClipSize(int clipSize)
    {
        if (clipSize < 0)
            return;

        ClipSize = clipSize;
    }

    public void AddExtraAmmo(int extraAmmo)
    {
        if (ExtraAmmo < 0)
            return;

        ExtraAmmo += extraAmmo;
    }

    public void SpendAmmo()
    {
        if (CurrentAmmo > 0)
            CurrentAmmo--;
    }

    public void Reload()
    {
        if (ExtraAmmo >= ClipSize)
        {
            int ammoToReload = ClipSize - CurrentAmmo;
            ExtraAmmo -= ammoToReload;
            CurrentAmmo += ammoToReload;
        }
        else if (ExtraAmmo > 0)
        {
            if(ExtraAmmo + CurrentAmmo > ClipSize)
            {
                int leftOverAmmo = ExtraAmmo + CurrentAmmo - ClipSize;
                ExtraAmmo = leftOverAmmo;
                CurrentAmmo = ClipSize;
            }
            else
            {
                CurrentAmmo += ExtraAmmo;
                ExtraAmmo = 0;
            }
        }
    }
}