namespace Application.Precios.GetPrecios;

public record PrecioResponse
(
    Guid Id,
    string Nombre,
    decimal PrecioActual,
    decimal PrecioPromocion
);
