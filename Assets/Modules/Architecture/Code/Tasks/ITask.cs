using Cysharp.Threading.Tasks;

namespace verell.Architecture
{
	public interface ITask
	{
		UniTask StartAsync();
	}
}
