using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpPositionSetter : MonoBehaviour
{
    [SerializeField] private Vector3 distanse = Vector3.up * 35f;
    public GameObject Target;
    [SerializeField] private RectTransform UItransform;


    public void Setup(GameObject target)
    {
        Target = target;
        UItransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (!Target.activeSelf)
        {
            Destroy(gameObject);
            return;
        }


        //������Ʈ ��ġ�� ���ŵ� ����UI�� ��ġ�� ���󰡾ߵȴ�.
        //�ٵ� ui�� canvas���� ����� �޴´� ��...
        //

        Vector3 ScreenPostion = Camera.main.WorldToScreenPoint(Target.transform.position);
        UItransform.position = ScreenPostion + distanse;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
