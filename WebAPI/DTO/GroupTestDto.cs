namespace WebAPI.DTO
{
    public class GroupTestDto
    {
        public Guid GroupTestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public List<TestDto>? Tests { get; set; }
    }
}
