using UnityEngine;
using UnityEngine.AI;

public class RobotWalker : MonoBehaviour
{
    [SerializeField] private LayerMask _terrainLayer;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit info, 1000f, _terrainLayer))
            {
                _agent.SetDestination(info.point);
            }
        }
    }

}