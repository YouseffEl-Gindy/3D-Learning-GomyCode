using UnityEngine;

public class BulletScript : MonoBehaviour
{
    #region References
    [SerializeField] Rigidbody rgd;
    [SerializeField] TrailRenderer trail;
    #endregion
    
    #region Variables
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    float trailTime;
    #endregion


    public void Shoot(Vector3 dir)
    {
        rgd.AddForce(dir * speed * Time.deltaTime, ForceMode.Impulse);
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        Reset();
    }
    public void Reset()
    {
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        rgd.WakeUp();
        Invoke("Reset", lifeTime);
    }
    private void OnDisable()
    {
        rgd.Sleep();
        trail.Clear();
        CancelInvoke();
    }
    private void ResetTrail()
    {
        trail.time = trailTime;
    }



}