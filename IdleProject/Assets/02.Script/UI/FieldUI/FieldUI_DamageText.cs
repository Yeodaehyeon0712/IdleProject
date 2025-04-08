using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class FieldUI_DamageText : BaseFieldUI
{
    #region Fields
    TextMeshProUGUI damageText;
    float _elapsedTime = 0;
    const float fadeTime = 0.75f;
    const float moveTime = 1f;
    const float disableTime = 2;

    #endregion

    #region Init Method
    public override BaseFieldUI Init(MemoryPool<BaseFieldUI>.del_Register register)
    {
        base.Init(register);
        damageText = transform.GetComponentInChildren<TextMeshProUGUI>();
        return this;
    }
    #endregion

    #region Damage Method
    public void Enabled(Vector3 position, double damage, bool isCritical, eActorType type)
    {
        var targetColor = type switch
        {
            eActorType.Character => Color.red,
            eActorType.Enemy => isCritical?Color.yellow:Color.white,
            _ => Color.white,
        };         

        if (damage >= 0f)
        {
            damageText.text = (type == eActorType.Enemy) ? damage.ToString() : "";
        }
        else
        {
            targetColor = Color.green;
            damageText.text = (-damage).ToString();
        }

        damageText.color = targetColor;
        transform.position = position;
        _elapsedTime = 0;

        gameObject.SetActive(true);
        Vector3 randomOffset = new Vector3(Random.Range(0.25f, 0.5f),Random.Range(0.5f, 1f),0f);
        damageText.DOFade(0, fadeTime);
        transform.DOMove(position + randomOffset, moveTime);
    }
    public void Disabled()
    {
        del_Register(this);
        gameObject.SetActive(false);
    }
    #endregion

    private void LateUpdate()
    {
        if(_elapsedTime > disableTime)
        {
            Disabled();
            return;
        }
        _elapsedTime += Time.deltaTime;
    }
}
