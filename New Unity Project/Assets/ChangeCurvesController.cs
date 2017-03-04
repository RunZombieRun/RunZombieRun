using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace VacuumShaders
{
    namespace CurvedWorld
    {
        public class ChangeCurvesController : MonoBehaviour
        {
            public float[] xAxisBend;
            public float[] xBias;
            public float[] yAxisBend;
            public float[] yBias;

            public float m_Time;

            private float shag_xAxis;
            private float shag_xBias;
            private float shag_yAxis;
            private float shag_yBias;
            private float StartTime;


            CurvedWorld_Controller m_controller;

            private void Start()
            {
                m_Time *= 100;
                StartTime = m_Time;
                m_controller = FindObjectOfType<CurvedWorld_Controller>();
                StartCoroutine(ChangeCurves());
 
            }
            void SetParam(int id)
            {
                shag_xAxis = 2 * xAxisBend[id] / StartTime;

                shag_xBias = 2 * xBias[id] / StartTime;

                shag_yAxis = 2 * yAxisBend[id] / StartTime;

                shag_yBias = 2 * yBias[id] / StartTime;

            }

           private IEnumerator ChangeCurves()
            {
                for (int i = 0; i < xAxisBend.Length; i++)
                {
                    SetParam(i);
                    while (m_Time >= 0)
                    {
                        yield return new WaitForSeconds(0.01f);

                        m_controller._V_CW_Bend_X += shag_xAxis;
                        m_controller._V_CW_Bias_X += shag_xBias;
                        m_controller._V_CW_Bend_Y += shag_xAxis;
                        m_controller._V_CW_Bias_Y += shag_xBias;
                        m_Time--;
                    }
                    m_Time = StartTime;
                }

            }

        }
    }
}