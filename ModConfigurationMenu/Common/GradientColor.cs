using System.Collections;
using UnityEngine.UI;

namespace MCM.Common;

internal static class ImageGradientColor
{
    internal static IEnumerator GradientColor(this Image image)
    {
        while (image.isActiveAndEnabled) {
            yield return image.StartCoroutine(image.LerpColor(Color.red, Color.yellow, 2f));
            yield return image.StartCoroutine(image.LerpColor(Color.yellow, Color.green, 2f));
            yield return image.StartCoroutine(image.LerpColor(Color.green, Color.cyan, 2f));
            yield return image.StartCoroutine(image.LerpColor(Color.cyan, Color.blue, 2f));
            yield return image.StartCoroutine(image.LerpColor(Color.blue, Color.magenta, 2f));
            yield return image.StartCoroutine(image.LerpColor(Color.magenta, Color.red, 2f));
        }
    }

    internal static IEnumerator LerpColor(this Image image, Color startColor, Color endColor, float duration)
    {
        float time = 0;
        while (time < duration) {
            image.color = Color.Lerp(startColor, endColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
