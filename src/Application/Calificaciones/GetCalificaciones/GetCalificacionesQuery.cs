namespace Application.Calificaciones.GetCalificaciones;

public record CalificacionResponse
(
    string Alumno,
    int Puntuacion,
    string Comentario,
    string NombreCurso
);
