namespace Films.Api.Models

{
    public class Film
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Director { get; set; }
        public required string Producer { get; set; }
        public required string ReleaseYear { get; set; }
        public GenderCategory Category { get; set; }
        
    }

    public enum GenderCategory
    {
        Terror = 0,
        Ficção = 1,
        Suspense = 2,
        Comedia = 3,
        Romance = 4,
        Ação = 5,
        Fantasia = 6
    }
}