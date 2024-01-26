using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation;
    public Vector3 _minRotation;
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float MaxSpeed;
    public float MinSpeed;
    public float ProjectileSpeed;
    public GameObject PotencyBar;
    private float initialScaleX;

    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }
    void Update()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//guardem posició de la càmera
        mousePos.z = 0;
        var dist = mousePos - ShootPoint.transform.position;//distància entre el click i la bala
        var ang = (Mathf.Atan2(dist.y, dist.x) * 180f / Mathf.PI + offset);
        transform.rotation = Quaternion.Euler(0,0,ang);
        ShootPoint.transform.rotation = Quaternion.Euler(0, 0, ang);

        if(Input.GetMouseButton(0))
        {
            ProjectileSpeed += 0.4f;//cada frame s'ha de fer 4 cops més gran
        }
        if(Input.GetMouseButtonUp(0))
        {
            var projectile = Instantiate(Bullet, ShootPoint.transform.position, Quaternion.identity); //On s'instancia?
            if(ProjectileSpeed >= MaxSpeed) ProjectileSpeed = MaxSpeed ;
            projectile.GetComponent<Rigidbody2D>().velocity = dist.normalized * ProjectileSpeed;//quina velocitat ha de tenir la bala? s'ha de fer alguna cosa al vector direcció?
            ProjectileSpeed = MinSpeed;
        }
        CalculateBarScale();

    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            transform.localScale.y,
            transform.localScale.z);
    }
}
