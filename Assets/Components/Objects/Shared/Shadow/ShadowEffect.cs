using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShadowEffect : MonoBehaviour
{
    public Vector3 Offset = new Vector3(-0.1f, -0.1f);
    public float angle = 0;
    public float2 scale = new float2(1, 1);
    public Material Material;

    private GameObject shadow;
    private SpriteRenderer parentRenderer;
    private SpriteRenderer shadowRenderer;

    void Start()
    {
        shadow = new GameObject("Shadow");
        shadow.transform.parent = transform;

        shadow.transform.localPosition = Offset;
        shadow.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        shadow.transform.localScale = new Vector3(scale.x, scale.y, 1);

        parentRenderer = GetComponent<SpriteRenderer>();
        shadowRenderer = shadow.AddComponent<SpriteRenderer>();
        shadowRenderer.sprite = parentRenderer.sprite;
        shadowRenderer.material = Material;
        shadowRenderer.sortingLayerName = parentRenderer.sortingLayerName;
        shadowRenderer.sortingOrder = parentRenderer.sortingOrder - 1;
    }

    private void LateUpdate()
    {
        shadowRenderer.sortingOrder = parentRenderer.sortingOrder - 1;
        shadow.transform.localPosition = Offset;
        shadow.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        shadow.transform.localScale = new Vector3(scale.x, scale.y, 1);
    }
}