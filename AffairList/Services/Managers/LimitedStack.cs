namespace AffairList.Services.Managers
{
    public class LimitedStack<T>
    {
        private LinkedList<T> _list;
        public int Capacity { get; private set; }
        public int Count { get => _list.Count; }

        public LimitedStack(int capacity)
        {
            _list = new();
            Capacity = capacity;
        }
        public void Push(T element)
        {
            if (Count == Capacity) _list.RemoveFirst();
            _list.AddLast(element);
        }
        public T Pop()
        {
            if(Count == 0) throw new InvalidOperationException("Stack is empty");
            var lastItem = _list.Last.Value;
            _list.RemoveLast();
            return lastItem;
        }
        public T Peek() => _list.Last.Value ?? throw new InvalidOperationException("Stack is empty");
        public void Clear() => _list = new();
    }
}
