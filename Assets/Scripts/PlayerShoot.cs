using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    #region UI
    [SerializeField] TMP_Text currentAmmoText;
    [SerializeField] TMP_Text magSizeText;
    #endregion

    #region References
    [SerializeField] Transform shootPoint;
    [SerializeField] Camera cam;
    [SerializeField] ParticleSystem muzzleFlash;
    #endregion
    

    #region Variables
    int currentAmmo;
    [SerializeField] int magSize;
    [SerializeField] float fireRate;
    [SerializeField] float reloadTime;
    bool reloading;
    float timesinceLastShoot;
    #endregion

    private void Start()
    {
        currentAmmo = magSize;
        currentAmmoText.text = currentAmmo.ToString();
        magSizeText.text = magSize.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        if (Input.GetMouseButton(0))
        {
            if(currentAmmo > 0 && canShoot())
            {
                Fire();
                currentAmmo--;
                currentAmmoText.text = currentAmmo.ToString();
                timesinceLastShoot = 0;

            }
        }
        timesinceLastShoot += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.R)) 
        {
            StartReload();
        }
        
    }
    void Fire()
    {
        GameObject bullet = ObjectPool.Instance.getBullet();
        if (bullet != null)
        {
            muzzleFlash.Play();   
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = shootPoint.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<BulletScript>().Shoot(Aim().normalized);
            AudioManager.Instance.PlayBulletSFX("AK Shoot Bullet Sound");
        }
    }
    Vector3 Aim()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit raycastHit;
        Physics.Raycast(ray, out raycastHit, 9999f);
        //Debug.Log(raycastHit.collider.gameObject.name);
        Debug.DrawRay(ray.origin, ray.direction * 9999f, Color.red);
        return raycastHit.point - shootPoint.position;
        
    }
    bool canShoot() => !reloading && timesinceLastShoot > 1f / (fireRate / 60f);

    void StartReload()
    {
        if(!reloading)
        {
            AudioManager.Instance.PlayReloadSFX("AK Reload Sound");
            StartCoroutine(Reload());
        }
    }
    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magSize;
        currentAmmoText.text = currentAmmo.ToString();
        reloading = false; 
    }
}
