namespace Company.Session03.PL.Services
{
    public interface ISengeltonService
    {
        public Guid Guid { get; set; }

        string GetGuid();
    }
}
