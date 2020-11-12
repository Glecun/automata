using TMPro;
using UnityEngine;

public class InfoPopupController : MonoBehaviour
{
    private const float MOVE_Y_SPEED = 0.3f;
    private const float DISAPPEAR_SPEED = 1.3f;
    private const float DISAPPEAR_TIMER = 0.5f;

    private TextMeshPro textMeshPro;
    private float disappearTimer;
    private Color textColor;

    public static void Create(GameObject infoPopupPrefab, Vector3 position, string msg)
    {
        var infoPopupPrefabCreated = InstantiateUtils.Instantiate(infoPopupPrefab, position, Quaternion.identity);
        var popupController = infoPopupPrefabCreated.GetComponent<InfoPopupController>();
        popupController.Setup(msg);
    }

    private void Setup(string msg)
    {
        textMeshPro.SetText(msg);
        textColor = textMeshPro.color;
        disappearTimer = DISAPPEAR_TIMER;
    }

    private void Awake()
    {
        textMeshPro = transform.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.position += new Vector3(0, MOVE_Y_SPEED) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            textColor.a -= DISAPPEAR_SPEED * Time.deltaTime;
            textMeshPro.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}