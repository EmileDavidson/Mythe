using System;
using System.Collections.Generic;

public class EasingDictonary {
    public static Dictionary<EasingType, Func<float, float>> dict = new Dictionary<EasingType, Func<float, float>>
    {
        {EasingType.Linear, Easing.Linear},
        {EasingType.EaseInSine, Easing.EaseInSine},
        {EasingType.EaseOutSine, Easing.EaseOutSine},
        {EasingType.EaseInOutSine, Easing.EaseInOutSine},
        {EasingType.EaseInCubic, Easing.EaseInCubic},
        {EasingType.EaseOutCubic, Easing.EaseOutCubic},
        {EasingType.EaseInOutCubic, Easing.EaseInOutCubic},
        {EasingType.EaseInQuint, Easing.EaseInQuint},
        {EasingType.EaseOutQuint, Easing.EaseOutQuint},
        {EasingType.EaseInOutQuint, Easing.EaseInOutQuint},
        {EasingType.EaseInCirc, Easing.EaseInCirc},
        {EasingType.EaseOutCirc, Easing.EaseOutCirc},
        {EasingType.EaseInOutCirc, Easing.EaseInOutCirc},
        {EasingType.EaseInElastic, Easing.EaseInElastic},
        {EasingType.EaseOutElastic, Easing.EaseOutElastic},
        {EasingType.EaseInOutElastic, Easing.EaseInOutElastic},
        {EasingType.EaseInQuad, Easing.EaseInQuad},
        {EasingType.EaseOutQuad, Easing.EaseOutQuad},
        {EasingType.EaseInOutQuad, Easing.EaseInOutQuad},
        {EasingType.EaseInQuart, Easing.EaseInQuart},
        {EasingType.EaseOutQuart, Easing.EaseOutQuart},
        {EasingType.EaseInOutQuart, Easing.EaseInOutQuart},
        {EasingType.EaseInExpo, Easing.EaseInExpo},
        {EasingType.EaseOutExpo, Easing.EaseOutExpo},
        {EasingType.EaseInOutExpo, Easing.EaseInOutExpo},
        {EasingType.EaseInBack, Easing.EaseInBack},
        {EasingType.EaseOutBack, Easing.EaseOutBack},
        {EasingType.EaseInOutBack, Easing.EaseInOutBack},
        {EasingType.EaseInBounce, Easing.EaseInBounce},
        {EasingType.EaseOutBounce, Easing.EaseOutBounce},
        {EasingType.EaseInOutBounce, Easing.EaseInOutBounce}
    };
}