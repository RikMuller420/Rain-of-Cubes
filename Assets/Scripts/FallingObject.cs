using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class FallingObject : MonoBehaviour
{
	[SerializeField] private MeshRenderer _renderer;

    private Color _defaultColor = Color.white;
    private bool _isFallen = false;
    private float _minLifeDuration = 2f;
    private float _maxLifeDuration = 5f;
    private UnityAction<FallingObject> _deactivate;

    public void SetDeactivateAction(UnityAction<FallingObject> deactivate)
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
        StartCoroutine(ActivateCube(lifeDuration));
    }

    private IEnumerator ActivateCube(float delay)
    {
        yield return new WaitForSeconds(delay);

        _deactivate?.Invoke(this);
    }
}
