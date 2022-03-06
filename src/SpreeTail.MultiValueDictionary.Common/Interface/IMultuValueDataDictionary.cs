using System.Collections.Generic;

namespace SpreeTail.MultiValueDictionary.Common.Interface
{
    public interface IMultuValueDataDictionary
    {
        bool Add(string key, string value);
        bool CheckIfKeyExist(string key);
        bool CheckIfMemberExist(string key, string member);
        bool RemoveKey(string key);
        bool RemoveMember(string key, string value);
        List<string> GetAllMembersOfAKey(string key);
        List<string> GetAllMembers();
        List<string> GetAllKeys();
        void ClearDictionary();
        List<string> GetAllKeysAndValues();
    }
}
