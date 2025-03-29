using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FallingObject : MonoBehaviour
{
	[SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Color _defaultColor = Color.white;

    private bool _isFallen = false;
    private float _minLifeDuration = 2f;
    private float _maxLifeDuration = 5f;
    private System.Action _deactivateAction;

    public void SetDeactivateAction(System.Action deactivate)
    {
        _deactivateAction = deactivate;
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
        StartCoroutine(DeactivateCube(lifeDuration));
    }

    private IEnumerator DeactivateCube(float delay)
    {
        yield return new WaitForSeconds(delay);

        _deactivateAction?.Invoke();
    }
}
