namespace Ion.Tools
{
    using System.Collections.ObjectModel;
    using System.Linq;

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
            {
                var index = this.Items.IndexOf(item);
                this.Items[index] = item;
            }
            else
                this.Items.Add(item);
        }
    }
}