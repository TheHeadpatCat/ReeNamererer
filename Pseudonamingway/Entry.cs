using System;
using System.Collections.Generic;
using System.Text;

namespace ReeNamererer
{
    [Serializable]
    public class Entry
    {
        public List<uint> IDs { get; set; } = new List<uint>();
        public string RealName { get; set; } = "Real Name";
        public string NickName { get; set; } = "Funny Name";
        public bool IsTitleEnabled { get; set; } = false;
        public string Title { get; set; } = "《Funny Title》";


        public bool ContainsID(uint id)
        {
            return IDs.Contains(id);
        }

        public string GetIDString()
        {
            string result = "";
            int i = 0;
            foreach (uint id in IDs)
            {
                if (i == 0)
                    result += id;
                else
                    result += "," + id;
                i++;
            }
            return result;
        }

        public bool SetIDsFromString(string input)
        {
            IDs.Clear();
            foreach (var part in input.Split(","))
            {
                if(uint.TryParse(part, out uint id))
                {
                    IDs.Add(id);
                }
                else
                {
                    // todo: maybe clear?
                    return false;
                }
            }
            return true;
        }
    }
}
