using ChronoArkMod.Helper;
using UnityEngine.UI;

namespace Mcm.Implementation.Displayables;

internal class McmSeparator : ScriptRef, ILine
{
    private Color _color = Color.gray;
    private float _thickness = 5f;

    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            DeferredUpdate();
        }
    }
    public float Thickness
    {
        get => _thickness;
        set
        {
            _thickness = value;
            DeferredUpdate();
        }
    }

    public override Transform Render(Transform parent)
    {
        if (Ref != null) {
            return Ref.transform;
        }

        var line = parent.AttachRectTransformObject("McmSeparator");
        if (Size == null) {
            line.SetToStretch();
        } else {
            line.sizeDelta = Size.Value;
        }

        line.AddComponent<Image>();
        DeferredUpdate();

        return base.Render(line);
    }

    public override void Update()
    {
        Ref!.GetComponent<Image>().color = _color;
        Ref!.GetComponent<LayoutElement>().preferredHeight = _thickness;
    }
}
