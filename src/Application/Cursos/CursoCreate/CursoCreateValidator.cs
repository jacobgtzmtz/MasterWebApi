using FluentValidation;

namespace Application.Cursos.CursoCreate;

public class CursoCreateValidator : AbstractValidator<CursoCreateRequest>
{
    public CursoCreateValidator()
    {
       RuleFor(x => x.Titulo)
        .NotEmpty().WithMessage("El título es obligatorio.")
        .MaximumLength(100).WithMessage("El título no puede exceder los 100 caracteres."); 
        RuleFor(x => x.Descripcion)
        .MaximumLength(100).WithMessage("La descripción no puede exceder los 100 caracteres."); 
    }
    

}
