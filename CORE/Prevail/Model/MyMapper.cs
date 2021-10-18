using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Prevail.Model
{
	public static class MyMapper
	{
		public static TDestination Map<TDestination, TSource>(TSource source)
		{
			var sourceProps = typeof(TSource).GetProperties();
			var destinationProps = typeof(TDestination).GetProperties();
			TDestination destination = (TDestination)Activator.CreateInstance(typeof(TDestination));
			foreach (var sourceProp in sourceProps)
			{
				foreach (var destinationProp in destinationProps)
				{
					if (sourceProp.Name == destinationProp.Name)
					{
						if (sourceProp.PropertyType == destinationProp.PropertyType)
						{
							var value = typeof(TSource).GetProperty(destinationProp.Name).GetValue(source);
							typeof(TDestination).GetProperty(destinationProp.Name).SetValue(destination, value);
						}
					}
				}
			}
			return destination;
		}
	}
}
