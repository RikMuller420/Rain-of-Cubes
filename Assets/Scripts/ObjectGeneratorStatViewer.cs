using TMPro;
using UnityEngine;

public class ObjectGeneratorStatViewer : MonoBehaviour
{
    [SerializeField] private ObjectGeneratorBase _objectGenerator;
    [SerializeField] private TextMeshProUGUI _droppedCountText;
    [SerializeField] private TextMeshProUGUI _instantiateCountText;
    [SerializeField] private TextMeshProUGUI _activeCountText;

    private void OnEnable()
    {
        _objectGenerator.ObjectDropped += UpdateDroppedCountText;
        _objectGenerator.ObjectInstantiated += UpdateInstantiateCountText;
        _objectGenerator.ActiveObjectCountChanged += UpdateActiveCountText;
    }

    private void OnDisable()
    {
        _objectGenerator.ObjectDropped -= UpdateDroppedCountText;
        _objectGenerator.ObjectInstantiated -= UpdateInstantiateCountText;
        _objectGenerator.ActiveObjectCountChanged -= UpdateActiveCountText;
    }

    private void UpdateDroppedCountText()
    {
        _droppedCountText.text = _objectGenerator.DroppedCount.ToString();
    }

    private void UpdateInstantiateCountText()
    {
        _instantiateCountText.text = _objectGenerator.InstantiateCount.ToString();
    }

    private void UpdateActiveCountText()
    {
        _activeCountText.text = _objectGenerator.ActiveCount.ToString();
    }
}
