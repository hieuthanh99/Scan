using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.Core
{
    public class ConcurrentObservableCollection<T> : ObservableCollection<T>
    {
        public ConcurrentObservableCollection() : base() { }

        public ConcurrentObservableCollection(List<T> listImages) : base(listImages) { }
    }
}
