using System;
using System.Security.Cryptography;

namespace BlazorCodeGeneration
{
    public class ClassItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Desc { get; set; }
        public string Type { get; set; }
        public bool IsRequired { get; set; }
        public int StringLength { get; set; }

        public ClassItem()
        {
            ClearValues();
        }

        public virtual void Clear()
        {
            ClearValues();
        }

        private void ClearValues()
        {
            Id = new Guid();
            Name = string.Empty;
            DisplayName = string.Empty;
            Desc = string.Empty;
            Type = string.Empty;
            IsRequired = false;
            StringLength = 0;
        }
    }
}
