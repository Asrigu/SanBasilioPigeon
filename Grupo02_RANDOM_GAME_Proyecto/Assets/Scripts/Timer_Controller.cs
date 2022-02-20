using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Controller : MonoBehaviour
{
    // Variables y campos a usar
        [SerializeField] private int minutes;
    
        [SerializeField] private int seconds;
    
        private int m, s;
    
        [SerializeField] private Text timer_Text;
    
        private GameManager _gameManager;

       public AudioSource tiktak;

        // Start is called before the first frame update
        void Start()
        {
            _gameManager = Object.FindObjectOfType<GameManager>();
            
            tiktak.Stop();
        }
    
        // Lógica para comenzar la cuenta atrás
        #region Start Timer Method
        public void startTimer()
        {
            m = minutes;
            s = seconds;
            tiktak.Play();
            writeTimer(m, s);
            Invoke("updateTimer", 1f);
        }
        #endregion
        
        // Lógica para parar el contador
        #region Stop Timer Method
        public void stopTimer()
        {
            tiktak.Stop();
            CancelInvoke();
        }
        #endregion
    
        // Lógica para ejecución en intervalos de 1 seg
        #region Update Timer Method
        public void updateTimer()
        {
            s--;
            if (s < 0)
            {
                if (m == 0)
                {
                    _gameManager.EndGame();
                    return;
                }
    
                else
                {
                    m--;
                    s = 59;
                }
            }
    
            writeTimer(m, s);
            Invoke("updateTimer", 1f);
        }
        #endregion
        
        // Lógica para escribir el timer
        #region Write Timer Method
        private void writeTimer(int m, int s)
        {
            if (s < 10)
            {
                timer_Text.text = m.ToString() + ":0" + s.ToString();
            }
            else
            {
                timer_Text.text = m.ToString() + ":" + s.ToString();
            }
        }
        #endregion
}
