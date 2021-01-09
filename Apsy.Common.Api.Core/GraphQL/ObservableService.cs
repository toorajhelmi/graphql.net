using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Apsy.Common.Api.Core
{
    public class ObservableService<T>
    {
        private readonly ISubject<Observable<T>> eventStream = new ReplaySubject<Observable<T>>(1);

        public IObservable<Observable<T>> EventStream()
        {
            return eventStream.AsObservable();
        }

        protected void AddError(Exception exception)
        {
            eventStream.OnError(exception);
        }

        protected void RaiseEvent(Observable<T> observable)
        {
            eventStream.OnNext(observable);
        }
    }
}
