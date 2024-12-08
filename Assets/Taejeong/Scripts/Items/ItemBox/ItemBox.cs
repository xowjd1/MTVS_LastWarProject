using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    /*
     x�ʸ��� �����ʿ��� ��ȯ�Ǽ� �����̵��� �Ѵ�.
     Ư�� : ���ǵ�, ü��
     
     �Ѿ˰� �浹�� ��
     itemHp = itemHp - bullet.damage;
     ���� itemHp <= 0 �̶��
     itemBox�� �ı�
     �÷��̾�� �����ϸ� ����Ʈ�� �����ϰ� �ɷ�ġ�� �ο��Ѵ�.

     �Ѿ˿� �ǰݽ� �����ϰ��� ���ߴٰ� ���ƿ´�.


     ī�޶� �ڱ��� �̵��ϸ� �ı�.

     ========================
     ü�� ����ui�� �ո鿡 ǥ��
     �Ѿ˿� �ı��Ǹ� ����Ʈ �����ǰ� �������� �÷��̾�� �̵��ϰ�
     �����۰� �÷��̾ �����ϸ� ����Ʈ�� �Բ� �÷��̾� ��ȭ

     */

   // public float speed = 10f; // �ӵ�
    public int itemHP; // ü��
    public int maxItemHP; // ������ �ִ�ü��
    public int boxDamage =1; // �ڽ��� �浹�Ҷ� �÷��̾ ���� ������
    Vector3 dir = Vector3.back; // �̵�����
    bool isHit = false; // �Ѿ˿� �¾Ҵ��� ����
    bool isDroneHit = false; // ����Ѿ˿� �¾Ҵ��� ����
    bool isMissileHit = false; // �̻��Ͽ� �¾Ҵ��� ����
    bool isMGBulletHit = false; // �ӽŰǿ� �¾Ҵ��� ����
   public bool isTutorial = false; // Ʃ�丮�� ���� ����
  

    

    // public GameObject Bullet;

    Bullet bullet;
    MGBullet mgBullet;
    DroneBullet dBullet;
    Missile missile;

    void OnEnable()
    {

        if (isTutorial) // Ʃ�丮�� �����̶�� ü���� 50���� �����.
            itemHP = 50;

        else // Ʃ�丮���� �ƴ϶�� ��������ð� * ���������� ü�°��� �����ش�.
        {
            maxItemHP = (int)(GameManager.instance.time * Random.Range(GameManager.instance.randomMin, GameManager.instance.randomMax) + Random.Range(80, 120));
            itemHP = maxItemHP;
        }
      

    }

    void Update()
    {
     
        if (isHit)
         {
          itemHP -= bullet.bDamage;

            isHit = false; // �������� �ѹ��� �޾ƾ��ϴϱ� falseó��
            //itemHP �� 0 ���ϰ� �Ǹ�
            if (itemHP <= 0)
            {
                ItemHPMinus(); // �̺�Ʈ(�Լ�)�� �ҷ��´�.
            }
         }
        if (isMGBulletHit)
        {
            itemHP -= mgBullet.mgDamage;
            isMGBulletHit = false; // �������� �ѹ��� �޾ƾ��ϴϱ� falseó��
            //itemHP �� 0 ���ϰ� �Ǹ�
            if (itemHP <= 0)
            {
                ItemHPMinus(); // �̺�Ʈ(�Լ�)�� �ҷ��´�.
            }
        }
        if (isDroneHit)
        {
            itemHP -= dBullet.damage;
            isDroneHit = false; // �������� �ѹ��� �޾ƾ��ϴϱ� falseó��
            //itemHP �� 0 ���ϰ� �Ǹ�
            if (itemHP <= 0)
            {
                ItemHPMinus(); // �̺�Ʈ(�Լ�)�� �ҷ��´�.
            }
        }
        if (isMissileHit)
        {
            itemHP -= missile.damage;
            isMissileHit = false; // �������� �ѹ��� �޾ƾ��ϴϱ� falseó��
            //itemHP �� 0 ���ϰ� �Ǹ�
            if (itemHP <= 0)
            {
                ItemHPMinus(); // �̺�Ʈ(�Լ�)�� �ҷ��´�.
            }
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // �浹�� �Ѿ��� Bullet ������Ʈ�� ������
            bullet = other.GetComponent<Bullet>(); 
            if (bullet != null)
            {
                StartCoroutine(ScaleBox());
                isHit = true;
                Debug.Log("Bullet Hit");
            }
        }
        if (other.CompareTag("MGBullet"))
        {
            // �浹�� �Ѿ��� Bullet ������Ʈ�� ������
            mgBullet = other.GetComponent<MGBullet>();
            if (mgBullet != null)
            {
                StartCoroutine(ScaleBox());
                isMGBulletHit = true;
                Debug.Log("Bullet Hit");
            }
        }
        if (other.CompareTag("DroneBullet"))
        {
            // �浹�� �Ѿ��� Bullet ������Ʈ�� ������
            dBullet = other.GetComponent<DroneBullet>();
            if (dBullet != null)
            {
                StartCoroutine(ScaleBox());
                isDroneHit = true;
                Debug.Log("DBullet Hit");
            }
        }
        if (other.CompareTag("Missile"))
        {
            // �浹�� �Ѿ��� Bullet ������Ʈ�� ������
            missile = other.GetComponent<Missile>();
            if (missile != null)
            {
                StartCoroutine(ScaleBox());
                isMissileHit = true;
                Debug.Log("Missile Hit");
            }
        }

    }


    void ItemHPMinus()
    {
        //����Ʈ ����, �ı��Ѵ�.
        Destroy(gameObject);

    }
    // �ǰݽ� �����ϰ� ����
    IEnumerator ScaleBox()
    {

        Vector3 originalParentScale = transform.localScale;


        transform.localScale = originalParentScale * 0.9f;

        yield return new WaitForSeconds(0.02f);

        transform.localScale = originalParentScale;
    }


}