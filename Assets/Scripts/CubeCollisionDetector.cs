using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class CubeCollisionDetector : MonoBehaviour
{
	[SerializeField] private MeshRenderer _renderer;

    private Color _defaultColor = Color.white;
    private UnityAction<CubeCollisionDetector> _deactivate;
    private bool _isFallen = false;
    private float _minLifeDuration = 2f;
    private float _maxLifeDuration = 5f;

    public void InitSettings(UnityAction<CubeCollisionDetector> deactivate)
    {
        _deactivate = deactivate;
    }

    public void ResetState()
    {
        _renderer.material.color = _defaultColor;
        _isFallen = false;
    }

    private void OnCollisionEnter(Collision collision)
	{
        if (_isFallen)
        {
            return;
        }

		_isFallen = true;
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
        float lifeDuration = Random.Range(_minLifeDuration, _maxLifeDuration);
        Invoke(nameof(Deactivate), lifeDuration);
    }

    private void Deactivate()
    {
        _deactivate?.Invoke(this);
    }
}
