using Apsy.Common.Api.Core.Graph;
using System.Collections.Generic;
using System.Linq;

namespace Apsy.Common.Api.Core
{
    public enum EventType
    {
        Added,
        Updated,
        Deleted
    }

    public class Update<T>
    {
        [Api]
        public string PropName { get; set; }
        [Api]
        public T OldValue { get; set; }
    }

    public class Observable<T>
    {
        public Observable(EventType eventType, T @object)
        {
            EventType = eventType;
            Object = @object;
        }

        public Observable(EventType eventType, T @object, params Update<T>[] updates)
        {
            EventType = eventType;
            Object = @object;
            Updates = updates.ToList();
        }

        [Api]
        public EventType EventType { get; private set; }
        [Api]
        public List<Update<T>> Updates { get; private set; }
        [Api]
        public T Object { get; private set; }
    }
}
