using System;
using System.Windows;

namespace XamlFlair
{
	public sealed class XamlFlairResources : ResourceDictionary
	{
		public XamlFlairResources()
		{
			Source = new Uri("pack://application:,,,/XamlFlair.WPF;component/DefaultAnimations.xaml");
		}
	}
}