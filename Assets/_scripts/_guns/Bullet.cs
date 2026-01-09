using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage = 10f;
    
    TrailRenderer _trailRenderer;
    
    void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    void Start()
    {
        SetRandomDefaultValues();
    }

    void SetRandomDefaultValues()
    {
        SetTrailTime(Random.Range(0.05f, 0.1f));
        SetTrailWidth(Random.Range(0.045f, 0.005f), Random.Range(0, 0.005f));
        SetTrailMinVertexDistance(Random.Range(0.01f, 0.5f));
    }
    
    /// <summary>
    /// Устанавливает время жизни следа (в секундах)
    /// </summary>
    public void SetTrailTime(float time)
    {
        if (_trailRenderer != null)
        {
            _trailRenderer.time = time;
        }
    }
    
    /// <summary>
    /// Устанавливает ширину следа (начальная и конечная)
    /// </summary>
    public void SetTrailWidth(float startWidth, float endWidth)
    {
        if (_trailRenderer != null)
        {
            _trailRenderer.startWidth = startWidth;
            _trailRenderer.endWidth = endWidth;
        }
    }
    
    /// <summary>
    /// Устанавливает минимальное расстояние между вершинами (влияет на длину следа)
    /// </summary>
    public void SetTrailMinVertexDistance(float distance)
    {
        if (_trailRenderer != null)
        {
            _trailRenderer.minVertexDistance = distance;
        }
    }
    
    /// <summary>
    /// Устанавливает все параметры следа одновременно
    /// </summary>
    public void SetTrailProperties(float time, float startWidth, float endWidth, float minVertexDistance = 0.1f)
    {
        if (_trailRenderer != null)
        {
            _trailRenderer.time = time;
            _trailRenderer.startWidth = startWidth;
            _trailRenderer.endWidth = endWidth;
            _trailRenderer.minVertexDistance = minVertexDistance;
        }
    }
}
