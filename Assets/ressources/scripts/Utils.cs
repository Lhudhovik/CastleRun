using System;

namespace AssemblyCSharp
{
		public class Utils
		{
				public static float FloatRemap (float value, float prevMin, float prevMax, float nextMin, float nextMax)
				{
			
						return (value - prevMin) * (nextMax - nextMin) / (prevMax - prevMin) + nextMin;
			
				}
		}
}

