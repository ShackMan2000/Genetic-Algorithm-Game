using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDummy : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRender;
    private Vector2 goalPosition;

    [SerializeField]
    private GameSettings settings;

    [SerializeField]
    private float speed = 1, rotationSpeed = 0;


    private void Start()
    {
        if (GameManager.Instance.currentLevel.gateColors.Length == 1)
            spriteRender.color = settings.colors[GameManager.Instance.currentLevel.gateColors[0]];
        else
            spriteRender.color = settings.colors[GameManager.Instance.currentLevel.mergedColor];


        goalPosition = GameManager.Instance.currentLevel.goalPosition;
        transform.position = Random.insideUnitCircle * 0.3f + goalPosition;
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
        StartCoroutine(FireWheel());
    }

    private void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime);
    }



    private IEnumerator FireWheel()
    {
        float maxTurn = 60;
        while (maxTurn > 0)
        {
            float turnAdjust = rotationSpeed * 360 * Time.deltaTime;
            maxTurn -= turnAdjust;
            var newRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + turnAdjust);
            transform.eulerAngles = newRotation;
            yield return new WaitForSeconds(0.1f);
        }

    }


}
