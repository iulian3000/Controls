namespace Ion.Tools
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Extended ObservableCollection AddOrReplaceExistent item method
    /// </summary>
    /// <typeparam name="T"> typeof collection </typeparam>
    public class ObservableCollectionEx<T> : ObservableCollection<T>
    {
        /// <summary>
        /// Add or replaces existing itme in collection
        /// </summary>
        /// <param name="item"> Item to add </param>
        public void AddOrReplaceExistent(T item)
        {
            if (this.Items.Contains(item))
                this.Items.Remove(item);
            else
                this.Items.Add(item);
        }
    }
}