using System.Collections;
using Mcm.Implementation.Displayables;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mcm.Implementation.Components;

public class ButtonHighlight : HoverBehaviour
{
    private bool _cancel;
    private Color? _original;

    public required McmButton Button { get; set; }

    protected override void OnDisable()
    {
        _cancel = true;
        var outline = Button.Ref?.GetComponentInChildren<Outline>();
        if (outline == null) {
            return;
        }

        outline.effectColor = _original ?? Color.white;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!Button.Interactable) {
            _cancel = true;
            return;
        }

        var outline = Button.Ref?.GetComponentInChildren<Outline>();
        if (outline is null) {
            return;
        }

        _original ??= outline.effectColor;
        _cancel = false;
        StartCoroutine(GradientColor(outline));
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        OnDisable();
    }

    private IEnumerator GradientColor(Outline outline)
    {
        while (outline.isActiveAndEnabled && !_cancel) {
            yield return outline.StartCoroutine(LerpColor(outline, Color.green, Color.cyan, 2f));
            yield return outline.StartCoroutine(LerpColor(outline, Color.cyan, Color.blue, 2f));
            yield return outline.StartCoroutine(LerpColor(outline, Color.blue, Color.magenta, 2f));
            yield return outline.StartCoroutine(LerpColor(outline, Color.magenta, Color.red, 2f));
            yield return outline.StartCoroutine(LerpColor(outline, Color.red, Color.yellow, 2f));
            yield return outline.StartCoroutine(LerpColor(outline, Color.yellow, Color.green, 2f));
        }
    }

    private IEnumerator LerpColor(Outline outline, Color startColor, Color endColor, float duration)
    {
        float time = 0;
        while (time < duration && !_cancel) {
            outline.effectColor = Color.Lerp(startColor, endColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}