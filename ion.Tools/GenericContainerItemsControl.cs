namespace Ion.Tools
{
    using System.Windows.Controls;

    /// <summary>
    /// ItemsControl with generic container
    /// </summary>
    /// <typeparam name="T"> T is the type of the container </typeparam>
    public class GenericContainerItemsControl<T> : ItemsControl where T : Control, new()
    {
        /// <summary>
        /// Gets the T container from item
        /// </summary>
        /// <param name="item"> Item from GenericContainerItemsControl </param>
        /// <returns> Return container as T </returns>
        public T GetGenericContainerFromItem(object item)
        {
            return this.ItemContainerGenerator.ContainerFromItem(item) as T;
        }

        /// <summary>
        /// GetContainerForItemOverride override method
        /// </summary>
        /// <returns> new T() </returns>
        protected override System.Windows.DependencyObject GetContainerForItemOverride()
        {
            return new T();
        }

        /// <summary>
        /// IsItemItsOwnContainerOverride override method
        /// </summary>
        /// <param name="item"> Item from GenericContainerItemsControl </param>
        /// <returns> Returns item as T </returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is T;
        }
    }
}