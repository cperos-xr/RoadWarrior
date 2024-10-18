using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponFire : MonoBehaviour
{
    public PlayerInputContols input;
    public Transform firePoint;
    
    private RaycastHit raycastHit;


    [SerializeField] private float range;
    [SerializeField] private float rateOfFire;
    [SerializeField] private bool isAutomatic;
    [SerializeField] private int magazineSize;
    [SerializeField] private float reloadTime;
    [SerializeField] private int currentAmmo;

    [SerializeField] private bool isFiring = false;
    [SerializeField] private bool ready = true;
    [SerializeField] private bool reloading = false;

    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private float bulletHoleLifespan;

    void Awake()
    {
        input = new PlayerInputContols();
        input.Car.Fire.started += value => StartShot();
        input.Car.Fire.canceled += value => EndShot();

        input.Car.Reload.performed += value => Reload();

        currentAmmo = magazineSize;
    }

    private void Update()
    {
        if (isFiring && ready && !reloading && currentAmmo > 0)
        {
            PerformShot();
        }
    }

    private void StartShot()
    {
        Debug.Log("Start Shot");
        isFiring = true;
    }

    private void EndShot()
    {
        Debug.Log("End Shot");
        isFiring = false;
    }

    private void PerformShot()
    {
        Debug.Log("Shot Fired");
        ready = false;

        Vector3 direction = firePoint.forward;

        if (Physics.Raycast(firePoint.position, direction, out raycastHit, range))
        {
            Debug.Log(raycastHit.transform.name);

            if (raycastHit.transform.CompareTag("Enemy"))
            {
                // Do damage to enemy
            }
            else
            {
                GameObject bulletHole = Instantiate(bulletHolePrefab, raycastHit.point + raycastHit.normal * 0.001f, Quaternion.LookRotation(raycastHit.normal));
                Destroy(bulletHole, bulletHoleLifespan);
            }

        }

        currentAmmo--;

        if (currentAmmo >= 0)
        {
            Invoke("ResetShot", rateOfFire);
            if (!isAutomatic)
            {
                EndShot();
            }
        }
    }

    private void ResetShot()
    {
        ready = true;  
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadComplete", reloadTime);
    }

    private void ReloadComplete()
    {
        currentAmmo = magazineSize;
        reloading = false;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }



}
