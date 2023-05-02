using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun", order = 0)]

public class Gun : ScriptableObject
{
    
    public GunType type;
    public string Name;
    public GameObject ModelPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;

    public ShootConfiguration ShootConfiguration;
    public TrailConfig TrailConfig;

    private MonoBehaviour ActiveMonoBehaviour;
    private GameObject Model;
    private float LastShootTime;
    private ParticleSystem shootSystem;
    private ObjectPool<TrailRenderer> TrailPool;

    public void Spawn(Transform Parent, MonoBehaviour ActiveMonoBehaviour)
    {
        this.ActiveMonoBehaviour = ActiveMonoBehaviour;
        LastShootTime = 0;
        TrailPool = new ObjectPool<TrailRenderer>(CreateTrail);
        Model = Instantiate(ModelPrefab);
        Model.transform.SetParent(Parent, false);
        Model.transform.localPosition = SpawnPoint;
        Model.transform.localRotation = Quaternion.Euler(SpawnRotation);

        shootSystem = Model.GetComponentInChildren<ParticleSystem>();
    }

    public void Shoot()
    {
        if(Time.time > ShootConfiguration.FireRate + LastShootTime)
        {
            LastShootTime = Time.time;
            shootSystem.Play(); 

            Vector3 shootDirection = shootSystem.transform.forward + 
            new Vector3
            (
                Random.Range(
                    -ShootConfiguration.Spread.x,
                    ShootConfiguration.Spread.x
                    ),
                Random.Range(
                    -ShootConfiguration.Spread.y,
                    ShootConfiguration.Spread.y
                    ),
                Random.Range(
                    -ShootConfiguration.Spread.z,
                    ShootConfiguration.Spread.z
                    )
            );

            shootDirection.Normalize();

            if(Physics.Raycast(shootSystem.transform.position, shootDirection, out RaycastHit Hit, float.MaxValue, ShootConfiguration.HitMask))
            {
                ActiveMonoBehaviour.StartCoroutine(PlayTrail(shootSystem.transform.position, Hit.point, Hit));
            }
            else
            {
                ActiveMonoBehaviour.StartCoroutine(PlayTrail(shootSystem.transform.position, shootSystem.transform.position + (shootDirection * TrailConfig.MissDistance), new RaycastHit()));                
            }
        }
    }

    private IEnumerator PlayTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit Hit)
    {
        TrailRenderer instance = TrailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = StartPoint;
        yield return null;
        
        instance.emitting = true;

        float distance = Vector3.Distance(StartPoint, EndPoint);
        float remainingDistance = distance;
        while(remainingDistance > 0)
        {
            instance.transform.position = Vector3.Lerp(StartPoint, EndPoint, Mathf.Clamp01(1 - (remainingDistance / distance)));
            remainingDistance -= TrailConfig.SimulationSpeed * Time.deltaTime;

            yield return null;
        }

        instance.transform.position = EndPoint;

        yield return new WaitForSeconds (TrailConfig.Duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false); 
        TrailPool.Release(instance);

    }

    private TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = TrailConfig.Color;
        trail.material = TrailConfig.Material;
        trail.widthCurve = TrailConfig.WidthCurve;
        trail.time = TrailConfig.Duration;
        trail.minVertexDistance = TrailConfig.MinVertexDistance;

        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }
}
