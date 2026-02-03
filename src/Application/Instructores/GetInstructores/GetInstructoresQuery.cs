namespace Application.Instructores.GetInstructores;

public record InstructorResponse
(
    Guid Id,
    string Nombre,
    string Apellidos,
    string Grado
);
