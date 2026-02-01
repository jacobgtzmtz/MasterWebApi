namespace Domain
{
    public class Foto: BaseEntity
    {
        public string? Url { get; set; }
        public Guid? CursoId { get; set; }
        public Curso? Curso { get; set; }
    
    }
}