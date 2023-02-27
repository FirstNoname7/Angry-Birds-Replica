using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //библиотека для перехода от сцены к сцене

public class Pig : MonoBehaviour
{
    public static int enemiesAlive = 0; //показываем кол-во живых врагов

    [SerializeField] GameObject deathEffect; //эффект для смерти свиней

    private float health = 4f;

    private void Start()
    {
        enemiesAlive++; //при старте показывается, что на карте есть 1 враг (объект, к которому прикреплен скрипт). Если этот скрипт прикреплён к 3-ем объектам, то при старте на карте будет показано, что есть 3 врага.
    }
    private void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.relativeVelocity.magnitude > health) //если у того, с чем столкнулась свинья (у птички), скорость относительно свиньи (colInfo.relativeVelocity.magnitude) больше значения здоровья свиньи (health), то:
        {
            Die(); //вызывается метод с именем Die
        }
    }

    void Die() //вызывается, когда свинью убили
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity); //включается анимация смерти (deathEffect) в позиции, где умерла свинья (transform.position) и без вращения (Quaternion.identity)
        enemiesAlive--; //уничтожаем 1 врага (того, к которому прикреплен этот скрипт)
        if (enemiesAlive <= 0) //если кол-во врагов = 0, то:
        {
            Debug.Log("Ты всех убил, можешь перейти на новый уровень!");
        }
        Destroy(gameObject); //удаляется игровой объект, к которому прикреплен этот скрипт (свинья)
    }
}
