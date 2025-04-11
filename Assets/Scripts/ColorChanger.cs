using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Color _defaultColor = Color.white;

    public void ResetColor()
    {
        _renderer.material.color = _defaultColor;
    }

    public void SetRandomColor()
    {
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    public void SetColorAlpha(float alpha)
    {
        Color color = _renderer.material.color;
        color.a = alpha;
        _renderer.material.color = color;
    }
}
