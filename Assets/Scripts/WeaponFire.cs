using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponFire : MonoBehaviour
{
    public PlayerInputContols input;
    public Weapon weapon;

    public Transform firePoint;
    public Transform weaponTurret;

    private RaycastHit raycastHit;
    [SerializeField] private int currentAmmo;

    [SerializeField] private bool isFiring = false;
    [SerializeField] private bool ready = true;
    [SerializeField] private bool reloading = false;

    private int bulletsFired;

    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletshell;

    public float rotationValue;
    public float rotationSpeed;

    public Vector2 thumbstickInput;


    void Awake()
    {
        input = new PlayerInputContols();
        input.Car.Fire.started += value => StartShot();
        input.Car.Fire.canceled += value => EndShot();

        input.Car.Reload.performed += value => Reload();
    }

    private void OnEnable()
    {
        input.Enable();
        CreatedCarManager.OnPrimaryWeaponSelected += OnPrimaryWeaponSelected;
        input.Car.Aim.performed += RotateTurret;
        input.Car.Aim.canceled += StopRotation;

        input.XRController.ThumbstickLeft.performed += OnThumbstickMove;
        input.XRController.ThumbstickLeft.canceled += OnThumbstickRelease;
    }

    private void OnDisable()
    {
        input.Disable();
        CreatedCarManager.OnPrimaryWeaponSelected -= OnPrimaryWeaponSelected;
        input.Car.Aim.performed += RotateTurret;
        input.Car.Aim.canceled += StopRotation;

        input.XRController.ThumbstickLeft.performed -= OnThumbstickMove;
        input.XRController.ThumbstickLeft.canceled -= OnThumbstickRelease;
    }

    private void OnThumbstickRelease(InputAction.CallbackContext context)
    {
        thumbstickInput = Vector2.zero;
        RotateTurret(thumbstickInput.x);
    }

    private void OnThumbstickMove(InputAction.CallbackContext context)
    {
        thumbstickInput = context.ReadValue<Vector2>();
        RotateTurret(thumbstickInput.x);
    }

    private void RotateTurret(float value)
    {
        rotationValue = value;
    }

    private void RotateTurret(InputAction.CallbackContext value)
    {
        rotationValue = value.ReadValue<float>();
    }

    private void StopRotation(InputAction.CallbackContext value)
    {
        rotationValue = 0;
    }

    private void RotateWeaponTurret()
    {
        // Calculate the new rotation amount based on the input and rotation speed
        float rotationAmount = rotationValue * rotationSpeed * Time.deltaTime;

        // Apply the rotation to the turret
        weaponTurret.Rotate(0, rotationAmount, 0);
    }

    private void OnPrimaryWeaponSelected(Weapon primaryWeapon)
    {
        weapon = primaryWeapon;
        currentAmmo = weapon.magazineSize;
    }

    private void Update()
    {
        if (isFiring && ready && !reloading && currentAmmo > 0)
        {
            bulletsFired = weapon.bulletsPerShot;
            PerformShot();
        }

        RotateWeaponTurret();
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

        float x = Random.Range(-weapon.horizontalSpread, weapon.horizontalSpread);
        float y = Random.Range(-weapon.verticalSpread, weapon.verticalSpread);

        Vector3 direction = firePoint.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(firePoint.position, direction, out raycastHit, weapon.range))
        {
            Debug.Log(raycastHit.transform.name);

            if (raycastHit.transform.CompareTag("Enemy"))
            {
                // Do damage to enemy
            }
            else
            {
                GameObject bulletHole = Instantiate(weapon.bulletHolePrefab, raycastHit.point + raycastHit.normal * 0.001f, Quaternion.LookRotation(raycastHit.normal));
                Destroy(bulletHole, weapon.bulletHoleLifespan);
            }

        }

        // Play MuzzleFlash here
        muzzleFlash.Play();
        bulletshell.Play();

        currentAmmo--;
        bulletsFired--;

        if (bulletsFired > 0 && currentAmmo > 0)
        {
            StartCoroutine(ResumeBurstAfterDelay());
        }
        else
        {
            StartCoroutine(ResetShotAfterDelay());
            if (!weapon.isAutomatic)
            {
                EndShot();
            }
        }
    }
    private IEnumerator ResumeBurstAfterDelay()
    {
        yield return new WaitForSeconds(weapon.burstDelay);
        ResumeBurst();
    }

    private void ResumeBurst()
    {
        ready = true;
        PerformShot();
    }

    private IEnumerator ResetShotAfterDelay()
    {
        yield return new WaitForSeconds(weapon.fireRate);
        ready = true;
    }

    private void Reload()
    {
        if (!reloading)
        {
            reloading = true;
            StartCoroutine(ReloadAfterDelay(weapon.reloadTime));
        }
    }

    private IEnumerator ReloadAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReloadComplete();
    }

    private void ReloadComplete()
    {
        currentAmmo = weapon.magazineSize;
        reloading = false;
    }


}
