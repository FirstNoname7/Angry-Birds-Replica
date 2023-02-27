using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //���������� ��� �������� �� ����� � �����

public class Pig : MonoBehaviour
{
    public static int enemiesAlive = 0; //���������� ���-�� ����� ������

    [SerializeField] GameObject deathEffect; //������ ��� ������ ������

    private float health = 4f;

    private void Start()
    {
        enemiesAlive++; //��� ������ ������������, ��� �� ����� ���� 1 ���� (������, � �������� ���������� ������). ���� ���� ������ ��������� � 3-�� ��������, �� ��� ������ �� ����� ����� ��������, ��� ���� 3 �����.
    }
    private void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.relativeVelocity.magnitude > health) //���� � ����, � ��� ����������� ������ (� ������), �������� ������������ ������ (colInfo.relativeVelocity.magnitude) ������ �������� �������� ������ (health), ��:
        {
            Die(); //���������� ����� � ������ Die
        }
    }

    void Die() //����������, ����� ������ �����
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity); //���������� �������� ������ (deathEffect) � �������, ��� ������ ������ (transform.position) � ��� �������� (Quaternion.identity)
        enemiesAlive--; //���������� 1 ����� (����, � �������� ���������� ���� ������)
        if (enemiesAlive <= 0) //���� ���-�� ������ = 0, ��:
        {
            Debug.Log("�� ���� ����, ������ ������� �� ����� �������!");
        }
        Destroy(gameObject); //��������� ������� ������, � �������� ���������� ���� ������ (������)
    }
}
