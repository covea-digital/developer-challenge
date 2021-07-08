using System.Collections.Generic;

namespace PetInsuranceApi.CodeLists
{
    public class CodeList
    {
        public string ListName { get; set; }

        public IEnumerable<CodeItem> Items { get; set; }
    }
}