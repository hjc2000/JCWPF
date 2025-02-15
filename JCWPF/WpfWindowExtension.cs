using System.Windows;

namespace JCWPF;

/// <summary>
///		提供 WPF 中的 Window 类的扩展方法。
/// </summary>
public static class WpfWindowExtension
{
	/// <summary>
	///		以异步方式显示模态对话框。
	///		<br/>* 在 Blaozr 中，如果直接调用 Window 对象的 ShowDialog 方法，就会造成
	///			   死锁。ShowDialog 是阻塞的，调用者，即 UI 线程在等待 ShowDialog 返回，
	///			   而 ShowDialog 里面的 UI 又需要 UI 线程去显示。于是打开的窗口是空白的，
	///			   无法显示。
	///		<br/>* 使用本扩展方法就可以以异步的方式打开模态对话框，释放 UI 线程到对话框中去
	///			   渲染。
	/// </summary>
	/// <param name="window"></param>
	/// <returns></returns>
	public static async Task ShowDialogAsync(this Window window)
	{
		System.Windows.Threading.Dispatcher dispatcher = Application.Current.Dispatcher;
		await dispatcher.InvokeAsync(() =>
		{
			window.ShowDialog();
		});
	}
}
