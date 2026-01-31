using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flock: MonoBehaviour
{
    [SerializeField]
    public int id;
    public List<FlockAgent> agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(1, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.5f;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareNeighborRadius;
    float squareAvoidanceRadius;

    //property is still a variable that can be readable or overwritten or both
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Variables used to avoid from
    public Transform fox;
    public Transform egg;
    public Transform fur;

    // this is public because this will be referenced in the StayinRadiusBehavior script as the center of the fishflock
    public Vector3 fishSpawnPoint;

    List<float> yRotation = new List<float>() { 0f, 180f };

    public Transform player;
    int typeOfFishes;
    int numberOfFishes;

    int layerIndex;
    int layerMask;

    private void Awake()
    {
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier;

        fishSpawnPoint = transform.position;

        typeOfFishes = agentPrefab.Count;
        numberOfFishes = startingCount / typeOfFishes;
    }


    void Start()
    {
        for (int j = 0; j < typeOfFishes; j++)
        {
            for (int i = 0; i < numberOfFishes; i++)
            {
                int randomIndex = Random.Range(0, 2);
                float currentYRotation = yRotation[randomIndex];
                Vector2 random2D = Random.insideUnitCircle * startingCount * AgentDensity;
                Vector3 spawnPos = fishSpawnPoint + new Vector3(random2D.x, 0f, random2D.y);
                FlockAgent newAgent = Instantiate(
                    agentPrefab[j],
                    spawnPos,
                    Quaternion.Euler(0, currentYRotation, 0),
                    transform
                    );
                newAgent.name = "Agent: " + (j * numberOfFishes + i + 1) + " type: " + j + " group: " + id;
                newAgent.Initialize(this);
                agents.Add(newAgent);
            }
        }

        layerIndex = LayerMask.NameToLayer("SeaAnimal");
        layerMask = 1 << layerIndex;
    }

    void FixedUpdate()
    {
        foreach (FlockAgent agent in agents)
        {
            //this context list contains all nearby objects within neighborRadius excluding the agent itself
            List<Transform> context = GetNearbyObjects(agent);

            //this refers to the current flock this particular agent belongs to
            Vector3 move = behavior.CalculateMove(agent, context, this);
            agent.Move(move);
        }
    }


    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        //here context list contains all nearbyObjects
        //it seems that it includes stones here as well
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius, layerMask);
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider && c.gameObject.tag == "Fish")
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

}
