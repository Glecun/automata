using System;
using UnityEngine;
using static Utils;

public class HumanController : MonoBehaviour
{
    public GameObject infoPopupPrefab;
    public HumanResourceController humanResourceController;

    [NonSerialized] public const float speed = 4f;
    [NonSerialized] public GenderEnum GenderEnum;

    private void Awake()
    {
        GenderEnum = getRandomGender();
        humanResourceController = new HumanResourceController();
    }

    private static GenderEnum getRandomGender()
    {
        var values = Enum.GetValues(typeof(GenderEnum));
        return (GenderEnum) values.GetValue(random(0, values.Length - 1));
    }
}

public enum GenderEnum
{
    MALE,
    FEMALE
}