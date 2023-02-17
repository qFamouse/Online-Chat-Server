using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IHubConnectionsService
    {
        bool TryGetValue(int key, out List<string> value);
        bool TryGetValues(out List<string> values, params int[] keys);
        void Add(int key, List<string> value);
        bool Remove(int key);
    }
}
