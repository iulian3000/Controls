using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ion.Tools
{

    class GenericContainerItemsControl<T> : ItemsControl where T : Control, new()
    {
        protected override System.Windows.DependencyObject GetContainerForItemOverride()
        {
            return new T();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is T;
        }

        public T GetGenericContainerFromItem(object item)
        {
            return this.ItemContainerGenerator.ContainerFromItem(item) as T;
        }
    }
}
