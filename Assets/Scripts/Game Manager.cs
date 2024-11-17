using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region Enemy Settings
    [Header("Enemy Settings")]
    public int vultureCount = 0;

    public void RegisterVulture(Vulture vulture)
    {
        vultureCount++;
    }

    public void UnregisterVulture(Vulture vulture)
    {
        vultureCount--;
    }
    #endregion
}
