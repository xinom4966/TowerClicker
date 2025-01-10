using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BoidSystem : MonoBehaviour
{
    public Transform boidPrefab;
    public int numberOf;
    Boid[] boids;
    [SerializeField] BoidSettings settings;
    BoidRegions regions = new BoidRegions();
    public Transform attractor;
    private void Start()
    {
        boids = new Boid[numberOf];
        regions.Add(Vector3Int.zero, new List<Boid>());
        for (int i = 0; i < numberOf; i++)
        {
            boids[i] = new Boid { transform = Instantiate(boidPrefab, transform), velocity = UnityEngine.Random.insideUnitSphere };
            regions[Vector3Int.zero].Add(boids[i]);
        }
    }

    private void Update()
    {
        ComputeNextVelocities();
        ApplyNextVelocities();
    }

    void ComputeNextVelocities()
    {
        Vector3 attractorPosition = attractor ? attractor.position : Vector3.zero;

        Parallel.For(0, boids.Length, i =>
        {
            boids[i].ComputeNextVelocities(settings, regions, attractorPosition);
        });
    }

    void ApplyNextVelocities()
    {
        for (int i = 0; i < boids.Length; i++)
        {
            boids[i].ApplyNextVelocities(settings, regions);
        }
    }

    struct Boid
    {
        public Transform transform;
        public Vector3 position;
        public Vector3 velocity;
        public Vector3 nextVelocity;
        Vector3Int region;

        public void ComputeNextVelocities(BoidSettings settings, BoidRegions regions, Vector3 attractor)
        {
            Vector3 alignment = Vector3.zero;
            Vector3 avoidance = Vector3.zero;
            Vector3 cohesion = Vector3.zero;

            int counter = 0;

            foreach (Boid b in regions.GetBoidsNearTo(region))
            {
                if (b.transform == transform)
                {
                    continue;
                }

                //alignment
                Vector3 direction = b.velocity;
                float distance = Vector3.Distance(position, b.position);
                alignment += Vector3.ClampMagnitude(direction / Mathf.Max(distance, 0.1f), 1);

                //avoidance
                direction = position - b.position;
                distance = direction.magnitude / settings.farThreshold;
                avoidance += direction.normalized * (1 - distance);

                //cohesion
                direction *= -1;
                if (distance > settings.farThreshold)
                {
                    cohesion += Vector3.ClampMagnitude(direction.normalized * (distance - 1), 1);
                }

                if (counter++ > settings.maxIterations)
                {
                    break;
                }
            }

            nextVelocity = alignment * settings.alignment +
                avoidance * settings.avoidance +
                cohesion * settings.cohesion +
                (attractor - position).normalized * settings.attraction;
            nextVelocity.Normalize();
        }

        public void ApplyNextVelocities(BoidSettings settings, BoidRegions regions)
        {
            velocity = Vector3.Slerp(velocity, nextVelocity, settings.turnRate);
            position = transform.position += velocity * settings.speed * Time.deltaTime;
            transform.forward = velocity;

            Vector3Int newRegion = new Vector3Int
            {
                x = Mathf.FloorToInt(transform.position.x / settings.farThreshold),
                y = Mathf.FloorToInt(transform.position.y / settings.farThreshold),
                z = Mathf.FloorToInt(transform.position.z / settings.farThreshold)
            };

            if (newRegion == region)
            {
                return;
            }
            regions[region].Remove(this);

            if (!regions.ContainsKey(newRegion))
            {
                regions[newRegion] = new List<Boid>();
            }

            regions[newRegion].Add(this);

            region = newRegion;
        }
    }

    [Serializable]
    class BoidSettings
    {
        public float alignment;
        public float avoidance;
        public float cohesion;
        public float attraction;
        public float farThreshold;
        public float speed;
        public float turnRate;
        public int maxIterations;
    }

    class BoidRegions : Dictionary<Vector3Int, List<Boid>>
    {
        public IEnumerable<Boid> GetBoidsNearTo(Vector3Int region)
        {

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        Vector3Int testedRegion = region + new Vector3Int(x, y, z);

                        if (!ContainsKey(testedRegion))
                        {
                            continue;
                        }

                        foreach (Boid b in this[testedRegion])
                        {
                            yield return b;
                        }
                    }
                }
            }
        }
    }
}
