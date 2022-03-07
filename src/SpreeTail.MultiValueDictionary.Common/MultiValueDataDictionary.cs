using SpreeTail.MultiValueDictionary.Common.Interface;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SpreeTail.MultiValueDictionary.Common
{
    public class MultiValueDataDictionary : IMultuValueDataDictionary
    {
        private ConcurrentDictionary<string, List<string>> dictionary;
        private static MultiValueDataDictionary instance;
        private static object _lock = new object();

        private MultiValueDataDictionary()
        {
            dictionary = new ConcurrentDictionary<string, List<string>>();
            instance = null;
        }

        public static MultiValueDataDictionary GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new MultiValueDataDictionary();
                    }
                }
            }
            return instance;
        }

        public bool Add(string key, string value)
        {
            if(dictionary.TryGetValue(key, out var values))
            {
                values.Add(value);
                dictionary[key] = values;
                return true;
            }
            
            return dictionary.TryAdd(key, new List<string> { value });
        }

        public bool CheckIfKeyExist(string key)
        {
            return dictionary.ContainsKey(key);
        }

        public bool CheckIfMemberExist(string key, string member)
        {
            if(dictionary.TryGetValue(key, out var values))
            {
                return values.Contains(member);
            }

            return false;
        }

        public bool RemoveKey(string key)
        {
            return  dictionary.TryRemove(key, out var _);
        }

        public bool RemoveMember(string key, string value)
        {
            if (dictionary.TryGetValue(key, out var values))
            {
                if(values.Count == 1)
                {
                    return RemoveKey(key);
                }

                values.Remove(value);
                dictionary[key] = values;
                return true;
            }
            return false;
        }

        public List<string> GetAllMembersOfAKey(string key)
        {
            return dictionary[key];
        }

        public List<string> GetAllMembers()
        {
            return dictionary.SelectMany(x => x.Value).ToList();
        }

        public List<string> GetAllKeys()
        {
            return new List<string>(dictionary.Keys);
        }

        public void ClearDictionary()
        {
            dictionary.Clear();
        }

        public List<string> GetAllKeysAndValues()
        {
            return dictionary.SelectMany(x => x.Value.Select(y => x.Key + ":" + y)).ToList();
        }
    }
}
