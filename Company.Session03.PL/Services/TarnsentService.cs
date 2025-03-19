namespace Company.Session03.PL.Services
{
    public class TarnsentService : ITarnsentService
    {
        public TarnsentService()
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
