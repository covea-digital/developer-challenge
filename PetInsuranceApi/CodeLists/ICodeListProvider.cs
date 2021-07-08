namespace PetInsuranceApi.CodeLists
{
    public interface ICodeListProvider
    {
        CodeList RetrieveDogs { get; }
        CodeList RetrieveCats { get; }
    }
}