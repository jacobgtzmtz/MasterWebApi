namespace Application.Instructores.GetInstructores;

public record InstructorResponse
(
    Guid Id,
    string Nombre,
    string Apellido,
    string Grado
);
