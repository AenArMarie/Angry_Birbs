using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlingshotHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> birds;
    private int _currentBirdCount = 0;
    private bool active = true;

    private void Awake()
    {
        NextBirdReady();
    }
    public bool NextBirdReady() //готовим следующую птицу к запуску
    {
        if (_currentBirdCount < birds.Count && active)
        {
            birds[_currentBirdCount].transform.DOMove(transform.position, 1f).SetEase(Ease.OutQuad);
            birds[_currentBirdCount].GetComponent<Slingshot>().SetActive(transform);
            _currentBirdCount++;
            return true;
        }
        else { return false; }
        
    }

    public void SetFreezeBird(bool status)
    {
        birds[_currentBirdCount-1].GetComponent<Slingshot>().SetFrozenStatus(status);
    }

    public void MakeActive(bool status)
    {
        active = status;
    }
}
