using UnityEngine;

public class CustomProceduralWalk : MonoBehaviour
{

    [SerializeField] private LayerMask _terrainLayer;

    [SerializeField] private int _maxRaycastDistance;

    [SerializeField] private Transform _body;

    [SerializeField] private float _lerpMax;

    [SerializeField] private float _stepDistance;

    [SerializeField] private float _stepHeight;

    [SerializeField] private float _speed;

    private Vector3 _oldPosition, _currentPosition, _newPosition;

    private float _lerp;

    private void Start()
    {

        _lerp = 1;
    }

    private void Update()
    {
        transform.position = _currentPosition;

        Ray ray = new Ray(_body.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit info, _maxRaycastDistance, _terrainLayer.value))
        {
            if (Vector3.Distance(_newPosition, info.point) > _stepDistance)
            {
                if (_lerp >= _lerpMax)
                {
                    _lerp = 0;
                }

                _newPosition = info.point;
            }

            if (_lerp <= 1)
            {
                Vector3 tempPosition = Vector3.Lerp(_oldPosition, _newPosition, _lerp);
                tempPosition.y += Mathf.Sin(_lerp * Mathf.PI) * _stepHeight;

                _currentPosition = tempPosition;
                _lerp += Time.deltaTime * _speed;
            }
            else
            {
                _oldPosition = _newPosition;
            }
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_newPosition, 0.5f);
    }
}
