using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitMover : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _agent.SetDestination(targetPosition);
    }

    public bool HasReachedDestination(float stoppingDistance)
    {
        if (_agent.pathPending == false && _agent.remainingDistance <= stoppingDistance)
        {
            return true;
        }

        return false;
    }
}