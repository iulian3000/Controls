namespace Ion.Tools
{
    using System.Collections.Specialized;
    using System.ComponentModel;

    /// <summary>
    /// Extended ObservableCollection AddOrReplaceExistent item method Notify collection changed on
    /// any item who raises INotifyPropertyChanged
    /// </summary>
    /// <typeparam name="T"> typeof collection </typeparam>
    public class ObservableCollectionEx2<T> : ObservableCollectionEx<T> where T : INotifyPropertyChanged
    {
        /// <summary>
        /// OnCollectionChanged overriden method
        /// </summary>
        /// <param name="e"> NotifyCollectionChangedEventArgs </param>
        protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                {
                    item.PropertyChanged -= Item_PropertyChanged;
                }
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }
        }

        /// <summary>
        /// item changed void method
        /// </summary>
        /// <param name="sender"> The sender </param>
        /// <param name="e">      PropertyChangedEventArgs </param>
        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(args);
        }
    }
}