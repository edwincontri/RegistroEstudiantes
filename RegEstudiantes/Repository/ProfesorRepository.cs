using Dapper;
using RegEstudiantes.DataContext;
using RegEstudiantes.Interfaces;
using RegEstudiantes.Models;
using System.Data;

namespace RegEstudiantes.Repository
{
    public class ProfesorRepository : IProfesorRepository
    {
        private readonly DapperContext _context;
        public ProfesorRepository(DapperContext context) 
        {
            _context = context;
        }

        public async Task<List<Profesor>> GetProfesor()
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                string sql = @"SELECT p.*, s.* 
                               FROM Profesores p
                               LEFT JOIN Materias s ON p.Id = s.ProfesorId";                

                var professorDict = new Dictionary<int, Profesor>();

                await db.QueryAsync<Profesor, Materia, Profesor>(
                    sql,
                    (profesor, materia) =>
                    {
                        if (!professorDict.TryGetValue(profesor.id, out var currentProfessor))
                        {
                            currentProfessor = profesor;
                            professorDict.Add(profesor.id, currentProfessor);
                        }

                        if (materia != null)
                            currentProfessor.materias.Add(materia);

                        return currentProfessor;
                    },
                    splitOn: "Id"
                );

                return professorDict.Values.ToList();
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        public async Task<Profesor> GetProfesorID(int id)
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                string sql = @"SELECT p.*, s.* 
                               FROM Profesores p
                               LEFT JOIN Materias s ON p.Id = s.ProfesorId
                               WHERE p.Id = @Id";                

                var professorDict = new Dictionary<int, Profesor>();

                await db.QueryAsync<Profesor, Materia, Profesor>(
                    sql,
                    (profesor, materia) =>
                    {
                        if (!professorDict.TryGetValue(profesor.id, out var currentProfessor))
                        {
                            currentProfessor = profesor;
                            professorDict.Add(profesor.id, currentProfessor);
                        }

                        if (materia != null)
                            currentProfessor.materias.Add(materia);

                        return currentProfessor;
                    },
                    new { Id = id },
                    splitOn: "Id"
                );

                return professorDict.Values.FirstOrDefault();
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }
    }
}
