using Services.Abstractions;

namespace Services
{
    public class HubConnectionsService : IHubConnectionsService
    {
        private readonly Dictionary<int, List<string>> _connectionDictionary;

        public HubConnectionsService()
        {
            _connectionDictionary = new Dictionary<int, List<string>>();
        }

        public bool TryGetValue(int key, out List<string> value) => _connectionDictionary.TryGetValue(key, out value);

        public bool TryGetValues(out List<string> values, params int[] keys)
        {
            values = _connectionDictionary
                .Where(x => keys.Contains(x.Key))
                .Select(x => x.Value)
                .SelectMany(list => list)
                .ToList();

            return values.Count > 0;
        }

        public void Add(int key, List<string> value) => _connectionDictionary.Add(key, value);

        public bool Remove(int key) => _connectionDictionary.Remove(key);

    }
}
