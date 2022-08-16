namespace Fragments.Domain.Dto
{
    public class FilterAndSearchDto
    {
        public List<string> Roles { get; set; } = null!;
        public bool RepresentativeHEI { get; set; }
        public bool RepresentativeAuthority { get; set; }
        public string SearchText { get; set; } = null!; 
        public bool IsFiltering { get; set; }   
    }
}
