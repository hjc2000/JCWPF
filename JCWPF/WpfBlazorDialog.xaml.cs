using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace JCWPF;

/// <summary>
///		帮助在 WPF 弹窗中显示 Blazor 组件。
///		<br/>* 本构造函数会在依赖服务中注入本窗口自己，即本 Window 对象。这样就能通过依赖服务
///			   获取并操作自己了。
/// </summary>
public partial class WpfBlazorDialog : Window
{
	/// <summary>
	///		构造函数。
	///		传入 Blazor 组件类的类型，创建一个窗口用来显示该组件。
	///		<br/>* 本构造函数会在依赖服务中注入本窗口自己，即本 Window 对象。这样就能通过依赖服务
	///			   获取并操作自己了。
	/// </summary>
	/// <param name="blazor_component_type">要在弹窗中显示的 Blazor 组件类的类型。</param>
	public WpfBlazorDialog(Type blazor_component_type)
		: this(blazor_component_type, new ServiceCollection())
	{

	}

	/// <summary>
	///		构造函数。
	///		<br/>* 传入 Blazor 组件类的类型，创建一个窗口用来显示该组件。
	///		<br/>* 同时还可以用依赖注入的方式向弹窗注入对象，进行与弹窗的通信。
	///		<br/>* 本构造函数会在依赖服务中注入本窗口自己，即本 Window 对象。这样就能通过依赖服务
	///			   获取并操作自己了。
	/// </summary>
	/// <param name="blazor_component_type">要在弹窗中显示的 Blazor 组件类的类型。</param>
	/// <param name="serviceCollection">
	///		服务集合。
	///		<br/>* 可以注入一些对象，进行与弹窗的通信。同时本构造函数也需要在服务集合里注入一些依赖。
	///	</param>
	public WpfBlazorDialog(Type blazor_component_type, IServiceCollection serviceCollection)
	{
		InitializeComponent();

		// 注入本窗口自己。这样 blazor 就可以通过依赖服务获取到本窗口，进而执行操作，例如关闭自己。
		serviceCollection.AddSingleton<Window>((p) =>
		{
			return this;
		});

		serviceCollection.AddWpfBlazorWebView();
		Resources.Add("services", serviceCollection.BuildServiceProvider());
		_root_component.ComponentType = blazor_component_type;
	}
}
