using System;
using _MainAssets.Scripts;
using _MainAssets.Scripts.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

public class TouchHandler : MonoBehaviour
{
    #region Singleton

    private static TouchHandler instance;

    public static TouchHandler Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<TouchHandler>();
            if (instance == null)
                Debug.LogError("TouchHandler not found");
            return instance;
        }
    }

    #endregion

    [SerializeField] private float _fingerControlSense = 1f;
    [SerializeField] private float _radius = 400f;
    [SerializeField] private GameObject panel;

    public Vector2 StartPosition { get; private set; }
    public Vector2 CurrentPosition { get; private set; }
    public Vector2 TempDelta { get; private set; }
    public bool Boosted { get; private set; }
    public static Action<Vector2> OnBeginDrag;
    public static Action<Vector2> OnDrag;
    public static Action OnEndDrag;

    private float _sqrRadius;
    private Vector2 _touchDelta;
    public bool Pressed { get; private set; }

    private void OnEnable()
    {
        CheckInstance();
        GameController.gameStarted += EnablePanel;
        GameController.enemiesDestroyed += DisablePanel;
        GameController.gameEnded += DisablePanel;
        Boosted = false;
        _sqrRadius = _radius * _radius;
    }

    private void OnDisable()
    {
        GameController.gameStarted -= EnablePanel;
        GameController.enemiesDestroyed -= DisablePanel;
        GameController.gameEnded -= DisablePanel;
    }


    private void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void EnablePanel()
    {
        panel.SetActive(true);
    }

    private void DisablePanel()
    {
        panel.SetActive(false);
        TempDelta = Vector2.zero;
    }


    public void BeginControl()
    {
        GameController.Instance.StartGame();
        if (Input.touchCount > 0)
        {
            StartPosition = Input.touches[0].position;
            Pressed = true;
            OnBeginDrag.Invoke(StartPosition);
        }
    }

    public void Drag()
    {
        if (Input.touchCount > 0)
        {
            CurrentPosition = Input.touches[0].position;
            _touchDelta = CurrentPosition - StartPosition;
            _touchDelta = Vector2.ClampMagnitude(_touchDelta, _radius);
            TempDelta = _touchDelta * _fingerControlSense;
            OnDrag.Invoke(StartPosition + _touchDelta);
        }
    }

    public void StopControl()
    {
        TempDelta = Vector2.zero;
        Pressed = false;
        OnEndDrag.Invoke();
    }
}