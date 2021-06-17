using RodosApi.Domain;

namespace RodosApi.Contract.V1.Response
{
    public class MakerResponse
    {
        public long MakerId { get; set; }
        public string Name { get; set; }
        public CountryResponse CountryResponse{ get; set; }
    }
}