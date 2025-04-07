using Dapper;
using RegEstudiantes.DataContext;
using RegEstudiantes.Interfaces;
using RegEstudiantes.Models;
using System.Data;

namespace RegEstudiantes.Repository
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly DapperContext _context;

        public EstudianteRepository(DapperContext context)
        {
           _context = context;
        }
        public async Task<List<Estudiante>> GetAllEstudiantes()
        {
            using IDbConnection db = _context.CreateConnection();
            try 
            {
                string sqlQuery = @"SELECT s.*, sub.*, p.* 
                                    FROM Estudiantes s
                                    LEFT JOIN EstudiantesMaterias ss ON s.Id = ss.EstudianteId
                                    LEFT JOIN Materias sub ON ss.MateriaId = sub.Id
                                    LEFT JOIN Profesores p ON sub.ProfesorId = p.Id";

                var estudianteDic = new Dictionary<int, Estudiante>();

                var result = await db.QueryAsync<Estudiante, Materia, Profesor, Estudiante>(
                sqlQuery,

                (estudiante, materia, profesor) =>
                {
                    if (!estudianteDic.TryGetValue(estudiante.id, out var currentStudent))
                    {
                        currentStudent = estudiante;
                        estudianteDic.Add(currentStudent.id, currentStudent);
                    }

                    if (materia != null)
                    {
                        materia.profesor = profesor;
                        currentStudent.materias.Add(materia);
                    }

                    return currentStudent;
                },
                splitOn: "Id,Id"
            );
                return estudianteDic.Values.ToList();
            }            
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        public async Task<int> CreateEstudiante(Estudiante estudiante)
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                string sqlQuery = @"INSERT INTO Estudiantes (nombre, email, fechaCreacion)
                                    VALUES (@nombre, @email, @fechaCreacion);
                                    SELECT CAST(SCOPE_IDENTITY() as int)";

                var id = await db.ExecuteScalarAsync<int>(sqlQuery, estudiante);
                return id;
            }
            catch(Exception ex)
            {
                var message = ex.Message;
                return 0;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }


        public async Task<bool> AsignaMateriaEstudiante(int estudianteId, List<int> materiaIds)
        {
            using IDbConnection db = _context.CreateConnection();            
            try
            {
                if(materiaIds.Count > 0)
                {
                    string sqlQueary = "DELETE FROM EstudiantesMaterias WHERE estudianteId = @id";

                    await db.ExecuteAsync(sqlQueary, new {id = estudianteId});
                }                    

                string sqlQuery = @"INSERT INTO EstudiantesMaterias (estudianteId, materiaId)
                                    VALUES (@estudianteIdM, @materiaId)";                

                foreach (var item in materiaIds)
                {
                    await db.ExecuteAsync(sqlQuery, new { estudianteIdM = estudianteId, materiaId = item });
                }

                return true;
            }
            catch(Exception ex)
            {
                var message = ex.Message;
                return false;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }        

        public async Task<Estudiante> GetEstudianteId(int id)
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                string sqlQuery = @"SELECT s.*, sub.*, p.* 
                                    FROM Estudiantes s
                                    LEFT JOIN EstudiantesMaterias ss ON s.Id = ss.EstudianteId
                                    LEFT JOIN Materias sub ON ss.MateriaId = sub.Id
                                    LEFT JOIN Profesores p ON sub.ProfesorId = p.Id
                                    WHERE s.Id = @Id";

                var estudianteDict = new Dictionary<int, Estudiante>();
                await db.QueryAsync<Estudiante, Materia, Profesor, Estudiante>(
                    sqlQuery,
                    (estudiante, materia, profesor) =>
                    {
                        if (!estudianteDict.TryGetValue(estudiante.id, out var currentStudent))
                        {
                            currentStudent = estudiante;
                            estudianteDict.Add(estudiante.id, currentStudent);
                        }

                        if (materia != null)
                        {
                            materia.profesor = profesor;
                            currentStudent.materias.Add(materia);
                        }

                        return currentStudent;
                    },
                    new { Id = id },
                    splitOn: "Id,Id"
                );

                return estudianteDict.Values.FirstOrDefault();
            }
            finally
            {
                if(db.State == ConnectionState.Open)
                    db.Close();
            }
            
        }

        public async Task<bool> ActualizaEstudiante(Estudiante estudiante)
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                string sql = @"UPDATE Estudiantes 
                               SET nombre = @nombre, 
                                   email = @email, 
                                   fechaCreacion = @fechaCreacion 
                               WHERE id = @id";
                
                var update = await db.ExecuteAsync(sql, estudiante);
                return update > 0;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }
        public async Task<bool> EliminaEstudiante(int id)
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                string sql = "DELETE FROM Estudiantes WHERE id = @Id";
                
                int delEstudiante = await db.ExecuteAsync(sql, new { Id = id });

                sql = "DELETE FROM EstudiantesMaterias WHERE estudianteId = @id";

                int matteriasEstudiante = await db.ExecuteAsync(sql, new { Id = id });

                return delEstudiante > 0;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        public async Task<List<ClaseEstudiante>> GetClaseEstudiante(int id)
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                const string sql = @"SELECT 
                                         s2.id,
                                         s2.nombre,
                                         sub.nombre AS materia
                                     FROM EstudiantesMaterias ss1
                                     JOIN EstudiantesMaterias ss2 ON ss1.materiaId = ss2.materiaId
                                     JOIN Estudiantes s1 ON ss1.estudianteId = s1.id
                                     JOIN Estudiantes s2 ON ss2.estudianteId = s2.id
                                     JOIN Materias sub ON ss1.materiaId = sub.id
                                     WHERE s1.id = @estudianteId AND s2.id != @estudianteId
                                     ORDER BY sub.nombre, s2.nombre";
                
                return (await db.QueryAsync<ClaseEstudiante>(sql, new { estudianteId = id })).ToList();
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        public async Task<bool> ExisteEstudiante(int id)
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                const string sql = "SELECT 1 FROM Estudiantes WHERE id = @Id";                
                return await db.ExecuteScalarAsync<bool>(sql, new { id = id });
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }

        public async Task<bool> TieneMaterias(int id)
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                const string sql = "SELECT 1 FROM EstudiantesMaterias WHERE estudianteId = @Id";
                return await db.ExecuteScalarAsync<bool>(sql, new { Id = id });
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
        }
    }
}
