namespace Company.Session03.PL.Services
{
    public class SengeltonService : ISengeltonService
    {
        public SengeltonService()
        {
            Guid = new Guid();
        }
        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
