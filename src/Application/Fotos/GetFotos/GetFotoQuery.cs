namespace Application.Fotos.GetFotos;

public record FotoResponse
(
    Guid Id,
    string Url,
    Guid CursoId
);
