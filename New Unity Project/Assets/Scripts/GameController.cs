using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


        public class GameController : MonoBehaviour
        {
            public static Action<GameObject> DeadMinion;
            public static Action<int> ScoreChanged;
            public static Action<float> StaminaChanged;
            public static Action<int> ArmorChanged;
            public static Action<int> HealthChanged;
            public List<GameObject> enemies; //Если будем хранить врагов, то пригодится

            public static GameController instance;
            private float start_speed;
            [SerializeField]
            private int m_Health;
            public int Health
            {
                set
                {
                    m_Health = value;
                    CheckHP();
                    HealthChanged.Invoke(m_Health);
                }
                get
                {
                    return m_Health;
                }
            }

            [SerializeField]
            private int m_Score;
            public int Score
            {
                set
                {
                    m_Score = value;
                    CheckHP();
                    ScoreChanged.Invoke(m_Score);

                }
                get
                {
                    return m_Score;
                }
            }

            [SerializeField]
            private float m_Stamina;
            public float Stamina
            {
                get
                {
                    return m_Stamina;

                }
                set
                {
                    m_Stamina = value;
                    StaminaChanged.Invoke(m_Stamina);
                }
            }

            [SerializeField]
            private int m_Armor;
            public int Armor
            {
                get
                {
                    return m_Armor;

                }
                set
                {
                    m_Armor = value;
                    ArmorChanged.Invoke(m_Armor);
                }
            }

            void Awake()
            {
                if (instance == null)
                {
                    instance = this;
                }
                else
                {
                    Destroy(instance.gameObject);
                    instance = this;
                }
            }

            //Подписываем на эвенты
            void OnEnable()
            {
                DeadMinion += DeadSound;
            }

            void OnDisable()
            {
                DeadMinion -= DeadSound;
            }
            private void Start()
            {
               start_speed = Runner_SceneManager.get.speed;
            }
            private void CheckHP()
            {
                if (m_Health <= 0)
                {
                    m_Health = 0;
                    Destroy(Player_Controller.get.gameObject);
                }
            }
            private void DoSomethingWithScore()
            {

                Debug.Log("ScoreChanges");
            }

            private void DeadSound(GameObject obj)
            {
                enemies.Remove(obj);
                Debug.Log("Minion is Dead");
            }

            public void Restart()
            {
                Application.LoadLevel(0);
            }

            public void SpeedBoost(int time, float BoostMultiplier)
            {
                StartCoroutine(BoostCoroutine(time, BoostMultiplier));
            }
            IEnumerator BoostCoroutine(int time, float BoostMultiplier)
            {            
                Runner_SceneManager.get.speed *= BoostMultiplier;
                yield return new WaitForSeconds(time);
                Runner_SceneManager.get.speed *= start_speed;

            }
        }
