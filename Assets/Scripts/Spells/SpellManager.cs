using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : Singleton<SpellManager>
{
    [SerializeField] private FireBallSpell fireBallSpellPrefab;
    [SerializeField] private FireStormSpell fireStormSpellPrefab;
    [SerializeField] private FireStepSpell fireStepSpellPrefab;
    [SerializeField] private KunaiSpell kunaiSpellPrefab;
    [SerializeField] private LightningSpell lightningSpellPrefab;

    /*private List<FireBallSpell> fireBallSpellList = new List<FireBallSpell>();
    private List<FireStormSpell> fireStormSpellList = new List<FireStormSpell>();
    private List<FireStepSpell> fireStepSpellList = new List<FireStepSpell>();
    private List<KunaiSpell> kunaiSpellList = new List<KunaiSpell>();
    private List<LightningSpell> lightningSpellList = new List<LightningSpell>();*/

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddFireBallSpell()
    {
        FireBallSpell _fireBallSpell = Instantiate(fireBallSpellPrefab, PlayerController.Instance.transform.position, Quaternion.identity);
        _fireBallSpell.gameObject.transform.SetParent(PlayerController.Instance.transform);
        _fireBallSpell.ActiveSpell();
        //fireBallSpellList.Add(_fireBallSpell);
    }

    public void AddFireStormSpell()
    {
        FireStormSpell _fireStormSpell = Instantiate(fireStormSpellPrefab, PlayerController.Instance.transform.position, Quaternion.identity);
        _fireStormSpell.gameObject.transform.SetParent(PlayerController.Instance.transform);
        _fireStormSpell.ActiveSpell();
        //fireStormSpellList.Add(_fireStormSpell);
    }

    public void AddFireStepSpell()
    {
        FireStepSpell _fireStepSpell = Instantiate(fireStepSpellPrefab, PlayerController.Instance.transform.position, Quaternion.identity);
        _fireStepSpell.gameObject.transform.SetParent(PlayerController.Instance.transform);
        _fireStepSpell.ActiveSpell();
        //fireStepSpellList.Add(_fireStepSpell);
    }

    public void AddKunaiSpell()
    {
        KunaiSpell _kunaiSpell = Instantiate(kunaiSpellPrefab, PlayerController.Instance.transform.position, Quaternion.identity);
        _kunaiSpell.gameObject.transform.SetParent(PlayerController.Instance.transform);
        _kunaiSpell.ActiveSpell();
        //kunaiSpellList.Add(_kunaiSpell);
    }

    public void AddLightningSpell()
    {
        LightningSpell _lightningSpell = Instantiate(lightningSpellPrefab, PlayerController.Instance.transform.position, Quaternion.identity);
        _lightningSpell.gameObject.transform.SetParent(PlayerController.Instance.transform);
        _lightningSpell.ActiveSpell();
        //lightningSpellList.Add(_lightningSpell);
    }
}
