using DIALOGUE;
using System.Collections;
using UnityEngine;

public class CanvasGroupController 
{
    private const float DEFAULT_FADE_SPEED = 3F;

    private MonoBehaviour owner;
    private CanvasGroup rootCG;

    private Coroutine co_showing = null;
    private Coroutine co_hiding = null;

    public bool isShowing => co_showing != null;
    public bool isHiding => co_hiding != null;
    public bool isFading => isShowing || isHiding;
    public bool isVisible => co_showing != null || rootCG.alpha > 0;
    public float alpha { get { return rootCG.alpha; } set { rootCG.alpha = value; } }
   
    public CanvasGroupController(MonoBehaviour owner, CanvasGroup rootCG)
    {
        this.owner = owner;
        this.rootCG = rootCG;
    }

    public void SetInteractableState(bool active)
    {
        rootCG.interactable = active;
        rootCG.blocksRaycasts = active;
    }

    public Coroutine Show()
    {
        if(isShowing)
            return co_showing;

        else if (isHiding)
        {
            DialogueSystem.instance.StopCoroutine(co_hiding);
            co_hiding = null;
        }

        co_showing = DialogueSystem.instance.StartCoroutine(Fading(1));
        return co_showing;
    }

    public Coroutine Hide()
    {
        if (isHiding)
            return co_hiding;

        else if (isShowing)
        {
            DialogueSystem.instance.StopCoroutine(co_showing);
            co_showing = null;
        }

        co_hiding = DialogueSystem.instance.StartCoroutine(Fading(0));
        return co_hiding;
    }

    private IEnumerator Fading (float alpha)
    {
        CanvasGroup cg = rootCG;

        while(cg.alpha != alpha)
        {
            cg.alpha = Mathf.MoveTowards(cg.alpha, alpha, Time.deltaTime * DEFAULT_FADE_SPEED);
            yield return null;
        }

        co_showing = null;
        co_hiding = null;
    }
}
