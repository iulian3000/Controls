using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ion.Tools
{
    /// <summary>
    /// Extended ObservableCollection
    /// AddOrReplaceExistent item method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableCollectionEx<T> : ObservableCollection<T>
    {
        public void AddOrReplaceExistent(T item)
        {
            if (this.Items.Contains(item))
                this.Items.Remove(item);
            else
                this.Items.Add(item);
        }
    }
}
