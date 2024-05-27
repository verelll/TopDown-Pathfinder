using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace verell.Architecture
{
    public sealed class TasksQueue
    {
        private readonly Queue<ITask> _queue;

        public TasksQueue()
        {
            _queue = new Queue<ITask>();
        }
        
        public void AddTask(ITask task)
        {
            _queue.Enqueue(task);
        }
		
        public async UniTask StartAsync()
        {
            while (_queue.Count > 0)
            {
                var task = _queue.Dequeue();
                await task.StartAsync();
            }
        }
    }
}