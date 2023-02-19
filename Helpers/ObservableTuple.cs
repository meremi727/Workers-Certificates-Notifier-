using WpfApp1.MVVM;

namespace WpfApp1.Helpers
{
    public class ObservableTuple<T1, T2> : NotificationObject
    {
        public T1 Item1
        {
            get => _item1;
            set => UpdatePropertyValue(ref _item1, value);
        }

        public T2 Item2
        {
            get => _item2;
            set => UpdatePropertyValue(ref _item2, value);
        }

        public ObservableTuple(T1 item1, T2 item2)
        {
            _item1 = item1;
            _item2 = item2;
        }

        private T1 _item1;
        private T2 _item2;
    }
}
