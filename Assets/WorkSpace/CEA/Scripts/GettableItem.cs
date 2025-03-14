using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemList
{
    stone, gun
}

public interface IGetable
{
    public void GetItem(ItemData itemData);
}


public class GettableItem : MonoBehaviour, IGetable
{
    #region 아이템 회전 관련 변수
    [SerializeField]
    private float rotSpeed = 100.0f;

    [SerializeField]
    private GameObject ItemModel;
    #endregion

    [SerializeField]
    private ItemData itemData;
    [SerializeField]
    private ParticleSystem destroyParticle;

    public void GetItem(ItemData itemData)
    {
        //플레이어가 아이템의 정보를 받아올 메서드
        //player.getitem(itemdata)

        /*
         * 나 이거 받아왔어
         * 이 아이템 가지고 있어
         * 총 무기 장탄수
         * 
         * 맨손 총 짱돌
         *
         *플레이어가 아이템 받아올 그무언가....
         *에다가 저 정보를 저장해서
         *아이템 정보를 공격 등에 써먹음
         *장착,직접 공격 등
         */

        Debug.Log("플레이어에게 아이템 전달");
    }

    private void Update()
    {
        ItemModel.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetItem(itemData);
            Instantiate(destroyParticle, transform.position, Quaternion.identity);

            Destroy(gameObject);

           // Destroy(destroyParticle, 2.0f);
        }
    }
}
