using System;
using System.Collections.Generic;
using Xamarin.Forms;

internal static class InternalExtension
{
    internal static void InternalPropertyChanged(BindableObject bindable, BindableProperty property, Func<bool> removalConditionFunc, IList<Type> supportedTypes = null)
    {
        if (supportedTypes != null && !supportedTypes.Contains(bindable.GetType()))
        {
            return;
        }

        var element = bindable as Element;
        string effectName = GetEffectName(property.PropertyName);
        Effect toRemove = null;

        foreach (var effect in element.Effects)
        {
            if (effect.ResolveId == effectName)
            {
                toRemove = effect;

                break;
            }
        }

        if (toRemove == null)
        {
            element.Effects.Add(Effect.Resolve(effectName));
        }
        else
        {
            if (removalConditionFunc())
            {
                element.Effects.Remove(toRemove);
            }
        }
    }

    internal static string GetEffectName(string propertyName)
    {
        return string.Format("Tizen.{0}Effect", propertyName);
    }
}