using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Cursos.CursoReporteExcel
{
    public class CursoReporteExcelQuery
    {
        public record CursoReporteExcelQueryRequest() : IRequest<MemoryStream>;

        internal class CursoReporteExcelQueryHandler : IRequestHandler<CursoReporteExcelQueryRequest, MemoryStream>
        {
            private readonly MasterNetDBContext _context;
            private readonly IReportService<Curso> _reportService;

            public CursoReporteExcelQueryHandler(MasterNetDBContext context, IReportService<Curso> reportService)
            {
                _context = context;
                _reportService = reportService;
            }

            public async Task<MemoryStream> Handle(CursoReporteExcelQueryRequest request, CancellationToken cancellationToken)
            {
                var cursos = await _context.Cursos!.ToListAsync();
                return await _reportService.GetCsvReport(cursos);
            }
        }

    }
}